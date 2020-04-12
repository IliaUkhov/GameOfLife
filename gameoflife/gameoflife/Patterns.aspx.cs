using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gameoflife
{
    public partial class Patterns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBConnector.Connect();

            var patterns = DBConnector.GetPatternList();

            DBConnector.Disconnect();

            foreach (var pattern in patterns)
            {
                int patternId = pattern.Item1;
                Page.ClientScript.RegisterStartupScript(GetType(), "setRow" + patternId.ToString(),
                        string.Format("addRow('{0}', '{1}', {2});", pattern.Item2, pattern.Item3, patternId), true);
            }
        }
    }
}