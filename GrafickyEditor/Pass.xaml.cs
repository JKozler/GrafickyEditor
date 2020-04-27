using System;
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
                    if (PassTxt.Text == File.ReadLines("pass.txt").First())
                    {
                        this.Close();
                    }
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
    }
}
