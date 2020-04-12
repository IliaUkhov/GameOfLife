<%@ Page Language="C#" Inherits="gameoflife.Guestbook" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Game of Life: Guestbook</title>
    <link rel="stylesheet" href="Style/Main.css" type="text/css" />
    <link rel="stylesheet" href="Style/Guestbook.css" type="text/css" />
    <script src="ClientScripts/GuestbookScript.js" language="javascript"></script>
</head>
<body>
<table id="comments" style="width: 60%; margin: auto;">
    <tr style="font-size: 24pt; font-weight: bold; height: 50px; text-align: center;">
         <td>Author</td>
         <td>Comment</td>
    </tr>
</table>
<br>
<div id="inputs">
<form id="usrform" method="post" runat="server">
    <asp:TextBox id="nameInput" class="uicontrol textin" 
        placeholder="Your name" runat="server" />
    <asp:TextBox id="commentInput"
        class="moretexts uicontrol textin"
        TextMode="MultiLine"
        Rows="10" placeholder="Your comment..." 
        runat="server" />
    <asp:Button id="submitButton"
        class="uicontrol submit_button"
        OnClick="LeaveComment" 
        Text="Leave comment" runat="server" />
</form>
</div>
</body>
</html>
