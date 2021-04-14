using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactTracing15.Pages.GovAgent
{

    [Authorize(Policy = "GovAgentOnly")]
    public class GovHomeModel : PageModel
    {
        public void OnGet()
        {
            var Postcodes = new string[] {"OX1", "OX2"};

            Lats = new double[Postcodes.Count()];
            Longs = new double[Postcodes.Count()];

            int i = 0;
            foreach (string postcode in Postcodes)
            {
                Tuple<double, double> location = GetLocation(postcode);
                Lats[i] = location.Item1;
                Longs[i] = location.Item2;
                i++;
            }

        }

        public Tuple<double, double> GetLocation(string postcode)
        {
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(postcode), "AIzaSyAtclzZCimV0CJog5ReUNG3rsL3fXL-o_8");
            
            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");
            XElement locationElement = result.Element("geometry").Element("location");
            XElement lat = locationElement.Element("lat");
            XElement lng = locationElement.Element("lng");

            Console.WriteLine(lat);

            return new Tuple<double, double>((double) lat, (double) lng);

        }

        public double[] Lats { get; set; }
        public double[] Longs { get; set; }
    }

    
}
