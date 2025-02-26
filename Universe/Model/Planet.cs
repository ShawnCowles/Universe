namespace VindemiatrixCollective.Universe.Model
{
    public class Planet : CelestialBody
    {
        public void AddSatellite(CelestialBody satellite)
        {
            AddOrbiter(satellite);
        }

        public void AddSatellites(IEnumerable<CelestialBody> satellites)
        {
            AddOrbiters(satellites);
        }
    }
}