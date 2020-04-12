using System;
using System.Web;
using System.Web.UI;

namespace gameoflife
{

    public partial class SignUp : System.Web.UI.Page
    {
        protected void Submit(object sender, EventArgs e)
        {
            DBConnector.Connect();

            if (CheckInput())
            {
                DBConnector.AddUser(name.Text, email.Text, password.Text);
                Response.Redirect("Default.aspx");
            }

            DBConnector.Disconnect();
        }

        protected bool CheckInput()
        {
            return emailValidator.IsValid;
        }
    }
}
