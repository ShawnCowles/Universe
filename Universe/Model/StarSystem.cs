using Godot;
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
    }
}