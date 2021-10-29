using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{

    public partial class insertSamplingOperation : System.Web.UI.Page
    {
        private string connectionString = "Server=127.0.0.1;Port=5432;" + "Database=postgres;User ID=postgres;" + "Password=123456;" + "Pooling=false;" + "ConnectionLifeTime=10";

        private int _accessionId;
        private int accessionId
        {
            get { return _accessionId; }
            set { _accessionId = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idAccess = Request.QueryString["AccessionId"];
                accessionId = int.Parse(idAccess);
                getSamplingData();
            }
        }


        private void getSamplingData()
        {

            //ddl operations
            country c1 = new country();
            researcher re = new researcher();
            //operation o1 = new operation();

            ddlResearcher.DataSource = re.GetResearcher(connectionString, 0);
            ddlResearcher.DataValueField = "researcher_id";
            ddlResearcher.DataTextField = "nomeCognome";
            ddlResearcher.DataBind();
            ddlResearcher.Items.Add(new ListItem { Text = "--", Value = "0" });
            ddlResearcher.SelectedValue = "0";

            //OPERATION
            DataTable dtGeneticOp = c1.GetCountries(connectionString);
            ddlCountry.DataSource = dtGeneticOp;
            ddlCountry.DataValueField = "country_id";
            ddlCountry.DataTextField = "country_name";
            ddlCountry.DataBind();
                    }

        protected void btnInsertGenOp_Click(object sender, EventArgs e)
        {
            accessionId = int.Parse(Request.QueryString["AccessionId"]);
            //OPEATION HISTORY
            operation op1 = new operation();
            int researcher = int.Parse(ddlResearcher.SelectedValue);
            string opNote = txtnote.Text;
            string txtPathFile = pathFile.FileName;
            int sampling = int.Parse(ddlSamplingSite.SelectedValue);

            int user = 0;

            op1.pusamplingoperation(connectionString, accessionId, 0, sampling, user, opNote, dateOp.SelectedDate, researcher, txtPathFile); //,  user, opNote, txtPathFile, 0, genProgram, genPop, genPopText, genPopnote, father, mother,
                //field
                //, subfield, opFather, txtGen, 0, operationsSelected);
        }
              

        private void showMessage(string msg)
        {
            string script = "alert(\" " + msg + "\"); ";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sampling o1 = new Sampling();
            int idCountry = int.Parse( ddlCountry.SelectedValue);
            DataTable dt = o1.GetSamplingSites(connectionString, idCountry);
            ddlSamplingSite.DataSource = dt;
            ddlSamplingSite.DataValueField = "site_id";
            ddlSamplingSite.DataTextField = "location";
            ddlSamplingSite.DataBind();
        }
    }
}