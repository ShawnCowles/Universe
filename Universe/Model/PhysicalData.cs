using Godot.Collections;
using System;
using UnitsNet;
using VindemiatrixCollective.Universe.CelestialMechanics;

namespace VindemiatrixCollective.Universe.Model
{
    public class PhysicalData
    {
        public Acceleration Gravity { get; private set; }
        public Density Density { get; private set; }
        public Length Radius { get; private set; }
        public Mass Mass { get; private set; }


        public static PhysicalData Create(Mass mass, Length radius, Acceleration gravity, Density density)
        {
            PhysicalData data = new()
            {
                Mass = mass,
                Radius = radius,
                Gravity = gravity,
                Density = density
            };
            return data;
        }

        public static PhysicalData Create(Density density, Length radius, GravitationalParameter gm)
        {
            Mass         mass    = Mass.FromKilograms((4 / 3d) * UniversalConstants.Tri.Pi * Math.Pow(radius.Meters, 3) * density.KilogramsPerCubicMeter);
            Acceleration gravity = Acceleration.FromMetersPerSecondSquared(gm.M3S2 / Math.Pow(radius.Meters, 2));
            return Create(mass, radius, gravity, density);
        }

        public Dictionary GetSaveData()
        {
            return new Dictionary
            {
                {"Gravity", Gravity.MetersPerSecondSquared },
                {"Density", Density.KilogramsPerCubicMeter },
                {"Radius", Radius.Meters },
                {"Mass", Mass.Kilograms },
            };
        }

        public static PhysicalData LoadSaveData(Dictionary data)
        {
            return Create(
                Mass.FromKilograms(data["Mass"].AsDouble()),
                Length.FromMeters(data["Radius"].AsDouble()),
                Acceleration.FromMetersPerSecondSquared(data["Gravity"].AsDouble()),
                Density.FromKilogramsPerCubicMeter(data["Density"].AsDouble()));
        }
    }
}