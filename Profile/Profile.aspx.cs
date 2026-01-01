using hassan11244WebApp1.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hassan11244WebApp1.Profile
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateDdlActivityLevels();
                populateDdlDietaryGoals();
                populateRbGender();
                populateCbDietaryRestrictions();


            }

        }
        protected void populateDdlActivityLevels(){

            CRUD myCrud = new CRUD();
            string mySql = @"SELECT ActivityId, ActivityName FROM ActivityLevels";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlActivity.DataValueField = "ActivityId";
            ddlActivity.DataTextField = "ActivityName";
            ddlActivity.DataSource = dr;
            ddlActivity.DataBind();
        }
        protected void populateDdlDietaryGoals()
        {

            CRUD myCrud = new CRUD();
            string mySql = @"SELECT GoalId, GoalName FROM DietaryGoals";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlGoal.DataValueField = "GoalId";
            ddlGoal.DataTextField = "GoalName";
            ddlGoal.DataSource = dr;
            ddlGoal.DataBind();
        }
        protected void populateRbGender()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT GenderId, GenderName FROM Gender";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            rbGender.DataTextField = "GenderName";
            rbGender.DataValueField = "GenderId";
            rbGender.DataSource = dr;
            rbGender.DataBind();
        }
        protected void populateCbDietaryRestrictions()
        {
            CRUD myCrud = new CRUD();
             string mySql = @"SELECT RestrictionId, RestrictionName FROM DietaryRestriction";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            cbDietaryRestriction.DataTextField = "RestrictionName";
            cbDietaryRestriction.DataValueField = "RestrictionId";
            cbDietaryRestriction.DataSource = dr;
            cbDietaryRestriction.DataBind();
        }



     
        protected void InsertUserDietaryRestrictions(int userId)
        {
            CRUD myCrud = new CRUD();
            string mySql = @"INSERT INTO UserDietaryRestriction (UserID, RestrictionID)
                     VALUES (@UserID, @RestrictionID)";

            foreach (ListItem item in cbDietaryRestriction.Items)
            {
                if (item.Selected)
                {
                    Dictionary<string, object> myPara = new Dictionary<string, object>();
                    myPara.Add("@UserID", userId);
                    myPara.Add("@RestrictionID", Convert.ToInt32(item.Value));

                    myCrud.InsertUpdateDelete(mySql, myPara);
                }
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            CRUD myCrud = new CRUD();
            string mySql = @"INSERT INTO UserProfile 
                     (UserID, Age, Weight, Height, Gender, ActivityLevel, Goal,Restrictions)
                     VALUES 
                     (@UserID, @Age, @Weight, @Height, @Gender, @ActivityLevel, @Goal,@Restrictions)";

            Dictionary<string, object> myPara = new Dictionary<string, object>();

            // Assume you're using logged-in user ID from session
            //myPara.Add("@UserId", Convert.ToInt32(Session["UserId"]));
            int hardcodedUserId =3;
            myPara.Add("@UserId", hardcodedUserId); // Replace 1 with an existing UserID from your database
            myPara.Add("@Age", Convert.ToInt32(txtAge.Text));
            myPara.Add("@Weight", Convert.ToDouble(txtWeight.Text));
            myPara.Add("@Height", Convert.ToDouble(txtHeight.Text));
            myPara.Add("@Gender", rbGender.SelectedItem.Text);
            myPara.Add("@ActivityLevel", ddlActivity.SelectedItem.Text);
            myPara.Add("@Goal", ddlGoal.SelectedItem.Text);
            myPara.Add("@Restrictions", cbDietaryRestriction.SelectedItem.Text);

            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

            if (rtn >= 1)
            {
                InsertUserDietaryRestrictions(1);
                lblOutput.Text = "✅ Profile saved successfully!";
            }
            else
            {
                lblOutput.Text = "❌ Failed to save profile.";
            }


        }
    }
   



}