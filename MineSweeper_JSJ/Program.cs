﻿using System;
using System.Windows.Forms;
using MineSweeper_JSJ.Image;
using MineSweeper_JSJ.GameField;

namespace MineSweeper_JSJ
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}