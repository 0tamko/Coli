using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.Database.Entities
{
    public class Configuration
    {
        public int Id { get; set; }
        public int CanBusSpeed { get; set; }
        public string Mode { get; set; }
        public string FilterSettings { get; set; }

    }
}
