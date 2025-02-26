namespace VindemiatrixCollective.Universe
{
    public class MatrixMult
    {
        public static double3x3 Mul(double3x3 a, double3x3 b)
        {
            return new double3x3(
                a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z,
                a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z,
                a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
        }

        public static double3 Mul(double3x3 a, double3 b)
        {
            return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
        }
    }
}
