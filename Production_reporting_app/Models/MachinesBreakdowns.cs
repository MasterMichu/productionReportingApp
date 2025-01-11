using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    public class MachinesBreakdowns
    {
        public machinesInLine Machine { get; set; }
        public string breakdowndetails { get; set; }
        public Categories nazwakategorii {get;set;}
    }
    
}
