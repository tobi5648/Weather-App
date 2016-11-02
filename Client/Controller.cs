using Entities;
using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Client
{
    public class Controller
    {
        //http://www.codeproject.com/Articles/630248/WPF-OpenWeather
        const string sUrl = "http://api.openweathermap.org/data/2.5/weather?";
        const string APIKEY = "a8a4fc02d1b05081906a085d971ab526";
        const string mode = "xml";
        string temp = string.Empty;

        public WeatherData GetTemperatureFor (string city)
        {
            WeatherData data = new WeatherData();
            string temp = string.Empty;
            string url = $"{sUrl}q={city}&mode={mode}&APPID={APIKEY}";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //request.ContentType = "application/xaml";

            if (!string.IsNullOrWhiteSpace(city))
            {

                data.City = city;

                using (WebClient wc = new WebClient())
                {
                    temp = wc.DownloadString(url);
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(temp);
                XmlNodeList nodeList = doc.SelectNodes("descendant::temperature");
                temp = string.Empty;
                double temperature;
                foreach (XmlElement tempe in nodeList)
                {
                    if (tempe.GetAttribute("unit") == "kelvin")
                    {
                        temp = tempe.GetAttribute("value");
                        temp = temp.Replace(".", ",");
                        temperature = Convert.ToDouble(temp);
                        temperature += -273.15;
                        data.Temperature = temperature;
                    }
                    else
                    {
                        temp = tempe.GetAttribute("value");
                        temp = temp.Replace(".", ",");
                        temperature = Convert.ToDouble(temp);
                        data.Temperature = temperature;
                    }
                }
                                
                #region Something
                //// http://stackoverflow.com/questions/7496913/how-to-load-xml-from-url-on-xmldocument
                #endregion
            }
            return data;
        }

        public Controller()
        {
            //url = $"http://api.openweathermap.org/data/2.5/weather?";
        }
    }
}
