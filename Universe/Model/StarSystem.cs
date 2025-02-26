using Godot;
using Godot.Collections;
using UnitsNet;

namespace VindemiatrixCollective.Universe.Model
{
    [Serializable]
    public class StarSystem : Node3D
    {
        public Length DistanceFromSol => Length.FromParsecs(Coordinates.Length());

        public string Id { get; set; }

        public Vector3 Coordinates { get; set; }

        public void CelestialUpdate(Duration deltaTime, DateTime currentDate)
        {
            foreach (var child in GetChildren().OfType<CelestialBody>())
            {
                child.CelestialUpdate(deltaTime, currentDate);
            }
        }

        public void AddStar(Star star)
        {
            star.StarSystem = this;

            AddChild(star);

            foreach (var planet in star.GetChildren().OfType<Planet>())
            {
                planet.StarSystem = this;

                foreach (var moon in planet.GetChildren().OfType<Planet>())
                {
                    moon.StarSystem = this;
                }
            }
        }

        public void AddStars(IEnumerable<Star> stars)
        {
            foreach (Star star in stars)
            {
                AddStar(star);
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

            return new Dictionary
            {
                { "SceneFilePath", SceneFilePath },
                { "Name", Name },
                { "Coordinates", Coordinates },
                { "Children", childrenData }
            };
        }

        public virtual void LoadSaveData(Node3D root, Node3D parent, Dictionary data)
        {
            Name = data["Name"].AsString();
            Coordinates = data["Coordinates"].AsVector3();

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
    }
}