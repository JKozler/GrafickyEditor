﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace GrafickyEditor
{
    /// <summary>
    /// Interaction logic for Pass.xaml
    /// </summary>
    public partial class Pass : Window
    {
        Password password = new Password();
        public Pass()
        {
            InitializeComponent();
        }

        private void SubmitPass_Click(object sender, RoutedEventArgs e)
        {
            if (PassTxt.Text != null)
            {
                if (File.Exists("pass.txt"))
                {
                    if (password.ControlPass(PassTxt.Text))
                        this.Close();
                    else
                        PassInfo.Content = "Bad password.";
                }
                else
                {
                    Stream stream = new FileStream("pass.txt", FileMode.Create);
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine(PassTxt.Text);
                    }
                    this.Close();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitPass_Click(sender, e);
            }
        }
    }
    public class Password
    {
        public string Pass { get; set; }
        public bool ControlPass(string pa)
        {
            if (pa == File.ReadLines("pass.txt").First())
            {
                return true;
            }
            return false;
        }
    }
}
