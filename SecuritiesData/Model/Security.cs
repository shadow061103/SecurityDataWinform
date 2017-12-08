using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritiesData.Model
{
   

        public class Security
        {
            public string stat { get; set; }
            public string date { get; set; }
            public string title { get; set; }
            public string[] fields { get; set; }
            public object[][] data { get; set; }
            public string selectType { get; set; }
            public string[] notes { get; set; }
        }

    
}
