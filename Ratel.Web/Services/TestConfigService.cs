using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Ratel.Web.Models;

namespace Ratel.Web.Services
{
    public class TestConfigService
    {
        private static string ConfigPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestConfig.json");

        public static TestConfig GetConfig()
        {
            if (File.Exists(ConfigPath))
            {
                return JsonConvert.DeserializeObject<TestConfig>(File.ReadAllText(ConfigPath));
            }

            var config = new TestConfig();
            using (var sw = File.CreateText(ConfigPath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(sw, config);
            }
            return config;
        }
    }
}
