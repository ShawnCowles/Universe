using UnitsNet;
using VindemiatrixCollective.Universe.CelestialMechanics.Orbits;

namespace VindemiatrixCollective.Universe.Model
{
    public interface IOrbitingThing
    {
        public OrbitState? OrbitState { get; }

        void CelestialUpdate(Duration deltaTime, DateTime currentDate);
    }
}
