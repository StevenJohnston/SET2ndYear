/*
    Name: Steven Johnston, Matthew Warren
    File: Pane.cs
    Assignment: SET Paint (#2)
    Date: 10/8/2015
    Description: Pane that allows for double buffering of panel
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SETPaint
{
    public class Pane: Panel
    {
        public Pane()
        {
            DoubleBuffered = true;
        }
    }
}
