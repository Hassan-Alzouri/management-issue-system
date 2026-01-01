using hassan11244WebApp1.App_Code;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hassan11244WebApp1.demo
{
    public partial class patient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!Page.IsPostBack)
            {
                populdateDdlCountry();
            }


        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void populdateDdlCountry()
        {
            CRUD myCrud = new CRUD();
            String mySql = @"select countryId,country from country";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlCountry.DataValueField = "countryId";
            ddlCountry.DataTextField = "country";
            ddlCountry.DataSource = dr;
            ddlCountry.DataBind();


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //server side valiadation
            if (String.IsNullOrEmpty(textFName.Text))
            {
                lblOutput.Text = "Please fill First Name field!";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                textFName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(textLName.Text))
            {
                lblOutput.Text = "Please fill Last Name field!";
                lblOutput.ForeColor = System.Drawing.Color.Red;
                textLName.Focus();
                return;
            }
            CRUD myCrud = new CRUD();
            String mySql = @"insert patient (fName,lName,dob,active,CountryId)
                         values(@fName,@lName,@dob,@active,@CountryId)";
            Dictionary<String, Object> myPara = new Dictionary<String, Object>();
            myPara.Add("@fName", textFName.Text);
            myPara.Add("@lName", textLName.Text);
            myPara.Add("@dob", textDob.Text);
            myPara.Add("@active", cbActive.Checked);
            myPara.Add("@CountryId", ddlCountry.SelectedValue);
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            { lblOutput.Text = "Operation Successful!"; }
            else
            {
                lblOutput.Text = "Operation Failed!";
            }

            poppulateGvPatient();
        }

        protected void btnGetPatientInfo_Click(object sender, EventArgs e)
        {
            poppulateGvPatient();
        }

        protected void poppulateGvPatient()
        {
            CRUD myCrud = new CRUD();
            String mySql = @"select * from v_patientInfo";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvPatientInfo.DataSource = dr;
            gvPatientInfo.DataBind();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            String mySql = @"delete patient where patientId = @patientId";
            Dictionary<String, Object> myPara = new Dictionary<String, Object>();
            myPara.Add("@patientId", textPatientId.Text);
           
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            { lblOutput.Text = "Operation Successful!"; }
            else
            {
                lblOutput.Text = "Operation Failed!";
            }

            poppulateGvPatient();
        }
        
         protected void btnUpdate_Click(object sender, EventArgs e)
        {
            CRUD myCrud = new CRUD();
            String mySql = @"update patient
                                set fName = @fName,lName =@lName ,dob = @dob,active =@active,CountryId=@CountryId
                                where patientId = @patientId)";
            Dictionary<String, Object> myPara = new Dictionary<String, Object>();
            myPara.Add("@patientId",int.Parse( textPatientId.Text)); //wxplicit conversion from string to int
            myPara.Add("@fName", textFName.Text);
            myPara.Add("@lName", textLName.Text);
            myPara.Add("@dob", textDob.Text);
            myPara.Add("@active", cbActive.Checked);
            myPara.Add("@CountryId", ddlCountry.SelectedValue);
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            if (rtn >= 1)
            { lblOutput.Text = "Operation Successful!"; }
            else
            {
                lblOutput.Text = "Operation Failed!";
            }

            poppulateGvPatient();
        }
       
         protected void btnClear_Click(object sender, EventArgs e)
        {
            textPatientId.Text = "";
            textFName.Text = "";
            textLName.Text = "";
            textDob.Text = "";
            cbActive.Checked = false;
        }

        protected void populateForm_Click(object sender, EventArgs e)
        {
            int PK = int.Parse((sender as LinkButton).CommandArgument);
            //lblOuput.Text = PK.ToString();

            string mySql = @"select patientId,fName,lName,dob,active,CountryId
                                from patient
                                Where patientId=@patientId";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@patientId", PK);
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        String patientId = dr["patientId"].ToString();
                        String fName = dr["fName"].ToString();
                        String lName = dr["lName"].ToString();
                        String dob = dr["dob"].ToString();
                        bool active =bool.Parse( dr["active"].ToString());
                        String CountryId = dr["CountryId"].ToString();
                        //lblOuput.Text = empId + employee+ depId;
                        textPatientId.Text = patientId;
                        textFName.Text = fName;
                        textLName.Text = lName;
                        textDob.Text = dob;
                        cbActive.Checked = active;
                        ddlCountry.SelectedValue = CountryId;
                    }
                }
            }
        }

        public static void ExportGridToExcel(GridView myGv) // working 1
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Charset = "";
            string FileName = "ExportedReport_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            myGv.GridLines = GridLines.Both;
            myGv.HeaderStyle.Font.Bold = true;
            myGv.RenderControl(htmltextwrtter);
            HttpContext.Current.Response.Write(strwritter.ToString());
            HttpContext.Current.Response.End();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(gvPatientInfo);
        }



    }//cls
}//ns