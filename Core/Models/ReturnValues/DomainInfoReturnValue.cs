﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Core.Models
{
    public class DomainInfoReturnValue:ReturnValueBase
    {
        [JsonProperty("domain")]
        public DomainInfoModel Info { get; set; }
    }
}
