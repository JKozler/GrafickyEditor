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
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;


namespace GrafickyEditor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //Zíkladní proměnné, které program využívá
        Ellipse helpEllipse = new Ellipse();
        Rectangle helpRectangle = new Rectangle();
        Rectangle helpRoundedRectangle = new Rectangle();
        Line helpLine = new Line();
        bool line = false;
        bool recta = false;
        bool roundedRe = false;
        bool roRe = false;
        bool movePic = false;
        bool ellep = false;
        bool mal;
        bool li = false;
        bool el = false;
        bool re = false;
        bool fullel = false;
        Point p1;
        Point p2;
        Point pic1;
        Point pic2;
        Brush brush = new SolidColorBrush(Colors.Black);
        int hod = 2;
        int index = 0;
        int backDelet;
        bool darkEff = false;
        UIElement[] elements = new UIElement[10000000];
        string jmeno = "*Untiteld - GPB";
        int p = 0;
        byte onoff = 0;
        byte addableBtn = 0;
        BlurEffect blur = new BlurEffect { KernelType = KernelType.Gaussian };
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public Window1(Load load)
        {
            InitializeComponent();            //set pro fog scene, když se zapne, tak default vypnout
            blur.Radius = 0;
            WorkStation.Effect = blur;
            WorkStation.Cursor = Cursors.Arrow;
            //jestliže si uživatel načítá projekt z 1. okna
            if (load.Nazev != "")
            {
                if (load.Nazev.Length >= 25)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(load.Nazev));
                    image.Width = WorkStation.Width;
                    image.Height = WorkStation.Height;
                    WorkStation.Children.Add(image);
                    elements[index] = image;
                    index++;
                }
                else
                {
                    string path = @"E:\VS2019WPF\GrafickyEditor\GrafickyEditor\bin\Debug" + @"\" + load.Nazev;
                    if (File.Exists(path))
                    {
                        jmeno = load.Nazev;
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri(path));
                        image.Width = WorkStation.Width;
                        image.Height = WorkStation.Height;
                        WorkStation.Children.Add(image);
                        elements[index] = image;
                        index++;
                        infoProj.Content = "You are editing " + jmeno;
                        lblHistory.Items.Add("You load it!");
                        TitleProject.Content = load.Nazev + " - GPB";
                    }
                    else 
                    {
                        MessageBox.Show("Mising file.", "Your file is not inside debug file or you delete your file.", MessageBoxButton.OK, MessageBoxImage.Error);
                        lblHistory.Items.Add("Missing file.");
                    }
                }
            }
        }
        //Pokud uživatel stiskne tlačítko myši dolů na WorkStation, tak......
        private void WorkStation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            efectPanel.Visibility = Visibility.Hidden;
            if (WorkStation.Cursor == Cursors.IBeam)
            {
                Point pa = e.GetPosition(WorkStation);
                TextBox tb = new TextBox();
                tb.Name = "tb" + p;
                tb.FontSize = 20;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.Background = new SolidColorBrush(Colors.LightGray);
                tb.Text = "TEXT";
                tb.Margin = new Thickness(Convert.ToDouble(pa.X), Convert.ToDouble(pa.Y), 0.0, 0.0);
                WorkStation.Children.Add(tb);
                elements[index] = tb;
                index++;
                lblHistory.Items.Add("Textbox was added.");
                DeleteHalf.Value = 1;
            }
            else if (WorkStation.Cursor == Cursors.Cross)
            {
                p1 = e.GetPosition(WorkStation);
                if (ellep == true)
                {
                    el = true;
                }
                else if (recta == true)
                {
                    re = true;
                }
                else if(line == true)
                {
                    li = true;
                }
                else
                {
                    roRe = true;
                }
            }
            else if (WorkStation.Cursor == Cursors.Pen || WorkStation.Cursor == Cursors.Hand)
            {
                mal = true;
                p1 = e.GetPosition(WorkStation);
            }
        }
        //Pokud uživatel pustí tlačítko myši nahoru, tak.....
        private void WorkStation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Stop();
            if (WorkStation.Cursor == Cursors.Hand)
            {
                lblHistory.Items.Add("Element was deleted.");
                mal = false;
            }
            else if (WorkStation.Cursor == Cursors.Cross && re == true)
            {
                WorkStation.Children.Remove(helpRectangle);
                Rectangle rec = new Rectangle();
                p2 = e.GetPosition(WorkStation);
                rec.Width = Math.Abs(p2.X - p1.X);
                rec.Height = Math.Abs(p2.Y - p1.Y);
                rec.Stroke = brush;
                rec.StrokeThickness = SliderTl.Value;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    index++;
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    index++;
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    rec.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    index++;
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    rec.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    index++;
                }
                re = false;
            }
            else if (WorkStation.Cursor == Cursors.Cross && roRe == true)
            {
                WorkStation.Children.Remove(helpRoundedRectangle);
                Rectangle rec = new Rectangle();
                p2 = e.GetPosition(WorkStation);
                rec.Width = Math.Abs(p2.X - p1.X);
                rec.RadiusX = 40;
                rec.RadiusY = 40;
                rec.Height = Math.Abs(p2.Y - p1.Y);
                rec.Stroke = brush;
                rec.StrokeThickness = SliderTl.Value;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    index++;
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    index++;
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    rec.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    index++;
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    rec.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    index++;
                }
                roRe = false;
            }
            else if (WorkStation.Cursor == Cursors.Cross && el == true && fullel == true)
            {
                WorkStation.Children.Remove(helpEllipse);
                Ellipse ellipse = new Ellipse();
                p2 = e.GetPosition(WorkStation);
                ellipse.StrokeThickness = SliderTl.Value;
                ellipse.Stroke = brush;
                ellipse.Width = Math.Abs(p2.X - p1.X) * 2;
                ellipse.Height = ellipse.Width;
                ellipse.Margin = new Thickness(p1.X - ellipse.Width / 2, p1.Y - ellipse.Height / 2, p1.X - ellipse.Width / 2, p1.Y - ellipse.Height / 2);
                WorkStation.Children.Add(ellipse);
                el = false;
                lblHistory.Items.Add("Ellispe was created.");
                elements[index] = ellipse;
                index++;
            }
            else if (WorkStation.Cursor == Cursors.Cross && el == true)
            {
                WorkStation.Children.Remove(helpEllipse);
                Ellipse ellipse = new Ellipse();
                p2 = e.GetPosition(WorkStation);
                ellipse.StrokeThickness = SliderTl.Value;
                ellipse.Stroke = brush;
                ellipse.Width = Math.Abs(p2.X - p1.X) * 2;
                ellipse.Height = Math.Abs(p2.Y - p1.Y) * 2;
                ellipse.Margin = new Thickness(p1.X  - ellipse.Width/2, p1.Y - ellipse.Height/2, p1.X - ellipse.Width/2, p1.Y - ellipse.Height/2);
                WorkStation.Children.Add(ellipse);
                el = false;
                lblHistory.Items.Add("Ellispe was created.");
                elements[index] = ellipse;
                index++;
            }
            else if (WorkStation.Cursor == Cursors.Cross && li == true)
            {
                WorkStation.Children.Remove(helpLine);
                Line line = new Line();
                p2 = e.GetPosition(WorkStation);
                line.X1 = p1.X;
                line.Y1 = p1.Y;
                line.X2 = p2.X;
                line.Y2 = p2.Y;
                line.StrokeThickness = SliderTl.Value;
                line.Stroke = brush;
                WorkStation.Children.Add(line);
                lblHistory.Items.Add("Line was created.");
                elements[index] = line;
                index++;
                li = false;
            }
            else if (WorkStation.Cursor != Cursors.Arrow)
            {
                lblHistory.Items.Add("Element was cerated.");
                mal = false;
                ellep = false;
                recta = false;
                el = false;
                re = false;
                line = false;
                li = false;
                roundedRe = false;
                roRe = false;
            }
        }
        //Když uživatel hýbe myší na WorkStation tak...
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
                elements[index] = line;
                index++;
            }
            else if (WorkStation.Cursor == Cursors.Cross && el == true && fullel == true)
            {
                WorkStation.Children.Remove(helpEllipse);
                p2 = e.GetPosition(WorkStation);
                helpEllipse.Width = Math.Abs(p2.X - p1.X) * 2;
                helpEllipse.Height = helpEllipse.Width;
                helpEllipse.Margin = new Thickness(p1.X - helpEllipse.Width / 2, p1.Y - helpEllipse.Height / 2, p1.X - helpEllipse.Width / 2, p1.Y - helpEllipse.Height / 2);
                helpEllipse.Stroke = new SolidColorBrush(Colors.Gray);
                helpEllipse.StrokeThickness = 2;
                WorkStation.Children.Add(helpEllipse);
            }
            else if (WorkStation.Cursor == Cursors.Cross && el == true)
            {
                WorkStation.Children.Remove(helpEllipse);
                p2 = e.GetPosition(WorkStation);
                helpEllipse.Width = Math.Abs(p2.X - p1.X) * 2;
                helpEllipse.Height = Math.Abs(p2.Y - p1.Y) * 2;
                helpEllipse.Margin = new Thickness(p1.X - helpEllipse.Width / 2, p1.Y - helpEllipse.Height / 2, p1.X - helpEllipse.Width / 2, p1.Y - helpEllipse.Height / 2);
                helpEllipse.Stroke = new SolidColorBrush(Colors.Gray);
                helpEllipse.StrokeThickness = 2;
                WorkStation.Children.Add(helpEllipse);
            }
            else if (WorkStation.Cursor == Cursors.Cross && li == true)
            {
                WorkStation.Children.Remove(helpLine);
                p2 = e.GetPosition(WorkStation);
                helpLine.X1 = p1.X;
                helpLine.X2 = p2.X;
                helpLine.Y1 = p1.Y;
                helpLine.Y2 = p2.Y;
                helpLine.Stroke = new SolidColorBrush(Colors.Gray);
                helpLine.StrokeThickness = 2;
                WorkStation.Children.Add(helpLine);
            }
            else if (WorkStation.Cursor == Cursors.Cross && re == true)
            {
                WorkStation.Children.Remove(helpRectangle);
                p2 = e.GetPosition(WorkStation);
                helpRectangle.Width = Math.Abs(p2.X - p1.X);
                helpRectangle.Height = Math.Abs(p2.Y - p1.Y);
                helpRectangle.Stroke = new SolidColorBrush(Colors.Gray);
                helpRectangle.StrokeThickness = 2;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    helpRectangle.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(helpRectangle);;
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    helpRectangle.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(helpRectangle);
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    helpRectangle.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(helpRectangle);
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    helpRectangle.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(helpRectangle);
                }
            }
            else if (WorkStation.Cursor == Cursors.Cross && roRe == true)
            {
                WorkStation.Children.Remove(helpRoundedRectangle);
                p2 = e.GetPosition(WorkStation);
                helpRoundedRectangle.Width = Math.Abs(p2.X - p1.X);
                helpRoundedRectangle.RadiusX = 40;
                helpRoundedRectangle.RadiusY = 40;
                helpRoundedRectangle.Height = Math.Abs(p2.Y - p1.Y);
                helpRoundedRectangle.Stroke = new SolidColorBrush(Colors.Gray);
                helpRoundedRectangle.StrokeThickness = 2;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    helpRoundedRectangle.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(helpRoundedRectangle); ;
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    helpRoundedRectangle.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(helpRoundedRectangle);
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    helpRoundedRectangle.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(helpRoundedRectangle);
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    helpRoundedRectangle.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(helpRoundedRectangle);
                }
            }
            else if (mal == true && WorkStation.Cursor == Cursors.Pen && darkEff == true)
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
                elements[index] = line;
                index++;
            }
            else if (mal == true && WorkStation.Cursor == Cursors.Hand)
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
                elements[index] = line;
                index++;
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
                image.Width = 1680;
                image.Height = 995;
                image.MouseDown += new MouseButtonEventHandler(DynamicPic_MouseDown);
                image.MouseMove += new MouseEventHandler(DynamicPic_MouseMove);
                image.MouseUp += new MouseButtonEventHandler(DynamicPic_MouseUp);
                WorkStation.Children.Add(image);
            }
            lblHistory.Items.Add("Uploading image...");
        }
        private void DynamicPic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pic1 = e.GetPosition(WorkStation);
            movePic = true;
        }
        private void DynamicPic_MouseMove(object sender, MouseEventArgs e)
        {
            Image i = (Image)sender;
            if (movePic == true)
            {
                WorkStation.Children.Remove(i);
                pic2 = e.GetPosition(WorkStation);
                i.Opacity = 0.5;
                i.Margin = new Thickness(pic1.X, pic2.Y, 0.0, 0.0);
                WorkStation.Children.Add(i);
                
            }
        }
        private void DynamicPic_MouseUp(object sender, MouseButtonEventArgs e)
        {
            pic2 = e.GetPosition(WorkStation);
            movePic = false;
        }
        private void MouseBack_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Arrow;
            lblHistory.Items.Add("Normal mouse is using.");
        }

        private void Guma_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Hand;
            lblHistory.Items.Add("Using rubber");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveSubmit.Visibility = Visibility.Visible;
            nameOfFile.Visibility = Visibility.Visible;
            efectPanel.Visibility = Visibility.Hidden;
            lblHistory.Items.Add("Save file...");
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
            lblHistory.Items.Add("project was loaded.");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Children.Clear();
            index = 0;
            lblHistory.Items.Add("Clear All Items");
            lblHistory.Items.Add("Work station is clear.");
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
                    WorkStation.Children.Add(elements[i]);
                }
                index = index - backDelet;
                lblHistory.Items.Add("Removing elments.");
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
                lblHistory.Items.Add("File was saved.");
                TitleProject.Content = nameOfFile.Text + " - GPB";
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
            lblHistory.Items.Add("Color was changed.");
        }

        private void Efect_Click(object sender, RoutedEventArgs e)
        {
            efectPanel.Visibility = Visibility.Visible;
        }

        private void FlavorPen_Click(object sender, RoutedEventArgs e)
        {
            darkEff = true;
            WorkStation.Cursor = Cursors.Pen;
            lblHistory.Items.Add("Dark pen activated.");
        }

        private void TextBTN_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.IBeam;
            p++;
            lblHistory.Items.Add("Adding text box.");
        }

        private void fogEff_Click(object sender, RoutedEventArgs e)
        {
            if (onoff == 0)
            {
                DoubleAnimation blurEnable = new DoubleAnimation(0, 10, new Duration(new TimeSpan(2000000)));
                blur.BeginAnimation(BlurEffect.RadiusProperty, blurEnable);
                fogEff.Background = new SolidColorBrush(Colors.Green);
                onoff++;
            }
            else if (onoff == 1)
            {
                onoff = 0;
                fogEff.Background = new SolidColorBrush(Colors.DarkRed);
                DoubleAnimation blurDisable = new DoubleAnimation(10, Convert.ToDouble(0), new Duration(new TimeSpan(2000000)));
                blur.BeginAnimation(BlurEffect.RadiusProperty, blurDisable);
            }
        }

        private void RectCreate_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Rectangle choose activated.");
            recta = true;
            line = false;
            ellep = false;
            el = false;
            li = false;
            roundedRe = false;
            roRe = false;
        }

        private void EllipseEff_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Ellipse choose activated.");
            ellep = true;
            recta = false;
            line = false;
            re = false;
            li = false;
            roundedRe = false;
            roRe = false;
        }

        private void LineEff_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Line choose activated.");
            ellep = false;
            recta = false;
            re = false;
            el = false;
            line = true;
            roundedRe = false;
            roRe = false;
        }
        private void roundedRect_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Rounded rect choose activated.");
            roundedRe = true;
            ellep = false;
            recta = false;
            re = false;
            el = false;
            line = false;
            roRe = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadFirstwinBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void MinNorBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DPUp_Click(object sender, RoutedEventArgs e)
        {
            if (addableBtn == 0)
            {
                AddableOne.Content = "Dark Pen";
                AddableOne.Opacity = 1;
                AddableOne.Click += new RoutedEventHandler(FlavorPen_Click);
                lblHistory.Items.Add("Dark Pen was added to main bar.");
                addableBtn++;
            }
            else if(addableBtn == 1)
            {
                AddableTwo.Content = "Dark Pen";
                AddableTwo.Opacity = 1;
                AddableTwo.Click += new RoutedEventHandler(FlavorPen_Click);
                lblHistory.Items.Add("Dark Pen was added to main bar.");
                addableBtn--;
            }
            else if (addableBtn == 2)
            {
                AddableThree.Content = "Dark Pen";
                AddableThree.Opacity = 1;
                AddableThree.Click += new RoutedEventHandler(FlavorPen_Click);
                lblHistory.Items.Add("Dark Pen was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 3)
            {
                AddableFour.Content = "Dark Pen";
                AddableFour.Opacity = 1;
                AddableFour.Click += new RoutedEventHandler(FlavorPen_Click);
                lblHistory.Items.Add("Dark Pen was added to main bar.");
                addableBtn = 0;
            }
        }

        private void FGUp_Click(object sender, RoutedEventArgs e)
        {
            //if (addableBtn == 0)
            //{
            //    AddableOne.Content = "Fog scene";
            //    AddableOne.Opacity = 1;
            //    AddableOne.Click += new RoutedEventHandler(fogEff_Click);
            //    lblHistory.Items.Add("Fog scene was added to main bar.");
            //    addableBtn++;
            //}
            //else if (addableBtn == 1)
            //{
            //    AddableTwo.Content = "Fog scene";
            //    AddableTwo.Opacity = 1;
            //    AddableTwo.Click += new RoutedEventHandler(fogEff_Click);
            //    lblHistory.Items.Add("Fog scene was added to main bar.");
            //    addableBtn--;
            //}
        }

        private void ReUp_Click(object sender, RoutedEventArgs e)
        {
            if (addableBtn == 0)
            {
                AddableOne.Content = "Rectangle";
                AddableOne.Opacity = 1;
                AddableOne.Click += new RoutedEventHandler(RectCreate_Click);
                lblHistory.Items.Add("Rectangle was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 1)
            {
                AddableTwo.Content = "Rectangle";
                AddableTwo.Opacity = 1;
                AddableTwo.Click += new RoutedEventHandler(RectCreate_Click);
                lblHistory.Items.Add("Rectangle was added to main bar.");
                addableBtn--;
            }
            else if (addableBtn == 2)
            {
                AddableThree.Content = "Rectangle";
                AddableThree.Opacity = 1;
                AddableThree.Click += new RoutedEventHandler(RectCreate_Click);
                lblHistory.Items.Add("Rectangle was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 3)
            {
                AddableFour.Content = "Rectangle";
                AddableFour.Opacity = 1;
                AddableFour.Click += new RoutedEventHandler(RectCreate_Click);
                lblHistory.Items.Add("Rectangle was added to main bar.");
                addableBtn = 0;
            }
        }

        private void ElUp_Click(object sender, RoutedEventArgs e)
        {
            if (addableBtn == 0)
            {
                AddableOne.Content = "Ellipse";
                AddableOne.Opacity = 1;
                AddableOne.Click += new RoutedEventHandler(EllipseEff_Click);
                lblHistory.Items.Add("Ellipse was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 1)
            {
                AddableTwo.Content = "Ellipse";
                AddableTwo.Opacity = 1;
                AddableTwo.Click += new RoutedEventHandler(EllipseEff_Click);
                lblHistory.Items.Add("Ellipse was added to main bar.");
                addableBtn--;
            }
            else if (addableBtn == 2)
            {
                AddableThree.Content = "Ellipse";
                AddableThree.Opacity = 1;
                AddableThree.Click += new RoutedEventHandler(EllipseEff_Click);
                lblHistory.Items.Add("Ellipse was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 3)
            {
                AddableFour.Content = "Ellipse";
                AddableFour.Opacity = 1;
                AddableFour.Click += new RoutedEventHandler(EllipseEff_Click);
                lblHistory.Items.Add("Ellipse was added to main bar.");
                addableBtn = 0;
            }
        }

        private void LiUp_Click(object sender, RoutedEventArgs e)
        {
            if (addableBtn == 0)
            {
                AddableOne.Content = "Line";
                AddableOne.Opacity = 1;
                AddableOne.Click += new RoutedEventHandler(LineEff_Click);
                lblHistory.Items.Add("Line was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 1)
            {
                AddableTwo.Content = "Line";
                AddableTwo.Opacity = 1;
                AddableTwo.Click += new RoutedEventHandler(LineEff_Click);
                lblHistory.Items.Add("Line was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 2)
            {
                AddableThree.Content = "Line";
                AddableThree.Opacity = 1;
                AddableThree.Click += new RoutedEventHandler(LineEff_Click);
                lblHistory.Items.Add("Line was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 3)
            {
                AddableFour.Content = "Line";
                AddableFour.Opacity = 1;
                AddableFour.Click += new RoutedEventHandler(LineEff_Click);
                lblHistory.Items.Add("Line was added to main bar.");
                addableBtn = 0;
            }
        }

        private void RoReUp_Click(object sender, RoutedEventArgs e)
        {
            if (addableBtn == 0)
            {
                AddableOne.Content = "Rounded rect";
                AddableOne.Opacity = 1;
                AddableOne.Click += new RoutedEventHandler(roundedRect_Click);
                lblHistory.Items.Add("Rounded rect was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 1)
            {
                AddableTwo.Content = "Rounded rect";
                AddableTwo.Opacity = 1;
                AddableTwo.Click += new RoutedEventHandler(roundedRect_Click);
                lblHistory.Items.Add("Rounded rect was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 2)
            {
                AddableThree.Content = "Rounded rect";
                AddableThree.Opacity = 1;
                AddableThree.Click += new RoutedEventHandler(roundedRect_Click);
                lblHistory.Items.Add("Rounded rect was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 3)
            {
                AddableFour.Content = "Rounded rect";
                AddableFour.Opacity = 1;
                AddableFour.Click += new RoutedEventHandler(roundedRect_Click);
                lblHistory.Items.Add("Rounded rect was added to main bar.");
                addableBtn = 0;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                fullel = true;
                TitleProject.Content = "Circle activated.";
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                fullel = false;
                TitleProject.Content = jmeno;
            }
        }
    }
}
