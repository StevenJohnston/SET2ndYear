/*
 * File: HiLoGame.aspx.cs
 * Name: Steven Johnston
 * Date: 11/16/2015
 * Assignment: Hi Lo Asp.Net
 * Description: Introduction to ASP.NET by recreating the hi lo game
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ASPNET_HiLo
{
    public partial class HiLoGame : System.Web.UI.Page
    {
        /// <summary>
        /// Called when the game sill accessed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Remove all error messsages
            divResult.InnerHtml = "";
            //User has accessed page directly, not through form
            if (Request.Form.Count == 0)
            {
                txtName.Focus();
                divMax.Visible = false;
                divGame.Visible = false;
                divPlayAgain.Visible = false;
                ViewState["state"] = "name"; 
            }
            else//User is submitting form
            {
                string state = (string)ViewState["state"];//Get state
                if (state == "name")//User is entering name
                {
                    string nameRegex = @"^[a-zA-Z]+$";
                    string name = Request.Form["txtName"];
                    if (Regex.IsMatch(name, nameRegex))//Regex is valid
                    {
                        ViewState["name"] = name; //Save name
                        divName.Visible = false;
                        divMax.Visible = true;
                        ViewState["state"] = "maxNum";//change state to user is entering a max num
                        lblName.InnerText = "Hi, " + name + ". Enter a max number for the game";
                        txtMax.Focus();
                    }
                    else//Name failed regex
                    {
                        divResult.InnerHtml = "<h2>Name must consist of only character<h2>";
                        txtName.Focus();
                    }
                    txtName.Value = "";
                }
                else if (state == "maxNum")//User is entering the max Number 
                {
                    string maxNumRegex = @"^\d+$";
                    string maxNum = Request.Form["txtMax"];
                    if (Regex.IsMatch(maxNum, maxNumRegex))//Max number passes regex
                    {
                        ViewState["state"] = "guess";//Change state to guessing
                        ViewState["max"] = maxNum;//Set max to user entered number
                        ViewState["min"] = 1;//Set min to 1
                        ViewState["answer"] = new Random().Next(1, Convert.ToInt16(maxNum));//Create answer to random number from 1 to max
                        divMax.Visible = false;
                        divGame.Visible = true;
                        lblName.InnerText = ViewState["name"] + " Enter a guess";
                        divResult.InnerText = "Guess from " + ViewState["min"] + " To " + ViewState["max"];
                        txtGuess.Focus();
                    }
                    else//Regex failed
                    {
                        divResult.InnerHtml = "<h2>Max must be an positve whole number<h2>";
                        txtMax.Focus();
                    }
                    txtMax.Value = "";
                }
                else if (state == "guess")//State of game is guess
                {
                    string guessRegex = @"^\d+$";
                    string guessNumTxt = Request.Form["txtGuess"];
                    if (Regex.IsMatch(guessNumTxt, guessRegex))//Guess passed regex
                    {
                        int guessNum = Convert.ToInt16(guessNumTxt);
                        if (guessNum < Convert.ToInt16(ViewState["min"]))//Guess lower then min 
                        {
                            //Message to send to client
                            divResult.InnerHtml = "<h3>Your guess was to LOW try " + ViewState["min"] + " To " + ViewState["max"] + "</h3>";
                        }
                        else
                        {
                            //'Guess is higher then max Guess
                            if (guessNum > Convert.ToInt16(ViewState["max"]))//Guess greater then max
                            {
                                //Message to send to client
                                divResult.InnerHtml = "<h3>Your guess was to high try " + ViewState["min"] + " To " + ViewState["max"] + "</h3>";
                            }
                            else
                            {
                                //Guess is lower then correct guess(answer)
                                if (guessNum < Convert.ToInt16(ViewState["answer"]))
                                {
                                    //Change min Guess to guess +1
                                    ViewState["min"] = guessNum + 1;
                                    //Message to send to client
                                    divResult.InnerHtml = "<h3>That guess was LOW. <br >Enter a Number from " + ViewState["min"] + " to " + ViewState["max"] + "</h3>";
                                    //'Guees is higher then the correct guess (answer)
                                }
                                else if (guessNum > Convert.ToInt16(ViewState["answer"]))
                                {
                                    //Change max guess to guess -1
                                    ViewState["max"] = guessNum - 1;
                                    //Message to send to client
                                    divResult.InnerHtml = "<h3>That guess was HIGH. <br >Enter a Number from " + ViewState["min"] + " to " + ViewState["max"] + "</h3>";
                                    //Guess is correct
                                }
                                else
                                {
                                    //Message to send to client
                                    lblName.InnerText = ViewState["name"] + " Congradulations";
                                    divResult.InnerHtml = "";
                                    divPlayAgain.Visible = true;
                                    divMain.Visible = false;
                                    divGame.Visible = false;
                                    btnPlayAgain.Focus();
                                    ViewState["state"] = "PlayAgain";
                                    bodyEl.Style.Add("Background-color", "orange");
                                    btnPlayAgain.Text = "Winner, Play Again";
                                }
                            }
                        }
                    }
                    else//Guess failed regex
                    {
                        divResult.InnerHtml = "<h2>Enter a positive integer from " + ViewState["min"] + " to " + ViewState["max"] + "<h2>";
                    }
                    txtGuess.Value = "";
                    txtGuess.Focus();
                }
                else if (state == "PlayAgain")//User has pressed play again button
                {
                    bodyEl.Style.Add("Background-color", "#eee");
                    ViewState["state"] = "maxNum";
                    lblName.InnerText = ViewState["name"] + ", Enter a new max number";
                    divPlayAgain.Visible = false;
                    divMax.Visible = true;
                    divMain.Visible = true;
                }
            }
        }
    }
}