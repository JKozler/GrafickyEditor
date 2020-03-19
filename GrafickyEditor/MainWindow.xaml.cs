using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrafickyEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("saves.txt"))
            {
                using (StreamReader sr = new StreamReader("saves.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        LastprojectLB.Items.Add(line + ".obr");
                    }
                }
            }
            else
            {
                loadInfo.Content = "You don't have any projects.";
            }
        }

        private void newProj_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            Window1 window1 = new Window1(s);
            window1.Show();
        }

        private void loadProj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image obr(*.obr)|*.obr";
            if (open.ShowDialog() == true)
            {
                Load load = new Load(open.FileName);
                Window1 window1 = new Window1(load.Nazev);
                window1.Show();
            }
        }

        private void LoadProject_Click(object sender, RoutedEventArgs e)
        {
            if (LastprojectLB.SelectedItem != null)
            {
                Load load = new Load(LastprojectLB.SelectedItem.ToString());
                Window1 window1 = new Window1(load.Nazev);
                window1.Show();
            }
        }
    }
}
