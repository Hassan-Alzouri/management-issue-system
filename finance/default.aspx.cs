using System;
using System.Globalization;
using System.Threading;
using System.Web.UI;

namespace hassan11244WebApp1.finance
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblOutput.Text = GetLocalResourceObject("lblOutputResource1.Text").ToString();
            }
        }

        protected void lbtnEnglish_Click(object sender, EventArgs e)
        {
            Session["culture"] = "en-US";
            Server.Transfer(Request.Url.PathAndQuery, false);
        }

        protected void lbtnArabic_Click(object sender, EventArgs e)
        {
            Session["culture"] = "ar-SA";
            Server.Transfer(Request.Url.PathAndQuery, false);
        }

        protected override void InitializeCulture()
        {
            if (Session["culture"] != null)
            {
                CultureInfo ci = new CultureInfo(Session["culture"].ToString());
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            base.InitializeCulture();
        }
    }
}
