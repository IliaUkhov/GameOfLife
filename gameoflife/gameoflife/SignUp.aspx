<%@ Page Language="C#" Inherits="gameoflife.SignUp" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Game of Life: Sign Up</title>
    <link rel="stylesheet" href="Style/Main.css" type="text/css" />
    <link rel="stylesheet" href="Style/SignUp.css" type="text/css" />
</head>
<body>
<form id="usrform" method="post" runat="server">
    <div id="signup_fields">
        Sign Up
        <asp:TextBox id="name" class="uicontrol textin" placeholder="name" runat="server" />
        <asp:TextBox id="email" class="uicontrol textin" TextMode="Email" placeholder="e-mail" runat="server" />
        <asp:TextBox id="password" class="uicontrol textin" TextMode="Password" placeholder="password" runat="server" />
        <asp:TextBox id="confirm" class="uicontrol textin" TextMode="Password" placeholder="confirm password" runat="server" />
        <asp:Button class="uicontrol submit_button" 
            OnClick="Submit" 
            Text="Create account" runat="server" />
        <asp:RegularExpressionValidator
            id="emailValidator"
            runat="server" 
            ControlToValidate="email"   
            ErrorMessage="Invalid e-mail" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" >
        </asp:RegularExpressionValidator>
    </div>
</form>
</body>
</html>
