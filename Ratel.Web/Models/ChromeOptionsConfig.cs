using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ratel.Web.Models
{
    public class ChromeOptionsConfig
    {

        [JsonProperty("binaryLocation")]
        public string BinaryLocation { get; set; } = string.Empty;

        [JsonProperty("arguments")]
        public List<string> Arguments { get; set; } = new List<string>();

        [JsonProperty("localStatePreference")]
        public Dictionary<string, string> LocalStatePreference { get; set; } = new Dictionary<string, string>();

        [JsonProperty("userProfilePreference")]
        public Dictionary<string, string> UserProfilePreference { get; set; } = new Dictionary<string, string>();

        [JsonProperty("additionalCapability")]
        public Dictionary<string, string> AdditionalCapability { get; set; } = new Dictionary<string, string>();
    }
}
