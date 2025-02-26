namespace VindemiatrixCollective.Universe
{
    public partial struct double3
    {
        public double x;
        public double y;
        public double z;

        public double3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double3(double v)
        {
            this.x = v;
            this.y = v;
            this.z = v;
        }

        public static implicit operator double3(double v) { return new double3(v); }

        public static double3 operator *(double3 lhs, double rhs) { return new double3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs); }

        public static double3 operator *(double lhs, double3 rhs) { return new double3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z); }

        public static double3 operator +(double3 lhs, double3 rhs) { return new double3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z); }

        public static double3 operator -(double3 val) { return new double3(-val.x, -val.y, -val.z); }

        public static double3 operator +(double3 val) { return new double3(+val.x, +val.y, +val.z); }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
