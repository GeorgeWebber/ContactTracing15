using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ContactTracing15.Pages.GovAgent
{

    [Authorize(Policy = "GovAgentOnly")]
    public class GovHomeModel : PageModel
    {


        private readonly IConfiguration _config;

        public GovHomeModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
            var Postcodes = new string[] {"OX1", "OX16", "OX1", "OX2", "OX3", "OX4", "OX5", "OX14", "SS11"};

            double[] latArray = new double[Postcodes.Count()];
            double[] longArray = new double[Postcodes.Count()];

            

            int i = 0;
            foreach (string postcode in Postcodes)
            {
                Tuple<double, double> location = GetLocation(postcode);
                Console.WriteLine(location);
                if (location != null)
                {
                    latArray[i] = location.Item1;
                    longArray[i] = location.Item2;
                    i++;
                }
            }
            Lats = new double[i];
            Longs = new double[i];

            Array.Copy(latArray, Lats, i);
            Array.Copy(longArray, Longs, i);


        }

        public Tuple<double, double> GetLocation(string postcode)
        {
            try
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&components=country:GB|postal_code:{0}&sensor=false", Uri.EscapeDataString(postcode), _config["googleApiKey"]);

                Console.WriteLine(_config["googleApiKey"]);

                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());

                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                XElement locationElement = result.Element("geometry").Element("location");
                XElement lat = locationElement.Element("lat");
                XElement lng = locationElement.Element("lng");

                Console.WriteLine(lat);

                return new Tuple<double, double>((double)lat, (double)lng);
            }
            catch {
                return null;
            };
            
            



        }

        public double[] Lats { get; set; }
        public double[] Longs { get; set; }
    }

    
}
