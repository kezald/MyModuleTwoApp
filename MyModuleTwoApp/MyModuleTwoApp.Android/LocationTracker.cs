using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModuleTwoApp
{
    class LocationTracker
    {
        private static IGeolocator locator;

        public async static Task<Position> getLocation()
        {
            if (locator == null)
            {
                locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
            }

            Position position = await locator.GetPositionAsync(10000);
            return position;
        }
    }
}
