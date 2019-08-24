using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ratel.Web.Models
{
    public class UserModel
    {
        [JsonProperty("email")]
        public string Email { get; set; } = "";

        [JsonProperty("password")]
        public string Password { get; set; } = "";

        [JsonProperty("username")]
        public string Username { get; set; } = "";

        [JsonProperty("role")]
        public object Role { get; set; } = "";
    }

    public enum UserIdentityType
    {
        Email, Username, Role
    }
}
