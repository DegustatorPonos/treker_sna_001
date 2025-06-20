using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treker_sna_001
{
    public partial class WakeUpper
    {
        public string FormatDate
        {
            get
            {
                string fd = dateTime.ToString("HH:mm - dd MMMM");
                return fd;
            }
        }
    }
}
