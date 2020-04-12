using System;
using System.Web;
using System.Web.UI;

namespace gameoflife
{

    public partial class Guestbook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBConnector.Connect();

            var comments = DBConnector.GetComments();

            DBConnector.Disconnect();

            int counter = 1;
            foreach (var comment in comments)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "setComment" + counter.ToString(),
                        string.Format("addComment('{0}', '{1}');", comment.Item1, comment.Item2), true);
                counter += 1;
            }
        }

        protected void LeaveComment(object sender, EventArgs e)
        {
            DBConnector.Connect();

            DBConnector.UploadComment(nameInput.Text, commentInput.Text);

            DBConnector.Disconnect();
        }
    }
}
