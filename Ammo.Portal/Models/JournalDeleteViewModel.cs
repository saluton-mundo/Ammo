using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ammo.Portal.Models
{
    public class JournalDeleteViewModel
    {
        [JsonProperty("JournalId")]
        public int JournalId { get; set; }
    }
}