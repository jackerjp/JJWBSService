using Dos.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JJWBSService
{
    public class DBService
    {
        public static readonly DbSession Context = new DbSession("YY");
        public static readonly DbSession Old = new DbSession("OldYY");
        public static readonly DbSession JJ = new DbSession("EMS");
        public static DbSession My = new DbSession(DatabaseType.MySql, "Database=jybim;Data Source=218.94.73.106;Port=8306;User Id=JJSDBASE;Password=JJSDBASE");// "Data Source=218.94.73.106:8306;Initial Catalog=jybim;User ID=JJSDBASE;Pwd=JJSDBASE;");
    }
}