using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Xml.Linq;

namespace ContactTracing15.Helper
{
    public static class PostcodeValidator
    {
        public static bool IsValid(string postcode, string apiKey)
        {
            if (postcode == null)
            {
                return false;
            }

            bool result = false;

            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&components=country:GB|postal_code:{0}&sensor=false", Uri.EscapeDataString(postcode), apiKey);

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            XElement responseResult = xdoc.Element("GeocodeResponse").Element("result");

            if (responseResult != null)
            {
                result = true;
            }
            return result;
        }

        public static string FormatErrorMessage()
        {
            return String.Format(CultureInfo.CurrentCulture,
              "That is not a registered UK postcode, please check and try again");
        }
    }

    
}

    
