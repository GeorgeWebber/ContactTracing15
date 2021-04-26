using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ContactTracing15.Pages.GovAgent
{
    [Authorize(Policy = "GovAgentOnly")]
    public class HeatmapPageModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ICaseService _caseService;
        public string googleUrl { get; }
        public double[] Lats { get; set; }
        public double[] Longs { get; set; }

        [BindProperty, Required, Display(Name = "Date from")]
        public DateTime DateFrom { get; set; }
        [BindProperty, Required]
        public DateTime DateTo { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostDateButton()
        {
            Console.WriteLine("Posting here");
            Console.WriteLine(DateFrom);
            SetUpLocationArrays();
            if (ModelState.IsValid)
            {
                Console.WriteLine(DateFrom);
                return Page();
            }
            return Page();
        }




        public HeatmapPageModel(IConfiguration config, ICaseService caseService)
        {
            _config = config;
            _caseService = caseService;
            googleUrl = "https://maps.googleapis.com/maps/api/js?v=3.exp&key=" + _config["googleApiKey"] + "&libraries=visualization";
            DateFrom = DateTime.Now.AddDays(-10);
            DateTo = DateTime.Now;
            SetUpLocationArrays();
        }

        public void SetUpLocationArrays()
        {
            IEnumerable<string> Postcodes = _caseService.GetPostcodesByRecentDays(DateFrom, DateTo);

            double[] latArray = new double[Postcodes.Count()];
            double[] longArray = new double[Postcodes.Count()];

            int i = 0;
            foreach (string postcode in Postcodes)
            {
                Tuple<double, double> location = GetLocation(postcode);
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

            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&components=country:GB|postal_code:{0}&sensor=false", Uri.EscapeDataString(postcode), _config["googleApiKey"]);

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement result = xdoc.Element("GeocodeResponse").Element("result");

            if (result != null)
            {
                XElement locationElement = result.Element("geometry").Element("location");
                XElement lat = locationElement.Element("lat");
                XElement lng = locationElement.Element("lng");

                return new Tuple<double, double>((double)lat, (double)lng);
            }
            else
            {
                return null;
            }
        }
    }
}
