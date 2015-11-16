﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiLoGame.aspx.cs" Inherits="ASPNET_HiLo.HiLoGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HiLo Asp Net</title>
</head>
<body>
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
                    <asp:Button id="btnName" style='width: 100%; height: 100%;' runat="server" Text="Submit"/></h3>
            </div>
            <!--Div to hold the enter max content-->
            <div id='divMax' runat="server">
                <input type='text' id='txtMax' runat="server"/>
                <asp:button id='btnMax' Text="Set Max" runat="server"/>
            </div>
            <!--Div to hold the guessing content for the game-->
            <div id='divGame' runat="server">
                <div id='divGuess'>
                    <input type='text' id='txtGuess' runat="server"/>
                    <asp:button id='btnGuess' Text="Guess" runat="server"/>
                </div>
            </div>
        </div>
        <!--This div will hold the messages that are recived from the server. Can be errors or just updated information-->
        <div id='divResult' runat="server">
        </div>
    </form>
</body>
</html>