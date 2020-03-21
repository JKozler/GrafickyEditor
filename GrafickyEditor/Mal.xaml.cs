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
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;


namespace GrafickyEditor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool mal;
        Point p1;
        Point p2;
        Brush brush = new SolidColorBrush(Colors.Black);
        int hod = 2;
        int index = 0;
        int backDelet;
        bool darkEff = false;
        Line[] cary = new Line[10000000];
        string jmeno = "";
        public Window1(string load)
        {
            InitializeComponent();
            
            if (load != "")
            {
                if (load.Length >= 25)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(load));
                    image.Width = WorkStation.Width;
                    image.Height = WorkStation.Height;
                    WorkStation.Children.Add(image);
                }
                else
                {
                    string path = @"E:\VS2019WPF\GrafickyEditor\GrafickyEditor\bin\Debug" + @"\" + load;
                    if (File.Exists(path))
                    {
                        jmeno = load;
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri(path));
                        image.Width = WorkStation.Width;
                        image.Height = WorkStation.Height;
                        WorkStation.Children.Add(image);
                        infoProj.Content = "You are editing " + jmeno;
                    }
                    else 
                    {
                        MessageBox.Show("Mising file.", "Your file is not inside debug file or you delete your file.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void WorkStation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            efectPanel.Visibility = Visibility.Hidden;
            if (WorkStation.Cursor == Cursors.Pen || WorkStation.Cursor == Cursors.Hand)
            {
                mal = true;
                p1 = e.GetPosition(WorkStation);
            }
        }

        private void WorkStation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mal = false;
        }

        private void WorkStation_MouseMove(object sender, MouseEventArgs e)
        {
            if (mal == true && WorkStation.Cursor == Cursors.Pen && darkEff == false)
            {
                Line line = new Line();

                p2 = e.GetPosition(WorkStation);
                line.X1 = p1.X;
                line.Y1 = p1.Y;
                line.X2 = p2.X;
                line.Y2 = p2.Y;
                p1 = p2;
                line.Stroke = brush;
                line.StrokeThickness = hod;
                WorkStation.Children.Add(line);
                cary[index] = line;
                index++;
            }
            if (mal == true && WorkStation.Cursor == Cursors.Pen && darkEff == true)
            {
                Line line = new Line();

                p2 = e.GetPosition(WorkStation);
                line.X1 = p1.X;
                line.Y1 = p1.Y;
                line.X2 = p2.X;
                line.Y2 = p2.Y;
                //p1 = p2;
                line.Stroke = brush;
                line.StrokeThickness = hod;
                WorkStation.Children.Add(line);
                cary[index] = line;
                index++;
            }
            if (mal == true && WorkStation.Cursor == Cursors.Hand)
            {
                Line line = new Line();

                p2 = e.GetPosition(WorkStation);
                line.X1 = p1.X;
                line.Y1 = p1.Y;
                line.X2 = p2.X;
                line.Y2 = p2.Y;
                p1 = p2;
                line.Stroke = new SolidColorBrush(Colors.LightGray);
                line.StrokeThickness = hod;
                WorkStation.Children.Add(line);

            }
        }

        private void Paint_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Pen;
            SliderTl.Maximum = 10;
            darkEff = false;
        }

        private void OnSliderValuChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            hod = Convert.ToInt32(SliderTl.Value);
        }

        private void BackPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image jpeg(*.jpg)|*.jpg|Image png(*.png)|*.png|Image PNG(*PNG)|*PNG";
            if (open.ShowDialog() == true)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(open.FileName));
                image.Width = WorkStation.Width;
                image.Height = WorkStation.Height;
                WorkStation.Children.Add(image);
            }
        }

        private void MouseBack_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Arrow;
        }

        private void Guma_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Hand;
            SliderTl.Maximum = 100;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveSubmit.Visibility = Visibility.Visible;
            nameOfFile.Visibility = Visibility.Visible;
            efectPanel.Visibility = Visibility.Hidden;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image obr(*.obr)|*.obr";
            if (open.ShowDialog() == true)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(open.FileName));
                image.Width = WorkStation.Width;
                image.Height = WorkStation.Height;
                WorkStation.Children.Add(image);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Children.Clear();
            index = 0;
            lblHistory.Items.Add("Clear All Items");
        }

        private void BackStep_Click(object sender, RoutedEventArgs e)
        {
            if ((index - backDelet) <= 0){
                index = 0;
                WorkStation.Children.Clear();
            }
            else
            {
                WorkStation.Children.Clear();
                for (int i = 0; i < index - backDelet; i++)
                {
                    WorkStation.Children.Add(cary[i]);
                }
                index = index - backDelet;
            }
        }

        private void DeleteHalf_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            backDelet = Convert.ToInt32(DeleteHalf.Value);
        }

        private void SaveSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (nameOfFile.Text != null)
            {
                string path = @"E:\VS2019WPF\GrafickyEditor\GrafickyEditor\bin\Debug\" + nameOfFile.Text + ".obr";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                FileStream fs = new FileStream(path, FileMode.Create);
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)WorkStation.ActualWidth,
                    (int)WorkStation.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                bmp.Render(WorkStation);
                BitmapEncoder encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();
                SaveSubmit.Visibility = Visibility.Hidden;
                nameOfFile.Visibility = Visibility.Hidden;
                Stream stream = new FileStream("saves.txt", FileMode.Append);
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.WriteLine(nameOfFile.Text);
                }
            }
            else
            {
                lblHistory.Items.Add("Write NAME!!!");
            }
        }

        private void ZmenaBarvy_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog color = new System.Windows.Forms.ColorDialog();
            if (color.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                brush = new SolidColorBrush(Color.FromRgb(color.Color.R, color.Color.G, color.Color.B));
            }
        }

        private void Efect_Click(object sender, RoutedEventArgs e)
        {
            efectPanel.Visibility = Visibility.Visible;
        }

        private void FlavorPen_Click(object sender, RoutedEventArgs e)
        {
            darkEff = true;
            WorkStation.Cursor = Cursors.Pen;
        }
    }
    class Cara
    {
        public Line line { get; set; }
        public Cara(Line line)
        {
            this.line = line;
        }
    }
}
