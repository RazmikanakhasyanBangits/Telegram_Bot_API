using System;

namespace Core.Helper
{
    public static class DistanceHelper
    {
        public static double CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            double earthRadius = 6371;

            double dLat = ToRadians(latitude2 - latitude1);
            double dLon = ToRadians(longitude2 - longitude1);

            latitude1 = ToRadians(latitude1);
            latitude2 = ToRadians(latitude2);

            double halfChordLength = (Math.Sin(dLat / 2) * Math.Sin(dLat / 2)) +
                       (Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(latitude1) * Math.Cos(latitude2));
            double angularDistance = 2 * Math.Atan2(Math.Sqrt(halfChordLength), Math.Sqrt(1 - halfChordLength));
            double distance = earthRadius * angularDistance;

            return distance;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
