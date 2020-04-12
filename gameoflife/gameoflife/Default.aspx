<%@ Page Language="C#" Inherits="gameoflife.Default" %>
<!DOCTYPE html>
<html>
<head>
    <title>John Conway's Game of Life</title>
    <link rel="stylesheet" href="Style/Main.css" type="text/css" />
    <link rel="stylesheet" href="Style/Default.css" type="text/css" />
    <script src="ClientScripts/DefaultScript.js" language="javascript"></script>
</head>
<body>
    <table style="width: 100%;">
        <tr>
            <td class="preferences">
                <div id="resize_div">Size: 15x15</div>
                <div onclick="resize(10, 10)" class="uicontrol button">10x10</div>
                <div onclick="resize(15, 15)" class="uicontrol button">15x15</div>
                <div onclick="resize(20, 20)" class="uicontrol button">20x20</div>
                <div onclick="resize(30, 30)" class="uicontrol button">30x30</div>
                <br>
                Speed:
                <table width="100%" style="margin-top: 5px">
                <tr>
                      <td id="s0.5" onclick="setSpeed(0.5)" class="uicontrol button sbutton first">1</td>
                      <td id="s1.0" onclick="setSpeed(1.0)" class="uicontrol button sbutton">2</td>
                      <td id="s2.0" onclick="setSpeed(2.0)" class="uicontrol button sbutton">4</td>
                      <td id="s4.0" onclick="setSpeed(4.0)" class="uicontrol button sbutton">8</td>
                      <td id="s8.0" onclick="setSpeed(8.0)" class="uicontrol button sbutton last">16</td>
                </tr>
                </table>
                <div id="speed_div">2 rounds/s</div>
                <br>
                <div id="step_div">Step: 0</div>
                <div id="start_button" class="color_button" onclick="start()">Start</div>
                <div id="pause_button" class="color_button" onclick="pause()">Pause</div>
                <div id="clear_button" class="color_button" onclick="clearf()">Clear</div>
            </td>
            <td>
                <div id="header">John Conway's Game of Life</div>
                <table id="field" style="margin-left: 15px">
                </table>
            </td>
            <td class="preferences">
                <div id="usrlabel" runat="server"></div><br>
                <div style="margin-top:10px">Community patterns</div>
                <div class="uicontrol button" onclick="window.location.href = 'Patterns.aspx';">View</div>
                <form id="save_form" method="post" runat="server">
                    <div id="loginFields" runat="server">
                        <sm>Please log in to share your patterns with community:</sm>
                        <asp:TextBox id="emailInput" class="uicontrol textin" TextMode="Email" 
                            placeholder="e-mail" runat="server" ClientIDMode="Static" />
                        <asp:TextBox id="pwdInput" class="uicontrol textin" TextMode="Password" 
                            placeholder="password" runat="server" ClientIDMode="Static" />
                        <asp:Button class="uicontrol submit_button" 
                            OnClick="Login"
                            Text="Login" runat="server" />
                        <asp:RegularExpressionValidator
                            id="emailValidator"
                            runat="server" 
                            ControlToValidate="emailInput"   
                            ErrorMessage="Invalid e-mail" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" >
                        </asp:RegularExpressionValidator>
                        <a href="SignUp.aspx"><sm>Don't have an account?</sm></a>
                    </div>
                    <asp:HiddenField id="aliveCells" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField id="patternHeight" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField id="patternWidth" runat="server" ClientIDMode="Static" />
                    <div id="shareDiv" class="shareDiv" style="display: none" runat="server">
                        <asp:HiddenField id="loggedUserId" runat="server" />
                        <asp:TextBox id="descInput"
                            class="uicontrol textin desc"
                            TextMode="MultiLine"
                            Rows="10" placeholder="Description" 
                            runat="server" />
                        <asp:Button id="shareButton"
                            class="uicontrol submit_button"
                            OnClientClick="sharePattern()" OnClick="SharePattern" 
                            Text="Share" runat="server" />
                    </div>
                    <a href="Guestbook.aspx"><sm>Guestbook</sm></a>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
