<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiLoGame.aspx.cs" Inherits="ASPNET_HiLo.HiLoGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hi Lo Asp Net</title>
</head>
<body id="bodyEl" style="background-color:#eee" runat="server">
    <form id="mainForm" runat="server">
        <!--Div hold the fullscreen play again button. Allows for easy hidding-->
        <div style='width: 100%; height: 1080px;' id='divPlayAgain' runat="server">
            <asp:Button id="btnPlayAgain" style='width: 100%; height: 100%;' runat="server" Text="Play Again"/>
        </div>
        <!--Div to hold entire game. Allows for easy hidding-->
        <div id='divMain' runat="server">
            <h1 id='chlg'>Client High Low Game</h1>
            <h2>Steven Johnston</h2>
            <!--Div to hold the enter name content-->
            <label id='lblName' runat="server"></label>
            <div id='divName' runat="server">
                <h3>Name:
                    <input type='text' id='txtName' runat="server"/>
                    <!--asp:RegularExpressionValidator to validate txtName before submitting to server-->
                    <asp:RegularExpressionValidator ID="nameValid" ValidationGroup="valName" ControlToValidate="txtName" ValidationExpression="^[a-zA-Z]+$" Display="Dynamic" ErrorMessage="Name must only be characters" runat="server"/>
                    <br/>
                    <asp:Button id="btnName" style='width: 100%; height: 100%;' ValidationGroup="valName" runat="server" Text="Submit"/></h3>
            </div>
            <!--Div to hold the enter max content-->
            <div id='divMax' runat="server">
                <input type='text' id='txtMax' runat="server"/>
                    <!--asp:RegularExpressionValidator to validate txtMax before submitting to server-->
                <asp:RegularExpressionValidator ID="maxValid" ValidationGroup="valMax" ControlToValidate="txtMax" ValidationExpression="^\d+$" Display="Dynamic" ErrorMessage="Max must be a whole number" runat="server"/>
                <br/>
                <asp:button id='btnMax' Text="Set Max" ValidationGroup="valMax" runat="server"/>
            </div>
            <!--Div to hold the guessing content for the game-->
            <div id='divGame' runat="server">
                <div id='divGuess'>
                    <input type='text' id='txtGuess' runat="server"/>
                    <!--asp:RegularExpressionValidator to validate txtGuess before submitting to server-->
                    <asp:RegularExpressionValidator ID="guessValid" ValidationGroup="valGuess" ControlToValidate="txtGuess" ValidationExpression="^\d+$" Display="Dynamic" ErrorMessage="Guess must be a whole number" runat="server"/>
                    <br/>
                    <asp:button id='btnGuess' Text="Guess" ValidationGroup="valGuess" runat="server"/>
                </div>
            </div>
        </div>
        <!--This div will hold the messages that are recived from the server. Can be errors or just updated information-->
        <div id='divResult' runat="server">
            
        </div>
    </form>
</body>
</html>
