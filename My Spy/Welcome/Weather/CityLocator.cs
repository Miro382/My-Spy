using My_Spy_Administrator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

/*
 * Miroslav Murin
 * City Locator
 * http://wiki.openstreetmap.org/wiki/Nominatim
 * 
 */


namespace CSharp_Weather
{
    public class CityLocator
    {

        public string attribution = "";
        public string PlaceDisplayName = "", Latitude = "", Longtitude = "";
        public string CountryCode = "",Country="",City="",City_District="", Village = "",State = "";

        public bool GetGeoCoordByCityName(string CityName)
        {
            try
            {
              string xml = HelpClass.GETHtml("http://nominatim.openstreetmap.org/search?addressdetails=0&city=" + CityName+ "&addressdetails=1&format=xml");

                if (ParseXML(xml))
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                Debug.Write(""+ex);
                return false;
            }
        }


        public bool GetGeoCoordByCityNameEmail(string CityName,string Email)
        {
            try
            {
                string xml = HelpClass.GETHtml("http://nominatim.openstreetmap.org/search?addressdetails=0&city=" + CityName + "&addressdetails=1&format=xml&email=" + Email);

                if (ParseXML(xml))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.Write("" + ex);
                return false;
            }
        }


        public bool GetGeoCoordByCityNameEmail(string CityName,string Country, string Email)
        {
            try
            {
                string xml = HelpClass.GETHtml("http://nominatim.openstreetmap.org/search?addressdetails=0&city=" + CityName + "&country="+Country + "&addressdetails=1&format=xml&email=" + Email);

                if (ParseXML(xml))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.Write("" + ex);
                return false;
            }
        }



        public bool GetGeoCoordByCityName(string CityName, string Country)
        {
            try
            {
                string xml = HelpClass.GETHtml("http://nominatim.openstreetmap.org/search?addressdetails=0&city=" + CityName + "&country=" + Country + "&addressdetails=1&format=xml");

                if (ParseXML(xml))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.Write("" + ex);
                return false;
            }
        }


        private bool ParseXML(string XML)
        {
            try
            {
                if (!string.IsNullOrEmpty(XML))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(XML);

                    XmlNodeList nodes = doc.DocumentElement.SelectNodes("/searchresults");
                    XmlNodeList nodes2 = doc.DocumentElement.SelectNodes("/searchresults/place");

                    attribution = nodes[0].Attributes["attribution"].Value;
                    PlaceDisplayName = nodes2[0].Attributes["display_name"].Value;
                    Latitude = nodes2[0].Attributes["lat"].Value;
                    Longtitude = nodes2[0].Attributes["lon"].Value;

                    if (nodes2[0].SelectSingleNode("country_code") != null)
                        CountryCode = nodes2[0].SelectSingleNode("country_code").InnerText;
                    if (nodes2[0].SelectSingleNode("country") != null)
                        Country = nodes2[0].SelectSingleNode("country").InnerText;
                    if(nodes2[0].SelectSingleNode("city")!=null)
                        City = nodes2[0].SelectSingleNode("city").InnerText;
                    if (nodes2[0].SelectSingleNode("city_district") != null)
                        City_District = nodes2[0].SelectSingleNode("city_district").InnerText;
                    if (nodes2[0].SelectSingleNode("village") != null)
                        Village = nodes2[0].SelectSingleNode("village").InnerText;
                    if (nodes2[0].SelectSingleNode("state") != null)
                        State = nodes2[0].SelectSingleNode("state").InnerText;

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                return false;
            }
        }




        ///<summary>
        ///Return Country English name
        ///</summary>
        public string GetCountry()
        {
            string culture = CultureInfo.CurrentCulture.EnglishName;
            return culture.Substring(culture.IndexOf('(') + 1, culture.LastIndexOf(')') - culture.IndexOf('(') - 1);
        }



    }
}
