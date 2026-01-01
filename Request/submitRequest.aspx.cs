using hassan11244WebApp1.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hassan11244WebApp1.Request
{
    public partial class submitRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            if (!IsPostBack)
            {
                populateDdlSubmitRequest();
                populateDdlLocation();
                populateCblEquipment();
                populateRblPriority();

            }
                
            
        }

     
        protected void populateDdlSubmitRequest()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"Select RequestTypeID,TypeName from RequestType";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlRequestType.DataValueField = "RequestTypeID";
            ddlRequestType.DataTextField = "TypeName";
            ddlRequestType.DataSource = dr;
            ddlRequestType.DataBind();
        }
        protected void populateDdlLocation()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT LocationID, 
       Department + ' - ' + CollegeCode + ' - ' + ClassNumber AS FullLocation FROM Location;";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlLocation.DataValueField = "LocationID";
            ddlLocation.DataTextField = "FullLocation";
            ddlLocation.DataSource = dr;
            ddlLocation.DataBind();
        }
   
        protected void populateCblEquipment()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT EquipmentID, EquipmentName  FROM Equipment;";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            cblEquipment.DataTextField = "EquipmentName";
            cblEquipment.DataValueField = "EquipmentID";
            cblEquipment.DataSource = dr;
            cblEquipment.DataBind();
        }
        protected void populateRblPriority()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT PriorityID, PriorityName FROM Priority;";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            rblPriority.DataTextField = "PriorityName";
            rblPriority.DataValueField = "PriorityID";
            rblPriority.DataSource = dr;
            rblPriority.DataBind();
        }
        

   
        protected void btnSubmitRequest_Click(object sender, EventArgs e)
        {
           
            lblOutput.Visible = false;
            try
            {

                CRUD myCrud = new CRUD();

                
                string mySql = @"INSERT INTO SubmitRequest 
                         (RequestTypeID, PriorityID, UserID, LocationID, DateTime, Description,EquipmentID)
                         VALUES 
                         (@RequestTypeID, @PriorityID, @UserID, @LocationID, @DateTime, @Description,@EquipmentID);
                         SELECT SCOPE_IDENTITY();";

                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@RequestTypeID", ddlRequestType.SelectedValue);
                myPara.Add("@PriorityID", rblPriority.SelectedValue);
                myPara.Add("@UserID", 4); 
                myPara.Add("@LocationID", ddlLocation.SelectedValue);
                myPara.Add("@DateTime", DateTime.Now);
                myPara.Add("@Description", txtDescription.Text.Trim());
                myPara.Add("@EquipmentID", cblEquipment.SelectedValue);

                object result = myCrud.ExecuteScalar(mySql, myPara);

               
                if (result != null && int.TryParse(result.ToString(), out int submitRequestId) && submitRequestId > 0)
                {
                    
                    foreach (ListItem item in cblEquipment.Items)
                    {
                        if (item.Selected)
                        {
                            string equipSql = @"INSERT INTO SubmitRequest_Equipment (SubmitRequestID, EquipmentID)
                                        VALUES (@SubmitRequestID, @EquipmentID)";
                            Dictionary<string, object> equipPara = new Dictionary<string, object>();
                            equipPara.Add("@SubmitRequestID", submitRequestId);
                            equipPara.Add("@EquipmentID", int.Parse(item.Value));
                            myCrud.InsertUpdateDelete(equipSql, equipPara);
                        }
                    }

                    lblOutput.Text = "✅ Operation completed successfully.";
                    lblOutput.Visible = true;
                }
                else
                {
                    lblOutput.Text = "⚠️ Operation failed. No records were affected.";
                    lblOutput.Visible = true;
                }
            }
            catch (Exception )
            {
               
                lblOutput.Text = "❌ Operation failed due to a system error. Please try again later.";
                lblOutput.Visible = true;
            }
           

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           
            rblPriority.ClearSelection();

            
            cblEquipment.ClearSelection();

            
            ddlRequestType.SelectedIndex = -1;  
            ddlLocation.SelectedIndex = -1;    

            
            txtDescription.Text = "";

            lblOutput.Visible = false;
        }
    }
}