using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class _Default : Page
    {
        public string connectionString = "Server=127.0.0.1;Port=5432;" + "Database=postgres;" +
            "User ID=postgres;" + "Password=123456;" + "Pooling=false;" + "ConnectionLifeTime=10"; //"Data Source=(local);Initial Catalog=Northwind;Integrated Security=true";

        protected void Page_Load(object sender, EventArgs e)
        {
            Class1 c1 = new Class1();
            c1.GetData(0, connectionString);
            c1.putAccession(connectionString, 11, 1);
        }
    }
}