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
                string fd = dateTime.ToString("dd MMMM");
                return fd;
            }
        }

        public string FormatTime
        {
            get
            {
                string fd = dateTime.ToString("HH:mm");
                return fd;
            }
        }
        
        public string FormatOcassion
        {
            get
            {
                string fd;
                if(Occasion == "Пробуждение")
                {
                    fd = "";
                    return fd;
                }
                else if(Occasion == "Засыпание")
                {
                    fd = "";
                    return fd;
                }
                else
                {
                    fd = "Другая";
                    return fd;
                }
            }
        }
    }
}
