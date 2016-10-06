using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace MapTool.MapAPI
{
    public class LocationManager
    {
        public async static Task<Geoposition> GetLocation()
        {
            var geoaccess = await Geolocator.RequestAccessAsync();
            //位置权限不允许
            if (geoaccess != GeolocationAccessStatus.Allowed) throw new Exception();

            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var geolocation = await geolocator.GetGeopositionAsync();
            return geolocation;
        }
    }
}
