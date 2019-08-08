using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Ratel.Web.Models
{
    public class TestConfig
    {
        [JsonProperty("browser")]
        public string Browser { get; set; } = "chrome";

        [JsonProperty("isSynchronization")]
        public bool Synchronization { get; set; } = true;

        [JsonProperty("isAngular")]
        public bool Angular { get; set; }

        [JsonIgnore]
        public List<Type> IgnoredExceptions { get; set; } = new List<Type>

        {
            typeof(StaleElementReferenceException),
            typeof(ElementClickInterceptedException),
            typeof(NoSuchElementException)
        };

        [JsonProperty("timeoutMilliseconds")]
        public int TimeoutMilliseconds { get; set; } = 10000;

        [JsonProperty("waitIntervalMilliseconds")]
        public int WaitIntervalMilliseconds { get; set; } = 100;

        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; } = string.Empty;

        [JsonProperty("urls")]
        public Dictionary<string, string> Urls { get; set; } = new Dictionary<string, string>();


        [JsonProperty("chromeOptions")]
        public ChromeOptionsConfig ChromeOptionsConfig { get; set; } = new ChromeOptionsConfig();

        [JsonIgnore]
        public ChromeOptions ChromeOptions{ get; set; }
    }
}
