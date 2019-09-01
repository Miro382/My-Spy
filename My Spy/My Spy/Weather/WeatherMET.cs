using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;


/*
 * 
 * Miroslav Murin
 * http://api.met.no/license_data.html
 * 
 */


namespace CSharp_Weather
{
    public class WeatherMET
    {
        public const int Kelvin = 1, Celsius = 0, Fahrenheit = 2;

        public WeatherInfo weatherinfo = new WeatherInfo();


        /// <summary>
        /// Read weather data from The Norwegian Meteorological Institute
        /// </summary>
        /// <param name="latitude">Latitude of your place</param>
        /// <param name="longtitude">Longtitude of your place</</param>
        /// <returns>Returns true if successful</returns>
        public bool GetWeatherData(float latitude, float longtitude)
        {
            try
            {
                string xml = HelpClass.GETHtml("https://api.met.no/weatherapi/locationforecastlts/1.3/?lat=" + latitude + ";lon=" + longtitude);

                weatherinfo.CityLat = "" + latitude;
                weatherinfo.CityLon = "" + longtitude;

                if (ParseXML(xml))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                return false;
            }
        }


        /// <summary>
        /// Read weather data from The Norwegian Meteorological Institute
        /// </summary>
        /// <param name="latitude">Latitude of your place</param>
        /// <param name="longtitude">Longtitude of your place</</param>
        /// <returns>Returns true if successful</returns>
        public bool GetWeatherData(string latitude, string longtitude)
        {
            try
            {
                string xml = HelpClass.GETHtml("https://api.met.no/weatherapi/locationforecastlts/1.3/?lat=" + latitude + ";lon=" + longtitude);

                weatherinfo.CityLat = latitude;
                weatherinfo.CityLon = longtitude;

                if (ParseXML(xml))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("" + ex);
                return false;
            }
        }

        /// <summary>
        /// Converts Celsius to Kelvin
        /// </summary>
        public float CelsiusToKelvin(float Celsius)
        {
            return Celsius + (float)273.15;
        }

        /// <summary>
        /// Converts Celsius to Fahrenheit
        /// </summary>
        public float CelsiusToFahrenheit(float Celsius)
        {
            return (Celsius * 9) / 5 + 32;
        }


        /// <summary>
        /// Returns temperature in specific units
        /// </summary>
        /// <param name="Unit">0 Celsius; 1 Kelvin; 2 Fahrenheit</param>
        public string GetTemperatureInUnits(int Unit)
        {
            if (Unit == 1)
                return CelsiusToKelvin(float.Parse(weatherinfo.Temperature.Replace('.', ',')))+ " K";
            else if (Unit == 2)
                return CelsiusToFahrenheit(float.Parse(weatherinfo.Temperature.Replace('.', ',')))+ " °F";
            else
                return weatherinfo.Temperature + " °C";
        }



        private bool ParseXML(string XML)
        {
            try
            {
                if (!string.IsNullOrEmpty(XML))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(XML);

                    XmlNode node = doc.DocumentElement.SelectNodes("/weatherdata/product/time")[0].SelectSingleNode("location");
                    XmlNode node2 = doc.DocumentElement.SelectNodes("/weatherdata/product/time")[1].SelectSingleNode("location");

                    weatherinfo.Temperature = node.SelectSingleNode("temperature").Attributes["value"].Value;
                    weatherinfo.TemperatureUnit = node.SelectSingleNode("temperature").Attributes["unit"].Value;
                    weatherinfo.Cloudiness = node.SelectSingleNode("cloudiness").Attributes["percent"].Value;
                    weatherinfo.Humidity = node.SelectSingleNode("humidity").Attributes["value"].Value;
                    weatherinfo.HumidityUnit = node.SelectSingleNode("humidity").Attributes["unit"].Value;
                    weatherinfo.Pressure = node.SelectSingleNode("pressure").Attributes["value"].Value;
                    weatherinfo.PressureUnit = node.SelectSingleNode("pressure").Attributes["unit"].Value;
                    weatherinfo.WindDirection = node.SelectSingleNode("windDirection").Attributes["deg"].Value;
                    weatherinfo.WindDirectionName = node.SelectSingleNode("windDirection").Attributes["name"].Value;
                    weatherinfo.WindSpeed = node.SelectSingleNode("windSpeed").Attributes["mps"].Value;
                    weatherinfo.Fog = node.SelectSingleNode("fog").Attributes["percent"].Value;

                    weatherinfo.Precipitation = node2.SelectSingleNode("precipitation").Attributes["value"].Value;
                    weatherinfo.Symbolnumber = node2.SelectSingleNode("symbol").Attributes["number"].Value;

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


        /// <summary>
        /// Get current weather icon
        /// </summary>
        /// <param name="night">0 is day; 1 is night</param>
        /// <returns>Returns Image</returns>
        public Bitmap GetIcon(int night)
        {
            System.Net.WebRequest request =
            System.Net.WebRequest.Create("https://api.met.no/weatherapi/weathericon/1.1/?symbol=" + weatherinfo.Symbolnumber + ";is_night=" + night + ";content_type=image/png");
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            return new Bitmap(responseStream);
        }


    }
}
