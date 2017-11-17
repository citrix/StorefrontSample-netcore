using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

    public class CitrixApplicationInfo
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string AppTitle { get; set; }
        
        [JsonProperty("launchurl")]
        public string AppLaunchURL { get; set; }
        
        [JsonProperty("iconurl")]
        public string AppIconUrl { get; set; }
        
        [JsonProperty("description")]
        public string AppDesc { get; set; }

		public CitrixAuthCredential Auth { get; set; }
		
        public string StorefrontURL { get; set; }
        
        [JsonProperty("clienttypes")]
        public string[] ClientTypes { get; set; }
        
        [JsonProperty("content")]
        public string Content { get; set; }
    }

