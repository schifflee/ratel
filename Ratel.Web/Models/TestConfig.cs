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

        [JsonProperty("alwaysSearch")]
        public bool AlwaysSearch { get; set; }

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

        [JsonProperty("users")]
        public List<UserModel> Users { get; set; } = new List<UserModel>();

        [JsonProperty("chromeOptions")]
        public ChromeOptionsConfig ChromeOptionsConfig { get; set; } = new ChromeOptionsConfig();

        [JsonIgnore]
        public ChromeOptions ChromeOptions{ get; set; }

        public UserModel GetUser(object identity, UserIdentityType identityType = UserIdentityType.Username)
        {
            List<UserModel> matchedUsers = new List<UserModel>();

            switch (identityType)
            {
                case UserIdentityType.Email:
                    matchedUsers = Users.Where(x => x.Email == (string) identity).ToList();
                    break;
                case UserIdentityType.Role:
                    matchedUsers = Users.Where(x => x.Role == identity).ToList();
                    break;
                case UserIdentityType.Username:
                    matchedUsers = Users.Where(x => x.Username == (string)identity).ToList();
                    break;
            }

            if (matchedUsers.Count == 0)
            {
                throw new ArgumentOutOfRangeException($"Can't find User by {identityType} {identity}");
            }

            if (matchedUsers.Count > 1)
            {
                throw new ArgumentOutOfRangeException($"There is more than one use with {identityType} {identity}");
            }
            return matchedUsers.First();
        }

        public void SwitchBaseUrl(string key)
        {
            var matchedUrls = Urls.Where(x => x.Key == key).ToList();
            switch (matchedUrls.Count)
            {
                case 1:
                    BaseUrl = matchedUrls.First().Value;
                    break;
                case 0:
                    throw new ArgumentOutOfRangeException($"Can't find url by key {key}");
                default:
                    throw new ArgumentOutOfRangeException($"There is more than one url with the same key: {key} Count: {matchedUrls.Count}");
            }
        }
    }
}
