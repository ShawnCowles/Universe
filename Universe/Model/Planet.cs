namespace VindemiatrixCollective.Universe.Model
{
    public class Planet : CelestialBody
    {
        public void AddSatellite(CelestialThing satellite)
        {
            AddOrbiter(satellite);
        }

        public void AddSatellites(IEnumerable<CelestialThing> satellites)
        {
            AddOrbiters(satellites);
        }
    }
}