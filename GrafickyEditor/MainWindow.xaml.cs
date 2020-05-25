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
        DateTime y;
        public MainWindow()
        {
            InitializeComponent();
            Load l = new Load();
            if (File.Exists("saves.txt"))
            {
                using (StreamReader sr = new StreamReader("saves.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string g = l.ObrCheck(line);
                        LastprojectLB.Items.Add(g);
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
            Load load = new Load(s, y);
            Window1 window1 = new Window1(load);
            window1.Show();
            this.Close();
        }

        private void loadProj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image obr(*.obr)|*.obr";
            if (open.ShowDialog() == true)
            {
                Load load = new Load(open.FileName, DateTime.Now);
                Window1 window1 = new Window1(load);
                window1.Show();
            }
        }

        private void LoadProject_Click(object sender, RoutedEventArgs e)
        {
            if (LastprojectLB.SelectedItem != null)
            {
                Load load = new Load(LastprojectLB.SelectedItem.ToString(), y);
                Window1 window1 = new Window1(load);
                window1.Show();
            }
        }

        private void RemoveProject_Click(object sender, RoutedEventArgs e)
        {
            List<string> projects = new List<string>();
            if (LastprojectLB.SelectedItem.ToString() != null)
            {
                if (File.Exists("saves.txt"))
                {
                    using (StreamReader sr = new StreamReader("saves.txt"))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string s = line + ".obr";
                            if (s == LastprojectLB.SelectedItem.ToString())
                            {}
                            else
                            {
                                projects.Add(line);
                            }
                        }
                    }
                }
            }
            Stream stream = new FileStream("saves.txt", FileMode.Create);
            LastprojectLB.Items.Clear();
            using (StreamWriter sw = new StreamWriter(stream))
            {
                foreach (string item in projects)
                {
                    LastprojectLB.Items.Add(item + ".obr");
                    sw.WriteLine(item);
                }
            }
        }
    }
}
