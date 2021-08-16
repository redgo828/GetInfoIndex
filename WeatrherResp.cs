using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetInfoIndex
{
    public class WeatrherResp
    {
        public Temperature Main { get; set; }
        public string Name { get; set; }
        public Coordinate Coord { get; set; }
    }
}
