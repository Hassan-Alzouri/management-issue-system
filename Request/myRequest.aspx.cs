using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
//using System.Data.SqlClient;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hassan11244WebApp1.App_Code;

namespace hassan11244WebApp1.Request
{
    public partial class myRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
    
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void ShowInfo()
        { 
        
        }
        protected void getData( string myId)
        {
           
            myAssistant(myId);
        }
        protected void myAssistant(string myId)
        {
            lblOutput.Text = myId.ToString();
        }
    

       
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTypeName.Text = "";
            txtPriority.Text = "";
            txtLocation.Text = "";
            txtDate.Text = "";
            txtDescripation.Text = "";
            lblOutput.Visible = false;
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            showData();
        }

        protected void showData()
        {
           
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT * FROM vw_UserMyRequests;";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            gvShowInternData.DataSource = dr;
            gvShowInternData.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblOutput.Visible = false;

            CRUD myCrud = new CRUD();
            string mySql = @"
                                DELETE FROM TaskAssignment WHERE SubmitRequestID = @SubmitRequestID;
                                DELETE FROM SubmitRequest_Equipment WHERE SubmitRequestID = @SubmitRequestID;
                                DELETE FROM SubmitRequest WHERE SubmitRequestID = @SubmitRequestID;";

            int submitRequestId;
            if (int.TryParse(txtSubmitRequestId.Text.Trim(), out submitRequestId))
            {
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@SubmitRequestID", submitRequestId);

                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                if (rtn >= 1)
                {
                    lblOutput.Text = "Operation Success!";
                }
                else
                {
                    lblOutput.Text = "Operation Failed!";
                }
                showData();
            }
            else
            {
                lblOutput.Text = "Invalid ID! Please enter a valid number.";
            }
            lblOutput.Visible = true;  
            showData();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            lblOutput.Visible = false;
            try
            {
                CRUD myCrud = new CRUD();

               
                string getRequestTypeIdSql = "SELECT RequestTypeID FROM RequestType WHERE TypeName = @TypeName";
                Dictionary<string, object> getRequestTypePara = new Dictionary<string, object>();
                getRequestTypePara.Add("@TypeName", txtTypeName.Text);

                object requestTypeResult = myCrud.ExecuteScalar(getRequestTypeIdSql, getRequestTypePara);
                if (requestTypeResult == null)
                {
                    lblOutput.Text = "❌ Request Type not found. Please enter a valid type name.";
                    lblOutput.Visible = true;
                    return;
                }
                int requestTypeId = Convert.ToInt32(requestTypeResult);

               
                string getPriorityIdSql = "SELECT PriorityID FROM Priority WHERE PriorityName = @PriorityName";
                Dictionary<string, object> getPriorityPara = new Dictionary<string, object>();
                getPriorityPara.Add("@PriorityName", txtPriority.Text);

                object priorityResult = myCrud.ExecuteScalar(getPriorityIdSql, getPriorityPara);
                if (priorityResult == null)
                {
                    lblOutput.Text = "❌ Priority not found. Please enter a valid priority.";
                    lblOutput.Visible = true;
                    return;
                }
                int priorityId = Convert.ToInt32(priorityResult);

                
                string[] locationParts = txtLocation.Text.Split('-');
                if (locationParts.Length != 3)
                {
                    lblOutput.Text = "❌ Invalid location format. Use format: Department - Code - ClassNumber.";
                    lblOutput.Visible = true;
                    return;
                }

                string department = locationParts[0].Trim();
                string collegeCode = locationParts[1].Trim();
                string classNumber = locationParts[2].Trim();

                string getLocationIdSql = @"SELECT LocationID FROM Location 
                                    WHERE Department = @Department 
                                      AND CollegeCode = @CollegeCode 
                                      AND ClassNumber = @ClassNumber";

                Dictionary<string, object> getLocationPara = new Dictionary<string, object>();
                getLocationPara.Add("@Department", department);
                getLocationPara.Add("@CollegeCode", collegeCode);
                getLocationPara.Add("@ClassNumber", classNumber);

                object locationResult = myCrud.ExecuteScalar(getLocationIdSql, getLocationPara);
                if (locationResult == null)
                {
                    lblOutput.Text = "❌ Location not found. Please enter a valid department, code, and class number.";
                    lblOutput.Visible = true;
                    return;
                }
                int locationId = Convert.ToInt32(locationResult);

                
                int submitRequestId;
                if (!int.TryParse(txtSubmitRequestId.Text, out submitRequestId))
                {
                    lblOutput.Text = "❌ Invalid Submit Request ID. Please enter a number.";
                    lblOutput.Visible = true;
                    return;
                }

                
                string checkSql = @"SELECT RequestTypeID, PriorityID, LocationID, DateTime, Description 
                            FROM SubmitRequest 
                            WHERE SubmitRequestID = @SubmitRequestID";

                Dictionary<string, object> checkPara = new Dictionary<string, object>();
                checkPara.Add("@SubmitRequestID", submitRequestId);

                var existingData = myCrud.getDrPassSql(checkSql, checkPara);

                if (existingData.Read())
                {
                    int existingRequestTypeId = Convert.ToInt32(existingData["RequestTypeID"]);
                    int existingPriorityId = Convert.ToInt32(existingData["PriorityID"]);
                    int existingLocationId = Convert.ToInt32(existingData["LocationID"]);
                    DateTime existingDateTime = Convert.ToDateTime(existingData["DateTime"]);
                    string existingDescription = existingData["Description"].ToString();

                    existingData.Close();

                    bool isSame = existingRequestTypeId == requestTypeId &&
                                  existingPriorityId == priorityId &&
                                  existingLocationId == locationId &&
                                  existingDateTime == Convert.ToDateTime(txtDate.Text) &&
                                  existingDescription == txtDescripation.Text.Trim();

                    if (isSame)
                    {
                        lblOutput.Text = "ℹ️ The data is the same. No update needed.";
                        lblOutput.Visible = true;
                        return;
                    }
                }
                else
                {
                    lblOutput.Text = "❌ Record not found to update.";
                    lblOutput.Visible = true;
                    return;
                }

                
                string mySql = @"UPDATE SubmitRequest
                         SET RequestTypeID = @RequestTypeID,
                             PriorityID = @PriorityID,
                             LocationID = @LocationID,
                             DateTime = @DateTime,
                             Description = @Description
                         WHERE SubmitRequestID = @SubmitRequestID";

                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@RequestTypeID", requestTypeId);
                myPara.Add("@PriorityID", priorityId);
                myPara.Add("@LocationID", locationId);
                myPara.Add("@DateTime", Convert.ToDateTime(txtDate.Text));
                myPara.Add("@Description", txtDescripation.Text.Trim());
                myPara.Add("@SubmitRequestID", submitRequestId);

                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

                if (rtn >= 1)
                {
                    lblOutput.Text = "✅ Request updated successfully.";
                }
                else
                {
                    lblOutput.Text = "⚠️ Update failed. No records were changed.";
                }

                lblOutput.Visible = true;

                showData();

            }
            catch (Exception )
            {
                lblOutput.Text = "❌ An unexpected error occurred. Please try again later.";
                lblOutput.Visible = true;
            }
        
    }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(gvShowInternData);
        }

        public static void ExportGridToExcel(GridView myGv) 
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
        protected void gvShowInternData_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvShowInternData.SelectedRow;

            txtSubmitRequestId.Text = row.Cells[1].Text;
            txtTypeName.Text = row.Cells[2].Text;
            txtPriority.Text = row.Cells[3].Text;
            txtLocation.Text = row.Cells[4].Text;
            txtDate.Text = row.Cells[5].Text;
            txtDescripation.Text = row.Cells[6].Text;
        }
        protected void gvShowInternData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectRequest")
            {
                string submitId = e.CommandArgument.ToString();

               
                foreach (GridViewRow row in gvShowInternData.Rows)
                {
                    
                    LinkButton lnk = (LinkButton)row.FindControl("lnkSubmitID");
                    if (lnk != null && lnk.Text == submitId)
                    {
                        
                        txtSubmitRequestId.Text = lnk.Text;
                        txtTypeName.Text = row.Cells[1].Text;
                        txtPriority.Text = row.Cells[2].Text;
                        txtLocation.Text = row.Cells[3].Text;
                        txtDate.Text = row.Cells[4].Text;
                        txtDescripation.Text = row.Cells[5].Text;

                        break;
                    }
                }
            }
        }


        protected void populateForm_Click(object sender, EventArgs e)
        {
            int PK = int.Parse((sender as LinkButton).CommandArgument);

            string mySql = @"select internParticipationId,	internId,	date,	note from internParticipation 
                                where internParticipationId =@internParticipationId ";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@internParticipationId", PK);
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        String InternParticipationId = dr["internParticipationId"].ToString();
                        String internId = dr["internId"].ToString();
                        String myDate = dr["date"].ToString();
                        String note = dr["note"].ToString();

                        txtSubmitRequestId.Text = InternParticipationId;
                        txtTypeName.Text = internId;
                        txtPriority.Text = myDate;
                        txtDescripation.Text = note;
                    }
                }
            }
        }

    }// cls
}// ns