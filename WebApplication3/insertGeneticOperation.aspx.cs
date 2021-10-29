using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{

    public partial class insertOperation : System.Web.UI.Page
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
                GetGeneticOpData();
            }
        }


        private void GetGeneticOpData()
        {
           
            //ddl operations
            operation o1 = new operation();
            researcher re = new researcher();
            accession a1 = new accession();

            //list of all possible genetic opoerations to do
            ddlProg.DataSource = o1.GetGeneticProgram(connectionString);
            ddlProg.DataBind();
            ddlProg.Items.Add(new ListItem { Text = "--", Value = "0" });
            ddlProg.SelectedValue = "0";

            ddlResearcher.DataSource = re.GetResearcher(connectionString, 0);
            ddlResearcher.DataValueField = "researcher_id";
            ddlResearcher.DataTextField = "nomeCognome";
            ddlResearcher.DataBind();
            ddlResearcher.Items.Add(new ListItem { Text = "--", Value = "0" });
            ddlResearcher.SelectedValue = "0";

            //OPERATION
            DataTable dtGeneticOp = o1.GetGeneticOperations(connectionString);
            chkPossibleOp.DataSource = dtGeneticOp;
            chkPossibleOp.DataValueField = "operation_id";
            chkPossibleOp.DataTextField = "description";
            chkPossibleOp.DataValueField = "operation_id";
            chkPossibleOp.DataBind();

            DataTable dtGenPop = o1.GetGeneticPopulation(connectionString);
            ddlGenPopulation.DataSource = dtGenPop;
            ddlGenPopulation.DataBind();
            ddlGenPopulation.Items.Add(new ListItem { Text = "--", Value = "0" });

            DataTable dtAccessioFather = a1.GetAccessions(connectionString, accessionId);
            ddlAccessionMother.DataSource = dtAccessioFather;
            ddlAccessionMother.DataTextField = "description";
            ddlAccessionMother.DataValueField = "ACCESSION_ID";
            ddlAccessionMother.DataBind();
            ddlAccessionMother.Items.Add(new ListItem { Text = "--", Value = "0" });
            ddlAccessionMother.SelectedValue = "0";

            ddlAccessionFather.DataSource = dtAccessioFather;
            ddlAccessionFather.DataTextField = "description";
            ddlAccessionFather.DataValueField = "ACCESSION_ID";
            ddlAccessionFather.DataBind();
            ddlAccessionFather.Items.Add(new ListItem { Text = "--", Value = "0" });
            ddlAccessionFather.SelectedValue = "0";

            //list of genetic programs
            DataTable dtOperations = o1.GetFatherOperation(connectionString, accessionId);
            ddlFatherOp.DataSource = dtOperations;
            ddlFatherOp.DataTextField = "description";
            ddlFatherOp.DataValueField = "ACCESSION_ID";
            ddlFatherOp.DataBind();
            ddlFatherOp.Items.Add(new ListItem { Text = "--", Value = "0" });
            ddlFatherOp.SelectedValue = "0";

        }

        protected void btnInsertGenOp_Click(object sender, EventArgs e)
        {
            accessionId = int.Parse( Request.QueryString["AccessionId"]);
            //OPEATION HISTORY
            operation op1 = new operation();
            int genProgram = int.Parse(ddlProg.SelectedValue);
            int researcher = int.Parse(ddlResearcher.SelectedValue);
            int genPop = int.Parse(ddlGenPopulation.SelectedValue);
            string opNote = txtnote.Text;
            string txtPathFile = pathFile.FileName;
            string genPopText = txtGenPop.Text;
            string genPopnote = txtGenPopNote.Text;
            int father = int.Parse(ddlAccessionFather.SelectedValue);
            int mother = int.Parse(ddlAccessionMother.SelectedValue);
            int opFather = int.Parse(ddlFatherOp.SelectedValue);
            string field = txtField.Text;
            string subfield = txtSubField.Text;
            List<int> operationsSelected = new List<int>();
            string txtGen = txtGenealogy.Text;
            int user = 0;

            if (chkPossibleOp.Items.Count > 0)
            {
                foreach (ListItem item in chkPossibleOp.Items)
                {
                    if (item.Selected)
                    {
                        operationsSelected.Add(int.Parse(item.Value));
                    }
                }
            }

            op1.putgeneticoperation(connectionString, accessionId, researcher, dateOp.SelectedDate, user, opNote, txtPathFile, 0, genProgram, genPop,  genPopText, genPopnote, father, mother,
                field 
                , subfield, opFather, txtGen, 0, operationsSelected  );
        }

        protected void lstPossibleOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            operation o1 = new operation();
            int ChosenOperation = 0;
            if (chkPossibleOp.SelectedIndex > -1)
            {
                ChosenOperation = int.Parse(chkPossibleOp.Items[chkPossibleOp.SelectedIndex].Value);

                if (o1.GetGeneticOperations(connectionString).Select("operation_id = " + ChosenOperation + " AND singleOccurrence = True").Count() > 0)
                {
                    foreach (ListItem item in chkPossibleOp.Items)
                    {
                        if (item.Selected && item.Value != chkPossibleOp.Items[chkPossibleOp.SelectedIndex].Value)
                        {
                            chkPossibleOp.Items[chkPossibleOp.SelectedIndex].Selected = false;
                            showMessage("Scelta multipla possibile sollo nel caso dei crossing");
                        }
                    }
                    
                }
            }
        }

        private void showMessage(string msg)
        {
            string script = "alert(\" "+ msg + "\"); ";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
    }
}