using Xamarin.Essentials;

namespace MovieApp.Helper
{
    public static class EndPoints
    {
        // published - LOCALHOST
        //public static string BaseUrl => DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.0.100:9030/api" : "http://localhost:9030/api";

        // debug - localhost
        //public static string BaseUrl => DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.0.100:11376/api" : "http://localhost:9030/api";

        // hospedado - SmarterASP.NET
        public static string BaseUrl => DeviceInfo.Platform == DevicePlatform.Android ? "http://emersonroots-001-site1.atempurl.com/api" : "http://localhost:9030/api";
    }
}
