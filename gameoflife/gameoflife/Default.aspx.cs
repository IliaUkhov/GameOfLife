using System;
using System.Web;
using System.Web.UI;

namespace gameoflife
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request.Form["selectedPatternId"]))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "defaultInit",
                    "setField();", true);
                return;
            }

            int patternId = int.Parse(Request.Form["selectedPatternId"]);

            DBConnector.Connect();

            var pattern = DBConnector.GetPattern(patternId);

            DBConnector.Disconnect();

            //var pattern = ("101 102 103", 10, 10);

            int height = pattern.Item2;
            int width = pattern.Item3;
            System.Diagnostics.Debug.WriteLine("::" + pattern.Item1 + "::");

            Page.ClientScript.RegisterStartupScript(GetType(), "setFieldSize",
                    string.Format("resize({0}, {1});", width, height),  true);

            Page.ClientScript.RegisterStartupScript(GetType(), "setPattern",
                    string.Format("setPattern('{0}');", pattern.Item1), true);

        }

        protected void SharePattern(object sender, EventArgs e)
        {
            DBConnector.Connect();

            DBConnector.UploadPattern(int.Parse(loggedUserId.Value), descInput.Text, 
                    aliveCells.Value, 
                    int.Parse(patternHeight.Value), 
                    int.Parse(patternWidth.Value));

            DBConnector.Disconnect();
        }

        protected void Login(object sender, EventArgs e)
        {
            DBConnector.Connect();

            (int, string) user = (-1, "");

            if (CheckInput())
            {
                user = DBConnector.CheckLogin(emailInput.Text, pwdInput.Text);
            }

            DBConnector.Disconnect();

            if (user.Item1 >= 0)
            {
                loggedUserId.Value = user.Item1.ToString();
                loginFields.Visible = false;
                usrlabel.InnerText = "Logged as " + user.Item2;
                shareDiv.Attributes["style"] = "display: block";
            }
            else
            {
                loginFields.InnerText = "Invalid credentials";
            }
        }

        protected bool CheckInput()
        {
            return emailValidator.IsValid;
        }
    }
}
