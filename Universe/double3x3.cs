namespace VindemiatrixCollective.Universe
{
    public partial struct double3x3
    {
        public double3 c0;
        public double3 c1;
        public double3 c2;

        public static readonly double3x3 identity = new double3x3(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0);

        public static readonly double3x3 zero;

        public double3x3(double3 c0, double3 c1, double3 c2)
        {
            this.c0 = c0;
            this.c1 = c1;
            this.c2 = c2;
        }

        public double3x3(double m00, double m01, double m02,
                         double m10, double m11, double m12,
                         double m20, double m21, double m22)
        {
            c0 = new double3(m00, m10, m20);
            c1 = new double3(m01, m11, m21);
            c2 = new double3(m02, m12, m22);
        }

        public unsafe ref double3 this[int index]
        {
            get
            {
                fixed (double3x3* array = &this) { return ref ((double3*)array)[index]; }
            }
        }
    }
}
