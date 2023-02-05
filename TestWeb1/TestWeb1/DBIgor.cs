using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWeb1
{
    public class DBIgor
    {
        public MySqlConnection cn;
        public void Connect()
        {
            cn = new MySqlConnection("Datasource=127.0.0.1;username=root;password=;database=test");
        }
    }
}