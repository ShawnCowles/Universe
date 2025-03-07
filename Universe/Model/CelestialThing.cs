using Godot;
using Godot.Collections;
using UnitsNet;
using VindemiatrixCollective.Universe.CelestialMechanics.Orbits;

namespace VindemiatrixCollective.Universe.Model
{
    public abstract class CelestialThing : Node3D, IOrbitingThing
    {
        private OrbitalData orbitalData;
        
        public CelestialBody ParentBody { get; internal set; }
        public OrbitState? OrbitState { get; private set; }
        public Star ParentStar { get; internal set; }
        public StarSystem StarSystem { get; internal set; }
        
        public OrbitalData OrbitalData
        {
            get => orbitalData;
            set
            {
                orbitalData = value;

                if (orbitalData != null)
                {
                    if (ParentStar != null)
                    {
                        OrbitState = OrbitState.FromOrbitalElements(orbitalData, ParentStar);
                    }
                    else
                    {
                        OrbitState = OrbitState.FromOrbitalElements(orbitalData);
                    }
                }
                else
                {
                    OrbitState = null;
                }
            }
        }

        public virtual void CelestialUpdate(Duration deltaTime, DateTime currentDate)
        {
            if (OrbitState != null)
            {
                OrbitState.Propagate(currentDate);
            }

            foreach (var child in GetChildren().OfType<IOrbitingThing>())
            {
                child.CelestialUpdate(deltaTime, currentDate);
            }
        }

        public virtual void SetParentBody(CelestialBody parentBody)
        {
            ParentBody = parentBody;
            
            if (ParentBody != null)
            {
                OrbitState.SetAttractor(parentBody);
            }
        }

        public virtual Dictionary GetSaveData()
        {
            var childrenData = new Godot.Collections.Array();

            var childrenInSaveOrder = GetChildren()
                .Where(c => (c is CelestialBody))
                .Concat(GetChildren()
                    .Where(c => c is not CelestialBody))
                .ToArray();

            foreach (var child in childrenInSaveOrder)
            {
                if (child.HasMethod("GetSaveData")
                    && !string.IsNullOrEmpty(child.SceneFilePath))
                {
                    childrenData.Add(child.Call("GetSaveData"));
                }
            }

            var data = new Dictionary
            {
                { "SceneFilePath", SceneFilePath },
                { "Name", Name },
                { "Children", childrenData },
            };

            if(OrbitalData != null)
            {
                data.Add("OrbitalData", OrbitalData.GetSaveData());
            }

            return data;
        }

        public virtual void LoadSaveData(Node3D root, Node3D parent, Dictionary data)
        {
            Name = data["Name"].AsString();

            if (data.ContainsKey("OrbitalData"))
            {
                OrbitalData = OrbitalData.LoadSaveData(data["OrbitalData"].AsGodotDictionary());
            }

            SetParentBody(parent as CelestialBody);

            PostLoadInit(root, parent, data);

            foreach (var childData in data["Children"].AsGodotArray())
            {
                var prefab = ResourceLoader.Load<PackedScene>(childData.AsGodotDictionary()["SceneFilePath"].AsString());

                var child = prefab.Instantiate();

                child.Call("LoadSaveData", root, this, childData);

                if (child.GetParent() == null)
                {
                    AddChild(child);
                }
            }
        }

        protected virtual void PostLoadInit(Node3D root, Node3D parent, Dictionary data)
        {
        }
    }
}