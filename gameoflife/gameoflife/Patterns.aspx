<%@ Page Language="C#" Inherits="gameoflife.Patterns" %>
<!DOCTYPE html>
<html>
<head>
    <title>Game of Life: Community Patterns</title>
    <link rel="stylesheet" href="Style/Main.css" type="text/css" />
    <link rel="stylesheet" href="Style/Patterns.css" type="text/css" />
    <script src="ClientScripts/PatternsScript.js" language="javascript"></script>
</head>
<body>
<form id="form" action="Default.aspx" runat="server">
<asp:HiddenField id="selectedPatternId" runat="server" ClientIDMode="Static" />
<table id="patterns_list" style="width: 60%; margin: auto;">
    <tr style="font-size: 24pt; font-weight: bold; height: 50px; text-align: center;">
         <td>Uploaded by</td>
         <td>Description</td>
    </tr>
</table>
</form>
</body>
</html>
