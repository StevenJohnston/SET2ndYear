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
        protected void Page_Load(object sender, EventArgs e)
        {
            divResult.InnerHtml = "";
            if (Request.Form.Count == 0)
            {
                divMax.Visible = false;
                divGame.Visible = false;
                divPlayAgain.Visible = false;
                ViewState["state"] = "name";
            }
            else
            {
                string state = (string)ViewState["state"];
                if (state == "name")
                {
                    string nameRegex = @"^[a-zA-Z]+$";
                    string name = Request.Form["txtName"];
                    if (Regex.IsMatch(name, nameRegex))
                    {
                        ViewState["name"] = name;
                        divName.Visible = false;
                        divMax.Visible = true;
                        ViewState["state"] = "maxNum";
                        lblName.InnerText = "Hi, " + name + ". Enter a max number for the game";
                    }
                    else
                    {
                        divResult.InnerHtml = "<h2>Name must consist of only character<h2>";
                    }
                }
                else if (state == "maxNum")
                {
                    string maxNumRegex = @"^\d+$";
                    string maxNum = Request.Form["txtMax"];
                    if (Regex.IsMatch(maxNum, maxNumRegex))
                    {
                        ViewState["state"] = "guess";
                        ViewState["max"] = maxNum;
                        ViewState["min"] = 1;
                        ViewState["answer"] = new Random().Next(1, Convert.ToInt16(maxNum));
                        divMax.Visible = false;
                        divGame.Visible = true;
                        state = "guess";
                        lblName.InnerText = ViewState["name"] + " Enter a guess";
                        divResult.InnerText = "Guess from " + ViewState["min"] + " To " + ViewState["max"];
                    }
                    else
                    {
                        divResult.InnerHtml = "<h2>Max must be an positve whole number<h2>";
                    }
                }
                else if (state == "guess")
                {
                    string guessRegex = @"^\d+$";
                    int guessNum = Convert.ToInt16(Request.Form["txtGuess"]);
                    if (Regex.IsMatch(Convert.ToString(guessNum), guessRegex))
                    {
                        if (guessNum < Convert.ToInt16(ViewState["min"]))
                        {
                            //Message to send to client
                            divResult.InnerHtml = "Your guess was to LOW try " + ViewState["min"] + " To " + ViewState["max"];
                        }
                        else
                        {
                            //'Guess is higher then max Guess
                            if (guessNum > Convert.ToInt16(ViewState["max"]))
                            {
                                //Message to send to client
                                divResult.InnerHtml = "Your guess was to high try " + ViewState["min"] + " To " + ViewState["max"];
                            }
                            else
                            {
                                //Guess is lower then correct guess(answer)
                                if (guessNum < Convert.ToInt16(ViewState["answer"]))
                                {
                                    //Change min Guess to guess +1
                                    ViewState["min"] = guessNum + 1;
                                    //Message to send to client
                                    divResult.InnerHtml = "That guess was LOW. <br >Enter a Number from " + ViewState["min"] + " to " + ViewState["max"];
                                    //'Guees is higher then the correct guess (answer)
                                }
                                else if (guessNum > Convert.ToInt16(ViewState["answer"]))
                                {
                                    //Change max guess to guess -1
                                    ViewState["max"] = guessNum - 1;
                                    //Message to send to client
                                    divResult.InnerHtml = "That guess was HIGH. <br >Enter a Number from " + ViewState["min"] + " to " + ViewState["max"];
                                    //Guess is correct
                                }
                                else
                                {
                                    //Message to send to client
                                    lblName.InnerText = ViewState["name"] + " Congradulations";
                                    divResult.InnerHtml = "WINNER";
                                    divPlayAgain.Visible = true;
                                    divMain.Visible = false;
                                    divGame.Visible = false;
                                    ViewState["state"] = "PlayAgain";
                                }
                            }
                        }
                    }
                    else
                    {
                        divResult.InnerHtml = "<h2>Enter a positive integer<h2>";
                    }
                }
                else if (state == "PlayAgain")
                {
                    divPlayAgain.Visible = false;
                    divMax.Visible = true;
                    divMain.Visible = true;
                }
            }
        }
    }
}