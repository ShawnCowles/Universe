using Godot;
using Godot.Collections;
using VindemiatrixCollective.Universe.CelestialMechanics;

namespace VindemiatrixCollective.Universe.Model
{
    public abstract class CelestialBody : CelestialThing, ICelestialBody
    {
        public GravitationalParameter Mu => GravitationalParameter.FromMass(PhysicalData.Mass);
        public PhysicalData PhysicalData { get; set; }
        
        protected virtual void AddOrbiter(CelestialThing orbiter)
        {
            orbiter.StarSystem = StarSystem;
            orbiter.SetParentBody(this);

            AddChild(orbiter);
        }

        protected void AddOrbiters(IEnumerable<CelestialThing> newOrbiters)
        {
            foreach (var orbiter in newOrbiters)
            {
                AddOrbiter(orbiter); 
            }
        }

        public override Dictionary GetSaveData()
        {
            var data = base.GetSaveData();

            data.Add("PhysicalData", PhysicalData.GetSaveData());
            
            return data;
        }

        public override void LoadSaveData(Node3D root, Node3D parent, Dictionary data)
        {
            PhysicalData = PhysicalData.LoadSaveData(data["PhysicalData"].AsGodotDictionary());

            base.LoadSaveData (root, parent, data);
        }
    }
}