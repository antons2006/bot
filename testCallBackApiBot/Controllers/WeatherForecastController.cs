using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCallBackApiBot.Controllers
{
    [Serializable]
    public class Updates
    {

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("object")]
        public JObject Object { get; set; }
        [JsonProperty("group_id")]
        public long GroupId
        {
            get; set;
        }
    }

   
}
