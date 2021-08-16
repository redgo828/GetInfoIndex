using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace GetInfoIndex
{
    public class WandCIndex
    {   
        static void Main(string[] args)
        {


            string zip = Console.ReadLine();
            GetInfo(zip);
        }
        static void GetInfo(string zip)
        {

            string url = $"http://api.openweathermap.org/data/2.5/weather?zip={zip}&units=metric&appid=4b0e837e0959313c0d99d04d1a5499af";

            HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httprespons = (HttpWebResponse)httprequest.GetResponse();

            string response;

            using (StreamReader reader = new StreamReader(httprespons.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }

            WeatrherResp weathresp = JsonConvert.DeserializeObject<WeatrherResp>(response);

            float lon = weathresp.Coord.lon;

            string longitude = lon.ToString().Replace(',', '.');
            float lat = weathresp.Coord.lat;
            string latitude = lat.ToString().Replace(',','.');
            string url_google = $"https://maps.googleapis.com/maps/api/timezone/json?location={latitude},{longitude}&timestamp=0&key=AIzaSyBJSWMwMX-0SNPe_WhANIzD-jkiKq3mfso";

            HttpWebRequest httprequest_google = (HttpWebRequest)WebRequest.Create(url_google);
            HttpWebResponse httprespons_google = (HttpWebResponse)httprequest_google.GetResponse();

            string response_google;

            using (StreamReader reader_google = new StreamReader(httprespons_google.GetResponseStream()))
            {
                response_google = reader_google.ReadToEnd();
            }

            GoogleApi timezone = JsonConvert.DeserializeObject<GoogleApi>(response_google);

            int time_zone = timezone.rawOffset / 3600;
            Console.WriteLine("City: {0}\nTemperature: {1} C\nTime zone: {2}", weathresp.Name, weathresp.Main.Temp, time_zone);
            Console.ReadLine();
        }
    }
}
