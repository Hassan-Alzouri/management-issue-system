using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using hassan11244WebApp1.App_Code;
using System.IO;

namespace hassan11244WebApp1.employee
{
    public partial class myAssignedRequests : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["hassan11244ConStr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (!IsPostBack)
            {
                BindEmployeeRequests();
                RepeterData();

            }
        }
        private void BindEmployeeRequests()
        {
            
            CRUD myCrud = new CRUD();
            
            string sql = @"SELECT TA.TaskID, SR.SubmitRequestID, SR.Description, RT.TypeName AS RequestType, 
                                  SR.DateTime, TA.IsActive
                                FROM TaskAssignment TA
                                JOIN SubmitRequest SR ON TA.SubmitRequestID = SR.SubmitRequestID
                                JOIN RequestType RT ON SR.RequestTypeID = RT.RequestTypeID
        
                            WHERE TA.AssignedToEmployeeID = 7 ";


            Dictionary<string, object> parameters = new Dictionary<string, object>();
            
            parameters.Add("@EmployeeID", 7);

            DataTable dt = myCrud.getDt(sql, parameters);
            rptRequests.DataSource = dt;
            rptRequests.DataBind();

            

        }

        protected void btnSubmit_click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                cmd = new SqlCommand("insert into Comment (UserName,Subject,CommentOn,Post_Date) values(@userName,@subject,@comment,@date)", con);
                cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = txtName.Text.ToString();
                cmd.Parameters.Add("@subject", SqlDbType.NVarChar).Value = txtSubject.Text.ToString();
                cmd.Parameters.Add("@comment", SqlDbType.NVarChar).Value = txtComment.Text.ToString();
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.ExecuteNonQuery();
                con.Close();
                txtName.Text = string.Empty;
                txtSubject.Text = string.Empty;
                txtComment.Text = string.Empty;
                RepeterData();
            }
            catch (Exception ex)
            {
                txtComment.Text = ex.Message;
            }
        }
        public void RepeterData()
        {
            con.Open();
            cmd = new SqlCommand("Select * from Comment Order By Post_Date desc", con);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            RepterDetails.DataSource = ds;
            RepterDetails.DataBind();
        }
        protected void cbActivate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)chk.NamingContainer;

            HiddenField hfTaskID = (HiddenField)item.FindControl("hfTaskID");

            if (hfTaskID == null)
            {
                lblOutputActive.Text = "Error: TaskID not found.";
                return;
            }

            int taskId;
            if (!int.TryParse(hfTaskID.Value, out taskId))
            {
                lblOutputActive.Text = "Error: Invalid TaskID.";
                return;
            }

            CRUD myCrud = new CRUD();
            string sql = "UPDATE TaskAssignment SET IsActive = @IsActive WHERE TaskID = @TaskID";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@TaskID", taskId);
            parameters.Add("@IsActive", chk.Checked);

            int rowsAffected = myCrud.ExecuteNonQuery(sql, parameters);

            if (rowsAffected > 0)
            {
                lblOutputActive.Text = "Task status updated successfully.";
            }
            else
            {
                lblOutputActive.Text = "Update failed.";
            }
        }

        protected void rptRequests_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox cbActivate = (CheckBox)e.Item.FindControl("cbActivate");
                if (cbActivate != null)
                {
                    cbActivate.Attributes.Add("onclick", "return confirm('Are you sure you want to change the status?');");
                }
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportRepeaterToExcel(rptRequests);
        }

        public void ExportRepeaterToExcel(Repeater repeater)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Requests_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            HttpContext.Current.Response.Charset = "";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            repeater.RenderControl(hw);

            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        
        public override void VerifyRenderingInServerForm(Control control)
        {
            // This confirms controls like Repeater can render during export
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtSubject.Text = "";
            txtComment.Text = "";
            
            
        }
    }//cls
}//ns