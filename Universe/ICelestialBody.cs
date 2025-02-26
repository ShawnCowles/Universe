using VindemiatrixCollective.Universe.CelestialMechanics.Orbits;
using VindemiatrixCollective.Universe.Model;

namespace VindemiatrixCollective.Universe
{
    public interface ICelestialBody
    {
        OrbitState OrbitState { get; }
        PhysicalData PhysicalData { get; }
    }
}