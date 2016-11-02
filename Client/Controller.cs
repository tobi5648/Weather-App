namespace Client
{
    #region Usings
    using Entities;
    using System;
    using System.Net;
    using System.Xml; 
    #endregion

    public class Controller
    {
        #region Links
        //// http://stackoverflow.com/questions/7496913/how-to-load-xml-from-url-on-xmldocument
        //http://www.codeproject.com/Articles/630248/WPF-OpenWeather
        #endregion

        #region Constants
        /// <summary>
        /// The Url used to check for the weather.
        /// </summary>
        const string sUrl = "http://api.openweathermap.org/data/2.5/weather?";
        /// <summary>
        /// The Apikey given.
        /// </summary>
        const string APIKEY = "a8a4fc02d1b05081906a085d971ab526";
        /// <summary>
        /// The type, the returned is read in.
        /// </summary>
        const string mode = "xml";
        #endregion

        #region Fields
        /// <summary>
        /// A string used to store the url, and the temperature.
        /// </summary>
        string temp = string.Empty;
        #endregion

        #region Methods
        /// <summary>
        /// It Comtacts the URI and gets the temperature for the given city.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public WeatherData GetTemperatureFor(string city)
        {
            WeatherData data = new WeatherData();
            string temp = string.Empty;
            string url = $"{sUrl}q={city}&mode={mode}&APPID={APIKEY}";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //request.ContentType = "application/xaml";

            if (!string.IsNullOrWhiteSpace(city))
            {
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
                        temperature = Math.Round(temperature, 0, MidpointRounding.AwayFromZero);
                        data.Temperature = temperature;
                    }
                    else
                    {
                        temp = tempe.GetAttribute("value");
                        temp = temp.Replace(".", ",");
                        temperature = Convert.ToDouble(temp);
                        temperature = Math.Round(temperature);
                        data.Temperature = temperature;
                    }
                }


            }
            return data;
        } 
        #endregion

        #region Controllers
        /// <summary>
        /// A controller for the class
        /// </summary>
        public Controller()
        {
            //url = $"http://api.openweathermap.org/data/2.5/weather?";
        } 
        #endregion
    }
}
