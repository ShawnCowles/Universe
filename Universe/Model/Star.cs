using Godot;
using Godot.Collections;
using UnitsNet;

namespace VindemiatrixCollective.Universe.Model
{
    public class Star : CelestialBody
    {
        public float Distance => (float)StarSystem.DistanceFromSol.LightYears;

        public Duration Age { get; set; }
        public Luminosity Luminosity { get; set; }
        public SpectralClass SpectralClass { get; set; }
        public Temperature Temperature { get; set; }

        public Mass CalculatePlanetaryMass()
        {
            Mass sum = Mass.Zero;
            foreach (var orbiter in GetChildren().OfType<CelestialBody>())
            {
                sum += orbiter.PhysicalData.Mass;
            }

            return sum;
        }

        public void AddPlanet(Planet planet)
        {
            AddOrbiter(planet);
            planet.ParentStar = this;
            foreach (var  satellite in planet.GetChildren().OfType<Planet>())
            {
                satellite.ParentStar = this;
            }
        }

        public void AddPlanets(IEnumerable<Planet> planets)
        {
            foreach (Planet planet in planets)
            {
                AddPlanet(planet);
            }
        }

        public override Dictionary GetSaveData()
        {
            var data = base.GetSaveData();

            data.Add("Age", Age.Years365);
            data.Add("Luminosity", Luminosity.Watts);
            data.Add("SpectralClass", SpectralClass.Signature);
            data.Add("Temperature", Temperature.Kelvins);

            return data;
        }

        public override void LoadSaveData(Node3D root, Node3D parent, Dictionary data)
        {
            base.LoadSaveData(root, parent, data);

            Age = Duration.FromYears365(data["Age"].AsDouble());
            Luminosity = Luminosity.FromWatts(data["Luminosity"].AsDouble());
            SpectralClass = new SpectralClass(data["SpectralClass"].AsString());
            Temperature = Temperature.FromKelvins(data["Temperature"].AsDouble());
        }
    }
}