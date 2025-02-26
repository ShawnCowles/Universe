namespace VindemiatrixCollective.Universe.Model
{
    public class Planet : CelestialBody
    {
        public void AddSattelites(IEnumerable<Planet> planets)
        {
            AddOrbiters(planets);   
        }
    }
}