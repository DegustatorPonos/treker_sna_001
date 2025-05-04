using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treker_sna_001
{
    public static class GlobalData
    {
        //public static string sh = "<add name="Kurs1Container" connectionString="metadata=res://*/Kurs1.csdl|res://*/Kurs1.ssdl|res://*/Kurs1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=dbsrv1;initial catalog=kursovaya_Kovylin;user id=Ист-3-23-30;password=KrovRom109;encrypt=False;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  
        public static string ad = "<!--Дом-->\n    <connectionStrings><add name=\"Kurs1Container\" connectionString=\"metadata=res://*/Kurs1.csdl|res://*/Kurs1.ssdl|res://*/Kurs1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.\\SQLEXPRESS;initial catalog=Kurs1;integrated security=True;encrypt=False;MultipleActiveResultSets=True;App=EntityFramework&quot;\" providerName=\"System.Data.EntityClient\" /></connectionStrings></configuration>";
        public static User user { get; set; }
    }
}
