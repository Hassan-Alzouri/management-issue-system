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

namespace hassan11244WebApp1.admin
{
    public partial class mangeRequest : System.Web.UI.Page
    {
        public int totalRequests, openRequests, inProgressRequests, closedRequests;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                GetDashboardCounts();
                populateDdlAssint();
            }
        }
        private void GetDashboardCounts()
        {
            CRUD myCrud = new CRUD();

            totalRequests = Convert.ToInt32(myCrud.ExecuteScalar("SELECT COUNT(*) FROM SubmitRequest", null));

            string openSql = @"SELECT COUNT(*) 
                       FROM SubmitRequest sr 
                       JOIN Status s ON sr.StatusID = s.StatusID 
                       WHERE s.StatusName = 'Open'";
            openRequests = Convert.ToInt32(myCrud.ExecuteScalar(openSql, null));

            string progressSql = @"SELECT COUNT(*) 
                           FROM SubmitRequest sr 
                           JOIN Status s ON sr.StatusID = s.StatusID 
                           WHERE s.StatusName = 'In Progress'";
            inProgressRequests = Convert.ToInt32(myCrud.ExecuteScalar(progressSql, null));

            string closedSql = @"SELECT COUNT(*) 
                         FROM SubmitRequest sr 
                         JOIN Status s ON sr.StatusID = s.StatusID 
                         WHERE s.StatusName = 'Closed'";
            closedRequests = Convert.ToInt32(myCrud.ExecuteScalar(closedSql, null));
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

            try
            {
                
                if (!int.TryParse(txtSubmitRequestId.Text.Trim(), out int submitRequestId))
                {
                    lblOutput.Text = "❌ Invalid ID format. Please enter a valid numeric ID.";
                    lblOutput.Visible = true;
                    return;
                }

                CRUD myCrud = new CRUD();
                string mySql = @"
            DELETE FROM TaskAssignment WHERE SubmitRequestID = @SubmitRequestID;
            DELETE FROM SubmitRequest_Equipment WHERE SubmitRequestID = @SubmitRequestID;
            DELETE FROM SubmitRequest WHERE SubmitRequestID = @SubmitRequestID;";

                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@SubmitRequestID", submitRequestId);

                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

                if (rtn >= 1)
                {
                    lblOutput.Text = "✅ The request and related records have been successfully deleted.";
                }
                else
                {
                    lblOutput.Text = "⚠️ No matching records were found to delete.";
                }

                lblOutput.Visible = true;
                showData();
            }
            catch (Exception )
            {
                 lblOutput.Text = "❌ An error occurred while deleting. Please try again later or contact support.";
                
                lblOutput.Visible = true;
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblOutput.Visible = false;

            try
            {
                CRUD myCrud = new CRUD();

                
                string getRequestTypeIdSql = "SELECT RequestTypeID FROM RequestType WHERE TypeName = @TypeName";
                var getRequestTypePara = new Dictionary<string, object> { { "@TypeName", txtTypeName.Text.Trim() } };

                object requestTypeResult = myCrud.ExecuteScalar(getRequestTypeIdSql, getRequestTypePara);
                if (requestTypeResult == null)
                {
                    lblOutput.Text = "❌ Request Type not found. Please enter a valid type name.";
                    lblOutput.Visible = true;
                    return;
                }
                int requestTypeId = Convert.ToInt32(requestTypeResult);

             
                string getPriorityIdSql = "SELECT PriorityID FROM Priority WHERE PriorityName = @PriorityName";
                var getPriorityPara = new Dictionary<string, object> { { "@PriorityName", txtPriority.Text.Trim() } };

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
                    lblOutput.Text = "❌ Invalid location format. Use: Department - Code - ClassNumber.";
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

                var getLocationPara = new Dictionary<string, object>
        {
            { "@Department", department },
            { "@CollegeCode", collegeCode },
            { "@ClassNumber", classNumber }
        };

                object locationResult = myCrud.ExecuteScalar(getLocationIdSql, getLocationPara);
                if (locationResult == null)
                {
                    lblOutput.Text = "❌ Location not found. Please enter a valid location.";
                    lblOutput.Visible = true;
                    return;
                }
                int locationId = Convert.ToInt32(locationResult);

               
                if (!int.TryParse(txtSubmitRequestId.Text.Trim(), out int submitRequestId))
                {
                    lblOutput.Text = "❌ Invalid Submit Request ID. Please enter a numeric value.";
                    lblOutput.Visible = true;
                    return;
                }

                
                string checkSql = @"SELECT RequestTypeID, PriorityID, LocationID, DateTime, Description
                            FROM SubmitRequest WHERE SubmitRequestID = @SubmitRequestID";

                var checkPara = new Dictionary<string, object> { { "@SubmitRequestID", submitRequestId } };
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
                                  existingDateTime == Convert.ToDateTime(txtDate.Text.Trim()) &&
                                  existingDescription == txtDescripation.Text.Trim();

                    if (isSame)
                    {
                        lblOutput.Text = "ℹ️ No changes detected. Update skipped.";
                        lblOutput.Visible = true;
                        return;
                    }
                }
                else
                {
                    lblOutput.Text = "❌ No record found with the provided Submit Request ID.";
                    lblOutput.Visible = true;
                    return;
                }

                
                string updateSql = @"UPDATE SubmitRequest
                             SET RequestTypeID = @RequestTypeID,
                                 PriorityID = @PriorityID,
                                 LocationID = @LocationID,
                                 DateTime = @DateTime,
                                 Description = @Description
                             WHERE SubmitRequestID = @SubmitRequestID";

                var updatePara = new Dictionary<string, object>
        {
            { "@RequestTypeID", requestTypeId },
            { "@PriorityID", priorityId },
            { "@LocationID", locationId },
            { "@DateTime", Convert.ToDateTime(txtDate.Text.Trim()) },
            { "@Description", txtDescripation.Text.Trim() },
            { "@SubmitRequestID", submitRequestId }
        };

                int rtn = myCrud.InsertUpdateDelete(updateSql, updatePara);

                if (rtn >= 1)
                {
                    lblOutput.Text = "✅ Operation successful.";
                }
                else
                {
                    lblOutput.Text = "❌ Operation failed. No changes were made.";
                }
                lblOutput.Visible = true;

                showData();
            }
            catch (Exception)
            {
                lblOutput.Text = "❌ An unexpected error occurred. Please try again later.";
                lblOutput.Visible = true;
            }
        }


        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel(gvShowInternData);
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

        protected void populateDdlAssint()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select EmployeesID,Name from Employees";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlAssigntTo.DataValueField = "EmployeesID";
            ddlAssigntTo.DataTextField = "Name";
            ddlAssigntTo.DataSource = dr;
            ddlAssigntTo.DataBind();

            ddlAssigntTo.Items.Insert(0, new ListItem("-- Select Employee --", "0")); 
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

        protected void btnSubmitAssign_Click(object sender, EventArgs e)
        {


            try
            {
                lblOutput.Visible = false;
                if (string.IsNullOrEmpty(txtSubmitRequestId.Text) || string.IsNullOrEmpty(ddlAssigntTo.SelectedValue))
                {
                    lblOutput.Text = "❌ Please fill in both the request ID and assign-to fields.";

                    lblOutput.Visible = true;
                    return;
                }


                int requestId = int.Parse(txtSubmitRequestId.Text.Trim());
                int employeeId = int.Parse(ddlAssigntTo.SelectedValue);
               

                bool isValidId = int.TryParse(txtSubmitRequestId.Text, out requestId);
                bool isValidEmp = int.TryParse(ddlAssigntTo.SelectedValue, out employeeId);
                if (!isValidId || !isValidEmp)
                {
                    lblOutput.Text = "❌ Invalid format. Request ID and Employee selection must be numbers.";

                    lblOutput.Visible = true;
                    return;
                }

                bool isActive = true;

                CRUD myCrud = new CRUD();
                string mySql = @"INSERT INTO TaskAssignment 
                         (SubmitRequestID, AssignedToEmployeeID, IsActive) 
                         VALUES (@SubmitRequestID, @AssignedToEmployeeID, @IsActive)";

                Dictionary<string, object> myParams = new Dictionary<string, object>
        {
            { "@SubmitRequestID", requestId },
            { "@AssignedToEmployeeID", employeeId },
            { "@IsActive", isActive }
        };

                int rowsAffected = myCrud.ExecuteNonQuery(mySql, myParams);

                if (rowsAffected > 0)
                {
                    lblOutput.Text = "✅ Task was successfully assigned to the employee.";

                }
                else
                {
                    lblOutput.Text = "⚠️ No task was assigned. Please make sure the request ID is valid.";

                }

                lblOutput.Visible = true;
            }
            catch (Exception )
            {
                lblOutput.Text = "❌ Unexpected error occurred while assigning the task. Please try again or contact support.";

                lblOutput.Visible = true;
            }

        }
    }// cls
}// ns