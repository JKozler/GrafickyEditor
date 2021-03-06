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
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;

namespace GrafickyEditor
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //Zíkladní proměnné, které program využívá
        Polygon helpPol = new Polygon();
        Polygon movedPolygon = new Polygon();
        Ellipse helpEllipse = new Ellipse();
        Ellipse movedEllipse = new Ellipse();
        Rectangle helpRectangle = new Rectangle();
        Rectangle helpRoundedRectangle = new Rectangle();
        Rectangle movedRect = new Rectangle();
        Line helpLine = new Line();
        Line lArrwo = new Line();
        Line pArrow = new Line();
        TextBox tBoxs = new TextBox();
        Image helpMovingImage = new Image();
        RichTextBox textBoxHelp = new RichTextBox();
        bool line = false;
        bool recta = false;
        bool roundedRe = false;
        bool roRe = false;
        bool ellep = false;
        bool fullRect = false;
        bool mal;
        bool li = false;
        bool el = false;
        bool re = false;
        bool dark = false;
        bool fullel = false;
        bool triangle = false;
        bool tr = false;
        bool loadPr = false;
        bool fillBool = false;
        bool blReBool = false;
        bool blElBool = false;
        bool blPoBool = false;
        bool moveText = false;
        bool holdPos = false;
        bool highlighterBool = false;
        bool highlighterBoolHelp = false;
        bool moveImageBool = false;
        bool dashedLine = false;
        bool helpDashedLine = false;
        bool arrow = false;
        bool helpArrow = false;
        bool blockText = false;
        bool textArea = false;
        bool textAreaSec = false;
        bool savePlease = false;
        Point p1;
        Point p2;
        Pages[] pages = new Pages[10];
        Brush back = new SolidColorBrush(Colors.White);
        Brush brush = new SolidColorBrush(Colors.Black);
        Brush fill = new SolidColorBrush(Colors.White);
        Brush externalFill = null;
        Brush ExternalBrush = null;
        int externalTl = 0;
        int hod = 2;
        int index = 0;
        int index1 = 0;
        int index2 = 0;
        int index3 = 0;
        int index4 = 0;
        int index5 = 0;
        int index6 = 0;
        int index7 = 0;
        int index8 = 0;
        int index9 = 0;
        int index10 = 0;
        int helpInd = 0;
        int backDelet;
        int forwBack;
        int hintPage = 0;
        bool darkEff = false;
        UIElement[] elements = new UIElement[10000000];
        UIElement[] elements0 = new UIElement[10000000];
        UIElement[] elements1 = new UIElement[10000000];
        UIElement[] elements2 = new UIElement[10000000];
        UIElement[] elements3 = new UIElement[10000000];
        UIElement[] elements4 = new UIElement[10000000];
        UIElement[] elements5 = new UIElement[10000000];
        UIElement[] elements6 = new UIElement[10000000];
        UIElement[] elements7 = new UIElement[10000000];
        UIElement[] elements8 = new UIElement[10000000];
        UIElement[] elements9 = new UIElement[10000000];
        string jmeno = "*Untiteld - GPB";
        string nazev;
        string pathSecondWay;
        int p = 0;
        byte onoff = 0;
        byte addableBtn = 0;
        byte dashedRecognize = 0;
        BlurEffect blur = new BlurEffect { KernelType = KernelType.Gaussian };
        List<TextBox> texts = new List<TextBox>();
        List<RichTextBox> richTextBoxes = new List<RichTextBox>();
        List<Line> guma = new List<Line>();
        System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();

        public Window1(Load load)
        {
            InitializeComponent();
            //Pro rozběhnutí pages
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i] = new Pages(false);
            }
            //set pro fog scene, když se zapne, tak default vypnout
            fill.Opacity = 0;
            blur.Radius = 0;
            WorkStation.Effect = blur;
            WorkStation.Cursor = Cursors.Arrow;
            infoProj.Content = "Page 0";
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
                    helpInd++;
                    index++;
                    loadPr = true;
                }
                else
                {
                    string path = @"C:\OBR\GrafickyEditor\GrafickyEditor\Project\Debug\" + load.Nazev;
                    string paths = @"C:\OBR\GrafickyEditor\GrafickyEditor\Project\Debug\" + "a" + load.Nazev;
                    try
                    {
                        if (File.Exists(path))
                        {
                            File.Copy(path, paths, true);
                            jmeno = load.Nazev;
                            nazev = load.Nazev;
                            Image image = new Image();
                            image.Source = new BitmapImage(new Uri(paths));
                            image.Width = WorkStation.Width;
                            image.Height = WorkStation.Height;
                            WorkStation.Children.Add(image);
                            elements[index] = image;
                            helpInd++;
                            index++;
                            pages[0].Element.Add(elements[index]);
                            infoProj.Content = "You are editing " + jmeno;
                            lblHistory.Items.Add("You load it!");
                            TitleProject.Content = load.Nazev + " - GPB";
                            loadPr = true;
                            savePlease = true;
                            PagesUpload(hintPage, image);
                        }
                        else
                        {
                            MessageBox.Show("Your file is not inside debug file or you delete your file.", "Mising file.", MessageBoxButton.OK, MessageBoxImage.Error);
                            lblHistory.Items.Add("Missing file.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error");
                    }
                }
            }
        }
        //Pokud uživatel stiskne tlačítko myši dolů na WorkStation, tak......
        private void WorkStation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            efectPanel.Visibility = Visibility.Hidden;
            if (WorkStation.Cursor == Cursors.IBeam && textArea == false)
            {
                Point pa = e.GetPosition(WorkStation);
                TextBox tb = new TextBox();
                tb.BorderThickness = new Thickness(0);
                tb.MouseDoubleClick += new MouseButtonEventHandler(DynamicTb_DoubleClick);
                tb.KeyDown += new KeyEventHandler(tb_MouseDown);
                tb.Name = "tb" + p;
                tb.FontSize = 20;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.Background = null;
                tb.Text = "TEXT";
                tb.Margin = new Thickness(Convert.ToDouble(pa.X), Convert.ToDouble(pa.Y), 0.0, 0.0);
                WorkStation.Children.Add(tb);
                texts.Add(tb);
                elements[index] = tb;
                helpInd++;
                index++;
                lblHistory.Items.Add("Textbox was added.");
                DeleteHalf.Value = 1;
                WorkStation.Cursor = Cursors.Arrow;
                PagesUpload(hintPage, tb);
            }
            else if (WorkStation.Cursor == Cursors.IBeam && textArea == true)
            {
                p1 = e.GetPosition(WorkStation);
                textAreaSec = true;
            }
            else if (highlighterBool == true)
            {
                p1 = e.GetPosition(WorkStation);
                highlighterBoolHelp = true;
                brush = new SolidColorBrush(Colors.Yellow);
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
                else if (triangle == true)
                {
                    tr = true;
                }
                else if (arrow == true)
                {
                    helpArrow = true;
                }
                else
                {
                    roRe = true;
                }
            }
            else if (WorkStation.Cursor == Cursors.Pen && dashedLine == true)
            {
                p1 = e.GetPosition(WorkStation);
                helpDashedLine = true;
            }
            else if (WorkStation.Cursor == Cursors.Pen || WorkStation.Cursor == Cursors.Hand && highlighterBool != true && dashedLine != true)
            {
                mal = true;
                p1 = e.GetPosition(WorkStation);
            }
        }
        //Pokud uživatel pustí tlačítko myši nahoru, tak.....
        private void WorkStation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (WorkStation.Cursor == Cursors.Arrow && blReBool == true || WorkStation.Cursor == Cursors.SizeAll && blReBool == true)
            {
                if (holdPos == true)
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedRect);
                    movedRect.Margin = new Thickness(p2.X, p1.Y, p1.X, p1.Y);
                    movedRect.Stroke = brush;
                    movedRect.Opacity = 1;
                    movedRect.Name = "blRe" + helpInd;
                    movedRect.Fill = externalFill;
                    movedRect.Stroke = ExternalBrush;
                    movedRect.StrokeThickness = externalTl;
                    movedRect.MouseDown += new MouseButtonEventHandler(Rec_MouseDown);
                    WorkStation.Children.Add(movedRect);
                    blReBool = false;
                    elements[index] = movedRect;
                    index++;
                    helpInd++;
                    lblHistory.Items.Add("Rectangle was successfully inherit.");
                    PagesUpload(hintPage, movedRect);
                }
                else
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedRect);
                    movedRect.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    movedRect.Stroke = brush;
                    movedRect.Opacity = 1;
                    movedRect.Name = "blRe" + helpInd;
                    movedRect.Fill = externalFill;
                    movedRect.Stroke = ExternalBrush;
                    movedRect.StrokeThickness = externalTl;
                    movedRect.MouseDown += new MouseButtonEventHandler(Rec_MouseDown);
                    WorkStation.Children.Add(movedRect);
                    blReBool = false;
                    elements[index] = movedRect;
                    index++;
                    helpInd++;
                    lblHistory.Items.Add("Rectangle was successfully inherit.");
                    PagesUpload(hintPage, movedRect);
                }
            }
            else if (WorkStation.Cursor == Cursors.IBeam && textAreaSec == true)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.BorderThickness = textBoxHelp.BorderThickness;
                textBox.Margin = textBoxHelp.Margin;
                textBox.Height = textBoxHelp.Height;
                textBox.Width = textBoxHelp.Width;
                textBox.Background = null;
                WorkStation.Children.Remove(textBoxHelp);
                WorkStation.Children.Add(textBox);
                elements[index] = textBox;
                richTextBoxes.Add(textBox);
                //textBox.MouseDoubleClick += new MouseButtonEventHandler(DynamicTbArea_DoubleClick);
                //textBox.MouseDown += new MouseButtonEventHandler(tbArea_MouseDown);
                index++;
                helpInd++;
                textAreaSec = false;
                textArea = false;
                WorkStation.Cursor = Cursors.Arrow;
                PagesUpload(hintPage, textBox);
            }
            else if (WorkStation.Cursor == Cursors.Arrow & blElBool == true || WorkStation.Cursor == Cursors.SizeAll && blElBool == true)
            {
                if (holdPos == true)
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedEllipse);
                    movedEllipse.Margin = new Thickness(p2.X, p1.Y, p1.X, p1.Y);
                    movedEllipse.Stroke = brush;
                    movedEllipse.Opacity = 1;
                    movedEllipse.Name = "blEl" + helpInd;
                    movedEllipse.Fill = externalFill;
                    movedEllipse.Stroke = ExternalBrush;
                    movedEllipse.MouseDown += new MouseButtonEventHandler(Ellipse_MouseDown);
                    movedEllipse.StrokeThickness = externalTl;
                    WorkStation.Children.Add(movedEllipse);
                    blElBool = false;
                    elements[index] = movedEllipse;
                    index++;
                    helpInd++;
                    lblHistory.Items.Add("Ellipse was successfully inherit.");
                    PagesUpload(hintPage, movedEllipse);
                }
                else
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedEllipse);
                    movedEllipse.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    movedEllipse.Stroke = brush;
                    movedEllipse.Opacity = 1;
                    movedEllipse.Name = "blEl" + helpInd;
                    movedEllipse.Fill = externalFill;
                    movedEllipse.Stroke = ExternalBrush;
                    movedEllipse.MouseDown += new MouseButtonEventHandler(Ellipse_MouseDown);
                    movedEllipse.StrokeThickness = externalTl;
                    WorkStation.Children.Add(movedEllipse);
                    blElBool = false;
                    elements[index] = movedEllipse;
                    index++;
                    helpInd++;
                    lblHistory.Items.Add("Ellipse was successfully inherit.");
                    PagesUpload(hintPage, movedEllipse);
                }
            }
            //Text move
            else if (WorkStation.Cursor == Cursors.SizeAll && moveText == true)
            {
                TextBox tb = new TextBox();
                WorkStation.Children.Remove(tBoxs);
                tBoxs.Opacity = 1;
                tBoxs.Margin = new Thickness(p2.X, p2.Y, 0.0, 0.0);
                tb = tBoxs;
                tb.Name = "tb" + helpInd;
                WorkStation.Children.Add(tb);
                moveText = false;
                elements[index] = tb;
                helpInd++;
                index++;
                PagesUpload(hintPage, tb);
            }
            //Image move
            else if (WorkStation.Cursor == Cursors.SizeAll && moveImageBool == true)
            {
                p2 = e.GetPosition(WorkStation);
                Image img = new Image();
                WorkStation.Children.Remove(helpMovingImage);
                img.Source = helpMovingImage.Source;
                img.Width = helpMovingImage.Width;
                img.Height = helpMovingImage.Height;
                img.Margin = new Thickness(p2.X - (helpMovingImage.Width / 2), p2.Y - (helpMovingImage.Height / 2), p2.X + (helpMovingImage.Width / 2), p2.Y + (helpMovingImage.Height / 2));
                img.Opacity = 1;
                img.Name = "img" + helpInd;
                img.MouseDown += new MouseButtonEventHandler(Image_MouseDown);
                WorkStation.Children.Add(img);
                lblHistory.Items.Add("Image was moved");
                moveImageBool = false;
                elements[index] = img;
                helpInd++;
                index++;
                PagesUpload(hintPage, img);
            }
            else if (WorkStation.Cursor == Cursors.Hand)
            {
                lblHistory.Items.Add("Element was deleted.");
                mal = false;
            }
            else if (WorkStation.Cursor == Cursors.Pen && helpDashedLine == true)
            {
                lblHistory.Items.Add("Element was deleted.");
                helpDashedLine = false;
                dashedRecognize = 0;
            }
            else if (WorkStation.Cursor == Cursors.Pen && highlighterBoolHelp == true)
            {
                highlighterBoolHelp = false;
                brush = new SolidColorBrush(Colors.Black);
            }
            else if (WorkStation.Cursor == Cursors.Cross && tr == true)
            {
                WorkStation.Children.Remove(helpPol);
                Polygon pol = new Polygon();
                pol.MouseDown += new MouseButtonEventHandler(Pol_MouseDown);
                PointCollection pts = new PointCollection();
                p2 = e.GetPosition(WorkStation);
                pts.Add(new Point(p1.X, p2.Y));
                pts.Add(new Point(p2.X, p2.Y));
                pts.Add(new Point(p1.X + (p2.X - p1.X), p1.Y));
                pol.Points = pts;
                pol.Stroke = brush;
                pol.Fill = fill;
                pol.StrokeThickness = SliderTl.Value;
                elements[index] = pol;
                helpInd++;
                index++;
                WorkStation.Children.Add(pol);
                lblHistory.Items.Add("Triangle was created.");
                tr = false;
                PagesUpload(hintPage, pol);
            }
            else if (WorkStation.Cursor == Cursors.Cross && re == true && fullRect == true)
            {
                WorkStation.Children.Remove(helpRectangle);
                Rectangle rec = new Rectangle();
                p2 = e.GetPosition(WorkStation);
                rec.Width = Math.Abs(p2.X - p1.X);
                rec.Name = "rec" + helpInd;
                rec.MouseDown += new MouseButtonEventHandler(Rec_MouseDown);
                rec.Height = rec.Width;
                rec.Stroke = brush;
                rec.Fill = fill;
                rec.StrokeThickness = SliderTl.Value;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    rec.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    rec.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                re = false;
            }
            else if (WorkStation.Cursor == Cursors.Cross && re == true)
            {
                WorkStation.Children.Remove(helpRectangle);
                Rectangle rec = new Rectangle();
                rec.Name = "rec" + helpInd;
                rec.MouseDown += new MouseButtonEventHandler(Rec_MouseDown);
                p2 = e.GetPosition(WorkStation);
                rec.Width = Math.Abs(p2.X - p1.X);
                rec.Height = Math.Abs(p2.Y - p1.Y);
                rec.Stroke = brush;
                rec.Fill = fill;
                rec.StrokeThickness = SliderTl.Value;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    rec.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    rec.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                re = false;
            }
            else if (WorkStation.Cursor == Cursors.Cross && roRe == true && fullRect == true)
            {
                WorkStation.Children.Remove(helpRoundedRectangle);
                Rectangle rec = new Rectangle();
                p2 = e.GetPosition(WorkStation);
                rec.Width = Math.Abs(p2.X - p1.X);
                rec.Name = "rec" + helpInd;
                rec.MouseDown += new MouseButtonEventHandler(Rec_MouseDown);
                rec.RadiusX = 40;
                rec.RadiusY = 40;
                rec.Height = rec.Width;
                rec.Stroke = brush;
                rec.Fill = fill;
                rec.StrokeThickness = SliderTl.Value;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    rec.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    rec.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                roRe = false;
            }
            else if (WorkStation.Cursor == Cursors.Cross && roRe == true)
            {
                WorkStation.Children.Remove(helpRoundedRectangle);
                Rectangle rec = new Rectangle();
                p2 = e.GetPosition(WorkStation);
                rec.Name = "rec" + helpInd;
                rec.MouseDown += new MouseButtonEventHandler(Rec_MouseDown);
                rec.Width = Math.Abs(p2.X - p1.X);
                rec.RadiusX = 40;
                rec.RadiusY = 40;
                rec.Height = Math.Abs(p2.Y - p1.Y);
                rec.Stroke = brush;
                rec.Fill = fill;
                rec.StrokeThickness = SliderTl.Value;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    rec.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    rec.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    rec.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(rec);
                    lblHistory.Items.Add("Rounded rect. was created");
                    elements[index] = rec;
                    helpInd++;
                    index++;
                    PagesUpload(hintPage, rec);
                }
                roRe = false;
            }
            else if (WorkStation.Cursor == Cursors.Cross && el == true && fullel == true)
            {
                WorkStation.Children.Remove(helpEllipse);
                Ellipse ellipse = new Ellipse();
                p2 = e.GetPosition(WorkStation);
                ellipse.StrokeThickness = SliderTl.Value;
                ellipse.MouseDown += new MouseButtonEventHandler(Ellipse_MouseDown);
                ellipse.Stroke = brush;
                ellipse.Width = Math.Abs(p2.X - p1.X) * 2;
                ellipse.Height = ellipse.Width;
                ellipse.Fill = fill;
                ellipse.Margin = new Thickness(p1.X - ellipse.Width / 2, p1.Y - ellipse.Height / 2, p1.X - ellipse.Width / 2, p1.Y - ellipse.Height / 2);
                WorkStation.Children.Add(ellipse);
                el = false;
                lblHistory.Items.Add("Ellispe was created.");
                elements[index] = ellipse;
                helpInd++;
                index++;
                PagesUpload(hintPage, ellipse);
            }
            else if (WorkStation.Cursor == Cursors.Cross && el == true)
            {
                WorkStation.Children.Remove(helpEllipse);
                Ellipse ellipse = new Ellipse();
                p2 = e.GetPosition(WorkStation);
                ellipse.StrokeThickness = SliderTl.Value;
                ellipse.Name = "EL" + helpInd;
                ellipse.Fill = fill;
                ellipse.MouseDown += new MouseButtonEventHandler(Ellipse_MouseDown);
                ellipse.Stroke = brush;
                ellipse.Width = Math.Abs(p2.X - p1.X) * 2;
                ellipse.Height = Math.Abs(p2.Y - p1.Y) * 2;
                ellipse.Margin = new Thickness(p1.X  - ellipse.Width/2, p1.Y - ellipse.Height/2, p1.X - ellipse.Width/2, p1.Y - ellipse.Height/2);
                WorkStation.Children.Add(ellipse);
                el = false;
                lblHistory.Items.Add("Ellispe was created.");
                elements[index] = ellipse;
                helpInd++;
                index++;
                PagesUpload(hintPage, ellipse);
            }
            else if (WorkStation.Cursor == Cursors.Cross && helpArrow == true)
            {
                WorkStation.Children.Remove(helpLine);
                WorkStation.Children.Remove(pArrow);
                WorkStation.Children.Remove(lArrwo);
                Line line = new Line();
                Line line1 = new Line();
                Line line2 = new Line();
                p2 = e.GetPosition(WorkStation);
                line = helpLine;
                line1 = lArrwo;
                line2 = pArrow;
                line.StrokeThickness = SliderTl.Value;
                line.Stroke = brush;
                line1.StrokeThickness = SliderTl.Value;
                line1.Stroke = brush;
                line2.StrokeThickness = SliderTl.Value;
                line2.Stroke = brush;
                WorkStation.Children.Add(line);
                WorkStation.Children.Add(line1);
                WorkStation.Children.Add(line2);
                lblHistory.Items.Add("Arrow was created.");
                elements[index] = line;
                helpInd++;
                index++;
                helpArrow = false;
                PagesUpload(hintPage, line);
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
                helpInd++;
                index++;
                li = false;
                PagesUpload(hintPage, line);
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
            if (WorkStation.Cursor == Cursors.Arrow && blReBool == true || WorkStation.Cursor == Cursors.SizeAll && blReBool == true)
            {
                if (holdPos == true)
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedRect);
                    movedRect.Margin = new Thickness(p2.X, p1.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(movedRect);
                }
                else
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedRect);
                    movedRect.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(movedRect);
                }
            }
            //Add text area
            else if (WorkStation.Cursor == Cursors.IBeam && textAreaSec == true)
            {
                p2 = e.GetPosition(WorkStation);
                WorkStation.Children.Remove(textBoxHelp);
                textBoxHelp.Width = Math.Abs(p2.X - p1.X);
                textBoxHelp.Height = Math.Abs(p2.Y - p1.Y);
                textBoxHelp.BorderBrush = brush;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    textBoxHelp.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(textBoxHelp);
                }
                else if (p1.X < p2.X && p1.Y > p2.Y)
                {
                    textBoxHelp.Margin = new Thickness(p1.X, p2.Y, p2.X, p1.Y);
                    WorkStation.Children.Add(textBoxHelp);
                }
                else if (p1.Y < p2.Y && p1.X < p2.X)
                {
                    textBoxHelp.Margin = new Thickness(p1.X, p1.Y, p2.X, p2.Y);
                    WorkStation.Children.Add(textBoxHelp);
                }
                else if (p1.Y < p2.Y && p1.X > p2.X)
                {
                    textBoxHelp.Margin = new Thickness(p2.X, p1.Y, p1.X, p2.Y);
                    WorkStation.Children.Add(textBoxHelp);
                }
            }
            //Text move
            else if (WorkStation.Cursor == Cursors.SizeAll && moveText == true)
            {
                p2 = e.GetPosition(WorkStation);
                WorkStation.Children.Remove(tBoxs);
                tBoxs.Opacity = 0.5;
                tBoxs.Margin = new Thickness(p2.X, p2.Y, 0.0, 0.0);
                WorkStation.Children.Add(tBoxs);
            }
            //Image move
            else if (WorkStation.Cursor == Cursors.SizeAll && moveImageBool == true)
            {
                p2 = e.GetPosition(WorkStation);
                WorkStation.Children.Remove(helpMovingImage);
                helpMovingImage.Opacity = 0.5;
                helpMovingImage.Margin = new Thickness(p2.X - (helpMovingImage.Width / 2), p2.Y - (helpMovingImage.Height / 2), p2.X + (helpMovingImage.Width / 2), p2.Y + (helpMovingImage.Height / 2));
                WorkStation.Children.Add(helpMovingImage);
            }
            else if (WorkStation.Cursor == Cursors.Arrow && blElBool == true || WorkStation.Cursor == Cursors.SizeAll && blElBool == true)
            {
                if (holdPos == true)
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedEllipse);
                    movedEllipse.Margin = new Thickness(p2.X, p1.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(movedEllipse);
                }
                else
                {
                    p2 = e.GetPosition(WorkStation);
                    WorkStation.Children.Remove(movedEllipse);
                    movedEllipse.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(movedEllipse);
                }
            }
            else if (WorkStation.Cursor == Cursors.Pen && highlighterBoolHelp == true)
            {
                Line line = new Line();

                p2 = e.GetPosition(WorkStation);
                line.X1 = p1.X;
                line.Y1 = p1.Y;
                line.X2 = p2.X;
                line.Y2 = p2.Y;
                line.Opacity = 0.3;
                p1 = p2;
                line.Stroke = brush;
                line.StrokeThickness = 10;
                WorkStation.Children.Add(line);
                elements[index] = line;
                helpInd++;
                index++;
                PagesUpload(hintPage, line);
            }
            else if (WorkStation.Cursor == Cursors.Cross && helpArrow == true)
            {
                WorkStation.Children.Remove(helpLine);
                WorkStation.Children.Remove(pArrow);
                WorkStation.Children.Remove(lArrwo);
                p2 = e.GetPosition(WorkStation);
                lArrwo.X1 = p2.X;
                pArrow.X1 = p2.X;
                lArrwo.Y1 = p2.Y;
                pArrow.Y1 = p2.Y;
                lArrwo.X2 = p2.X - ((p2.X - p1.X) * 0.05);
                pArrow.X2 = p2.X - ((p2.X - p1.X) * 0.05);
                lArrwo.Y2 = p2.Y + 10;
                pArrow.Y2 = p2.Y - 10;
                helpLine.X1 = p1.X;
                helpLine.X2 = p2.X;
                helpLine.Y1 = p1.Y;
                helpLine.Y2 = p2.Y;
                lArrwo.Stroke = new SolidColorBrush(Colors.Gray);
                pArrow.Stroke = new SolidColorBrush(Colors.Gray);
                helpLine.Stroke = new SolidColorBrush(Colors.Gray);
                pArrow.StrokeThickness = 2;
                lArrwo.StrokeThickness = 2;
                helpLine.StrokeThickness = 2;
                WorkStation.Children.Add(pArrow);
                WorkStation.Children.Add(lArrwo);
                WorkStation.Children.Add(helpLine);

            }
            else if (WorkStation.Cursor == Cursors.Pen && helpDashedLine == true)
            {
                Line line = new Line();
                p2 = e.GetPosition(WorkStation);
                if (dashedRecognize <= 21)
                {
                    line.X1 = p1.X;
                    line.Y1 = p1.Y;
                    line.X2 = p2.X;
                    line.Y2 = p2.Y;
                    p1 = p2;
                    line.Stroke = brush;
                    line.StrokeThickness = hod;
                    WorkStation.Children.Add(line);
                    elements[index] = line;
                    helpInd++;
                    index++;
                    dashedRecognize++;
                    PagesUpload(hintPage, line);
                }
                else
                {
                    line.X1 = p1.X;
                    line.Y1 = p1.Y;
                    line.X2 = p2.X;
                    line.Y2 = p2.Y;
                    p1 = p2;
                    line.Stroke = back;
                    line.StrokeThickness = hod;
                    WorkStation.Children.Add(line);
                    elements[index] = line;
                    helpInd++;
                    index++;
                    if (dashedRecognize == 41)
                    {
                        dashedRecognize = 0;
                    }
                    dashedRecognize++;
                    PagesUpload(hintPage, line);
                }
            }
            else if (mal == true && WorkStation.Cursor == Cursors.Pen && darkEff == false)
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
                helpInd++;
                index++;
                PagesUpload(hintPage, line);
            }
            else if (WorkStation.Cursor == Cursors.Cross && tr == true)
            {
                WorkStation.Children.Remove(helpPol);
                PointCollection pts = new PointCollection();
                p2 = e.GetPosition(WorkStation);
                pts.Add(new Point(p1.X, p2.Y));
                pts.Add(new Point(p2.X, p2.Y));
                pts.Add(new Point(p1.X + (p2.X - p1.X), p1.Y));
                helpPol.Points = pts;
                helpPol.Stroke = new SolidColorBrush(Colors.Gray);
                helpPol.StrokeThickness = 2;
                WorkStation.Children.Add(helpPol);
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
            else if (WorkStation.Cursor == Cursors.Cross && re == true && fullRect == true)
            {
                WorkStation.Children.Remove(helpRectangle);
                p2 = e.GetPosition(WorkStation);
                helpRectangle.Width = Math.Abs(p2.X - p1.X);
                helpRectangle.Height = helpRectangle.Width;
                helpRectangle.Stroke = new SolidColorBrush(Colors.Gray);
                helpRectangle.StrokeThickness = 2;
                if (p1.X > p2.X && p1.Y > p2.Y)
                {
                    helpRectangle.Margin = new Thickness(p2.X, p2.Y, p1.X, p1.Y);
                    WorkStation.Children.Add(helpRectangle); ;
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
                    WorkStation.Children.Add(helpRectangle); ;
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
            else if (WorkStation.Cursor == Cursors.Cross && roRe == true && fullRect == true)
            {
                WorkStation.Children.Remove(helpRoundedRectangle);
                p2 = e.GetPosition(WorkStation);
                helpRoundedRectangle.Width = Math.Abs(p2.X - p1.X);
                helpRoundedRectangle.RadiusX = 40;
                helpRoundedRectangle.RadiusY = 40;
                helpRoundedRectangle.Height = helpRoundedRectangle.Width;
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
                helpInd++;
                index++;
                PagesUpload(hintPage, line);
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
                line.Stroke = back;
                line.StrokeThickness = hod;
                WorkStation.Children.Add(line);
                elements[index] = line;
                guma.Add(line);
                helpInd++;
                index++;
                PagesUpload(hintPage, line);
            }
        }

        private void Paint_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Pen;
            SliderTl.Maximum = 10;
            darkEff = false;
            highlighterBool = false;
            highlighterBoolHelp = false;
            dashedLine = false;
        }
        private void ArrowBtn_Click(object sender, RoutedEventArgs e)
        {
            arrow = true;
            WorkStation.Cursor = Cursors.Cross;
            ellep = false;
            recta = false;
            re = false;
            el = false;
            line = false;
            roundedRe = false;
            roRe = false;
            tr = false;
            triangle = false;
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
                image.Name = "img" + helpInd;
                image.MouseDown += new MouseButtonEventHandler(Image_MouseDown);
                WorkStation.Children.Add(image);
                elements[index] = image;
                helpInd++;
                index++;
                PagesUpload(hintPage, image);
            }
            lblHistory.Items.Add("Uploading image...");
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            if (WorkStation.Cursor == Cursors.SizeAll)
            {
                WorkStation.Children.Remove(helpMovingImage);
                moveImageBool = true;
                helpMovingImage.Source = img.Source;
                helpMovingImage.Width = img.Width;
                helpMovingImage.Height = img.Height;
                WorkStation.Children.Remove(img);
                UIElement[] uIElement = SearchElement(hintPage);
                int i = Array.IndexOf(elements, img);
                int k = Array.IndexOf(uIElement, img);
                for (int y = i; y < elements.Length - 1; y++)
                {
                    elements[y] = elements[y + 1];
                }
                for (int y = k; y < uIElement.Length - 1; y++)
                {
                    uIElement[y] = uIElement[y + 1];
                }
                WorkStation.Children.Add(helpMovingImage);
                lblHistory.Items.Add("Only BETA version.");
            }
            else
            {
                lblHistory.Items.Add("If you want to move wih image, click on Moving button.");
            }
        }
        private void tb_MouseDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            TextBox tbox = new TextBox();
            TitleProject.Content = "asd";
            if (e.Key == Key.LeftCtrl)
            {
                if (WorkStation.Cursor == Cursors.SizeAll)
                {
                    moveText = true;
                    tbox = tb;
                    tBoxs = tbox;
                    WorkStation.Children.Remove(tb);
                    WorkStation.Children.Add(tbox);
                }
                else
                {
                    lblHistory.Items.Add("If you want to move wih text, click on Moving button.");
                }
            }
        }
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse el = (Ellipse)sender;
            if (WorkStation.Cursor == Cursors.Arrow && fillBool == true)
            {
                System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
                System.Windows.Forms.DialogResult res = cd.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    el.Fill = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
                }
            }
            else if (WorkStation.Cursor == Cursors.SizeAll)
            {
                Ellipse blRe = new Ellipse();
                blRe.Stroke = new SolidColorBrush(Colors.LightBlue);
                blRe.StrokeThickness = 2;
                blRe.Height = el.Height;
                blRe.Width = el.Width;
                blRe.Margin = el.Margin;
                blRe.Opacity = 0.5;
                blElBool = true;
                externalFill = el.Fill;
                ExternalBrush = el.Stroke;
                externalTl = Convert.ToInt32(el.StrokeThickness);
                lblHistory.Items.Add("Moving with ellipse.");
                movedEllipse = blRe;
                UIElement[] iElement = SearchElement(hintPage);
                WorkStation.Children.Add(movedEllipse);
                int i = Array.IndexOf(elements, el);
                for (int y = i; y < elements.Length - 1; y++)
                {
                    elements[y] = elements[y + 1];
                }
                int k = Array.IndexOf(iElement, el);
                for (int y = k; y < iElement.Length - 1; y++)
                {
                    iElement[y] = iElement[y + 1];
                }
                WorkStation.Children.Remove(el);
            }
            else if (WorkStation.Cursor == Cursors.Arrow)
            {
                Ellipse blRe = new Ellipse();
                blRe.Stroke = new SolidColorBrush(Colors.LightBlue);
                blRe.StrokeThickness = 2;
                blRe.Height = el.Height;
                blRe.Width = el.Width;
                blRe.Margin = el.Margin;
                blRe.Opacity = 0.5;
                blElBool = true;
                externalFill = el.Fill;
                ExternalBrush = el.Stroke;
                externalTl = Convert.ToInt32(el.StrokeThickness);
                lblHistory.Items.Add("Inherit ellipse.");
                movedEllipse = blRe;
                WorkStation.Children.Add(movedEllipse);
            }
        }
        private void Rec_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rec = (Rectangle)sender;
            if (WorkStation.Cursor == Cursors.Arrow && fillBool == true)
            {
                System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
                System.Windows.Forms.DialogResult res = cd.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    rec.Fill = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
                }
            }
            else if (WorkStation.Cursor == Cursors.SizeAll)
            {
                Rectangle blRe = new Rectangle();
                blRe.Stroke = new SolidColorBrush(Colors.Gray);
                blRe.StrokeThickness = 2;
                blRe.Height = rec.Height;
                blRe.RadiusX = rec.RadiusX;
                blRe.RadiusY = rec.RadiusY;
                blRe.Width = rec.Width;
                blRe.Margin = rec.Margin;
                blRe.Opacity = 0.5;
                blReBool = true;
                externalFill = rec.Fill;
                ExternalBrush = rec.Stroke;
                externalTl = Convert.ToInt32(rec.StrokeThickness);
                lblHistory.Items.Add("Moving with rectangle.");
                movedRect = blRe;
                WorkStation.Children.Add(movedRect);
                UIElement[] iElement = SearchElement(hintPage);
                int i = Array.IndexOf(elements, rec);
                for (int y = i; y < elements.Length - 1; y++)
                {
                    elements[y] = elements[y+1];
                }
                int k = Array.IndexOf(iElement, rec);
                for (int y = k; y < iElement.Length - 1; y++)
                {
                    iElement[y] = iElement[y + 1];
                }
                WorkStation.Children.Remove(rec);
            }
            else if (WorkStation.Cursor == Cursors.Arrow)
            {
                Rectangle blRe = new Rectangle();
                blRe.Stroke = new SolidColorBrush(Colors.LightBlue);
                blRe.StrokeThickness = 2;
                blRe.Height = rec.Height;
                blRe.RadiusX = rec.RadiusX;
                blRe.RadiusY = rec.RadiusY;
                blRe.Width = rec.Width;
                blRe.Margin = rec.Margin;
                blRe.Opacity = 0.5;
                blReBool = true;
                externalFill = rec.Fill;
                ExternalBrush = rec.Stroke;
                externalTl = Convert.ToInt32(rec.StrokeThickness);
                lblHistory.Items.Add("Inherit rectangle.");
                movedRect = blRe;
                WorkStation.Children.Add(movedRect);
            }
        }
        private void Pol_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Polygon pol = (Polygon)sender;
            if (WorkStation.Cursor == Cursors.Arrow && fillBool == true)
            {
                System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
                System.Windows.Forms.DialogResult res = cd.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    pol.Fill = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
                }
            }
        }
        private void MouseBack_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Arrow;
            fillBool = false;
            lblHistory.Items.Add("Normal mouse is using.");
        }

        private void Guma_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Hand;
            lblHistory.Items.Add("Using rubber");
                        darkEff = false;
            highlighterBool = false;
            highlighterBoolHelp = false;
            dashedLine = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (loadPr == true)
            {
                SaveSubmit.Visibility = Visibility.Visible;
                nameOfFile.Visibility = Visibility.Visible;
                SaveDifferentWay.Visibility = Visibility.Visible;
                efectPanel.Visibility = Visibility.Hidden;
                lblHistory.Items.Add("Save file...");
                nameOfFile.Text = nazev;
            }
            else
            {
            SaveSubmit.Visibility = Visibility.Visible;
            nameOfFile.Visibility = Visibility.Visible;
            SaveDifferentWay.Visibility = Visibility.Visible;
            efectPanel.Visibility = Visibility.Hidden;
            lblHistory.Items.Add("Save file...");
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image obr(*.obr) | *.obr | Image jpeg(*.jpg) | *.jpg | Image png(*.png) | *.png | Image PNG(*PNG) | *PNG";
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
            helpInd++;
            index = 0;
            lblHistory.Items.Add("Clear All Items");
            lblHistory.Items.Add("Work station is clear.");
        }
        //Krok zpět
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
                    if (elements[i] == null)
                    {

                    }
                    else
                    {
                        try
                        {
                            WorkStation.Children.Add(elements[i]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    }
                }
                index = index - backDelet;
                lblHistory.Items.Add("Removing elments.");
            }
        }

        private void DeleteHalf_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            backDelet = Convert.ToInt32(DeleteHalf.Value);
        }
        //Krok dopředu
        private void ForwaBack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            forwBack = Convert.ToInt32(ForwaBack.Value);
        }
        //Text move
        private void MovedBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.SizeAll;
        }

        private void ForwStep_Click(object sender, RoutedEventArgs e)
        {
            if ((index + forwBack) >= helpInd)
            {
                WorkStation.Children.Clear();
                index = helpInd;
                for (int i = 0; i < index; i++)
                {
                    if (elements[i] == null)
                    {

                    }
                    else
                    {
                        try
                        {
                            WorkStation.Children.Add(elements[i]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    }                    
                }
            }
            else
            {
                WorkStation.Children.Clear();
                for (int i = 0; i < index + forwBack; i++)
                {
                    if (elements[i] == null)
                    {

                    }
                    else
                    {
                        try
                        {
                            WorkStation.Children.Add(elements[i]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error");
                        }
                    }
                }
                index = index + forwBack;
                lblHistory.Items.Add("Adding elments.");
            }
        }

        private void SaveSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (nameOfFile.Text != null)
            {
                if (loadPr == true)
                {
                    if (savePlease == true)
                    {
                        pathSecondWay = @"C:\OBR\GrafickyEditor\GrafickyEditor\Project\Debug\" + nazev;
                        FileStream fs = new FileStream(pathSecondWay, FileMode.Create);
                        RenderTargetBitmap bmp = new RenderTargetBitmap((int)WorkStation.ActualWidth,
                            (int)WorkStation.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                        bmp.Render(WorkStation);
                        BitmapEncoder encoder = new TiffBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bmp));
                        encoder.Save(fs);
                        fs.Close();
                        SaveSubmit.Visibility = Visibility.Hidden;
                        nameOfFile.Visibility = Visibility.Hidden;
                        SaveDifferentWay.Visibility = Visibility.Hidden;
                        lblHistory.Items.Add("File was saved.");
                        TitleProject.Content = nameOfFile.Text + " - GPB";
                    }
                    else
                    {
                        pathSecondWay = @"C:\OBR\GrafickyEditor\GrafickyEditor\Project\Debug\" + "a" + nazev;
                        FileStream fs = new FileStream(pathSecondWay, FileMode.Create);
                        RenderTargetBitmap bmp = new RenderTargetBitmap((int)WorkStation.ActualWidth,
                            (int)WorkStation.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                        bmp.Render(WorkStation);
                        BitmapEncoder encoder = new TiffBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bmp));
                        encoder.Save(fs);
                        fs.Close();
                        SaveSubmit.Visibility = Visibility.Hidden;
                        nameOfFile.Visibility = Visibility.Hidden;
                        SaveDifferentWay.Visibility = Visibility.Hidden;
                        lblHistory.Items.Add("File was saved.");
                        TitleProject.Content = nameOfFile.Text + " - GPB";
                    }
                }
                else
                {
                    string path = @"C:\OBR\GrafickyEditor\GrafickyEditor\Project\Debug\";
                    if (File.Exists(path)){}
                    else
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }
                    nazev = nameOfFile.Text + ".obr";
                    string full = path + nazev;
                    FileStream fs = new FileStream(full, FileMode.Create);
                    RenderTargetBitmap bmp = new RenderTargetBitmap((int)WorkStation.ActualWidth,
                        (int)WorkStation.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                    bmp.Render(WorkStation);
                    BitmapEncoder encoder = new TiffBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    encoder.Save(fs);
                    fs.Close();
                    SaveSubmit.Visibility = Visibility.Hidden;
                    nameOfFile.Visibility = Visibility.Hidden;
                    SaveDifferentWay.Visibility = Visibility.Hidden;
                    Stream stream = new FileStream("saves.txt", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine(nameOfFile.Text);
                    }
                    lblHistory.Items.Add("File was saved.");
                    TitleProject.Content = nazev+ " - GPB";
                    loadPr = true;
                    savePlease = true;
                }
            }
            else
            {
                lblHistory.Items.Add("Please write name.");
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
        private void RichTxtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (blockText == false)
            {
                textArea = true;
                WorkStation.Cursor = Cursors.IBeam;
                p++;
                lblHistory.Items.Add("Adding text area.");
            }
            else
            {
                MessageBox.Show("You have to enable text. - click on Disable/enable button.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            textArea = true;
        }
        private void FlavorPen_Click(object sender, RoutedEventArgs e)
        {
            darkEff = true;
            highlighterBool = false;
            highlighterBoolHelp = false;
            WorkStation.Cursor = Cursors.Pen;
            lblHistory.Items.Add("Dark pen activated.");
        }
        private void TextBTN_Click(object sender, RoutedEventArgs e)
        {
            if (blockText == false)
            {
                WorkStation.Cursor = Cursors.IBeam;
                textArea = false;
                p++;
                lblHistory.Items.Add("Adding text box.");
            }
            else
            {
                MessageBox.Show("You have to enable text. - click on Disable/enable button.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DeletAllText_Click(object sender, RoutedEventArgs e)
        {
            if (blockText == false)
            {
                if (File.Exists("pass.txt"))
                {
                    foreach (var text in texts)
                    {
                        text.IsEnabled = false;
                    }
                    foreach (var rich in richTextBoxes)
                    {
                        rich.IsEnabled = false;
                    }
                    blockText = true;
                }
                else
                {
                    Pass pass = new Pass();
                    pass.ShowDialog();
                    foreach (var text in texts)
                    {
                        text.IsEnabled = false;
                    }
                    foreach (var rich in richTextBoxes)
                    {
                        rich.IsEnabled = false;
                    }
                    blockText = true;
                }
            }
            else if (blockText == true)
            {
                Pass pass = new Pass();
                pass.ShowDialog();
                foreach (var text in texts)
                {
                    text.IsEnabled = true;
                }
                foreach (var rich in richTextBoxes)
                {
                    rich.IsEnabled = true;
                }
                blockText = false;
            }
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
            highlighterBool = false;
            brush = new SolidColorBrush(Colors.Black);
            ellep = false;
            el = false;
            li = false;
            roundedRe = false;
            roRe = false;
            tr = false;
            triangle = false;
            arrow = false;
        }

        private void EllipseEff_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Ellipse choose activated.");
            ellep = true;
            recta = false;
            highlighterBool = false;
            brush = new SolidColorBrush(Colors.Black);
            line = false;
            re = false;
            li = false;
            roundedRe = false;
            arrow = false;
            roRe = false;
            tr = false;
            triangle = false;
        }

        private void LineEff_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Line choose activated.");
            ellep = false;
            recta = false;
            highlighterBool = false;
            brush = new SolidColorBrush(Colors.Black);
            re = false;
            el = false;
            line = true;
            roundedRe = false;
            roRe = false;
            tr = false;
            triangle = false;
            arrow = false;
        }
        private void roundedRect_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Rounded rect choose activated.");
            highlighterBool = false;
            brush = new SolidColorBrush(Colors.Black);
            roundedRe = true;
            ellep = false;
            recta = false;
            re = false;
            el = false;
            line = false;
            roRe = false;
            tr = false;
            arrow = false;
            triangle = false;
        }

        private void PolygonBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Cross;
            lblHistory.Items.Add("Triangle choose activated.");
            roundedRe = false;
            highlighterBool = false;
            brush = new SolidColorBrush(Colors.Black);
            ellep = false;
            recta = false;
            re = false;
            arrow = false;
            el = false;
            line = false;
            roRe = false;
            tr = false;
            triangle = true;
        }
        private void DashedLine_Click(object sender, RoutedEventArgs e)
        {
            dashedLine = true;
            SliderTl.Value = 2;
            WorkStation.Cursor = Cursors.Pen;
            lblHistory.Items.Add("DashLine was selected.");
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
        private void HighlighterBtn_Click(object sender, RoutedEventArgs e)
        {
            highlighterBool = true;
            WorkStation.Cursor = Cursors.Pen;
            lblHistory.Items.Add("Highlighter is using.");
        }

        private void MinNorBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //2x kliknutí na text
        private void DynamicTb_DoubleClick(object sender, EventArgs e)
        {
            lblHistory.Items.Add("Editing text....");
            TextBox tb = (TextBox)sender;
            System.Windows.Forms.FontDialog fd = new System.Windows.Forms.FontDialog();
            System.Windows.Forms.DialogResult res = fd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                tb.FontFamily = new FontFamily(fd.Font.Name);
                tb.FontSize = fd.Font.Size * 96.0 / 72.0;
                tb.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                tb.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                TextDecorationCollection tdc = new TextDecorationCollection();
                if (fd.Font.Underline) tdc.Add(TextDecorations.Underline);
                if (fd.Font.Strikeout) tdc.Add(TextDecorations.Strikethrough);
                tb.TextDecorations = tdc;
                lblHistory.Items.Add("Text was Edited.");
            }
            else
            {
                lblHistory.Items.Add("Text was not edited.");
            }
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
                addableBtn++;
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
            //dodělat
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
                addableBtn++;
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
                addableBtn++;
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

        private void TrUp_Click(object sender, RoutedEventArgs e)
        {
            if (addableBtn == 0)
            {
                AddableOne.Content = "Triangle";
                AddableOne.Opacity = 1;
                AddableOne.Click += new RoutedEventHandler(PolygonBtn_Click);
                lblHistory.Items.Add("Triangle was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 1)
            {
                AddableTwo.Content = "Triangle";
                AddableTwo.Opacity = 1;
                AddableTwo.Click += new RoutedEventHandler(PolygonBtn_Click);
                lblHistory.Items.Add("Triangle was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 2)
            {
                AddableThree.Content = "Triangle";
                AddableThree.Opacity = 1;
                AddableThree.Click += new RoutedEventHandler(PolygonBtn_Click);
                lblHistory.Items.Add("Triangle was added to main bar.");
                addableBtn++;
            }
            else if (addableBtn == 3)
            {
                AddableFour.Content = "Triangle";
                AddableFour.Opacity = 1;
                AddableFour.Click += new RoutedEventHandler(PolygonBtn_Click);
                lblHistory.Items.Add("Triangle was added to main bar.");
                addableBtn = 0;
            }
        }
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            WorkStation.Cursor = Cursors.Arrow;
            fillBool = true;
            lblHistory.Items.Add("Select item to be filled.");
        }
        private void Background_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult res = cd.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                back = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
                WorkStation.Background = back;
                foreach (var item in guma)
                {
                    item.Stroke = back;
                }
            }
            lblHistory.Items.Add("Filling backgound");
        }
        //Ukládání do jiného složky/dokončení projektu
        private void SaveDifferentWay_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = @"E:\VS2019WPF\GrafickyEditor\GrafickyEditor\bin\Debug";
            save.Filter = "Image obr(*.obr) | *.obr | Image jpeg(*.jpg) | *.jpg | Image png(*.png) | *.png";
            if (nameOfFile != null)
            {
                save.FileName = nameOfFile.Text;
            }
            else
            {
                save.FileName = "name.obr";
            }
            if (save.ShowDialog() == true)
            {
                FileStream fs = new FileStream(save.FileName, FileMode.Create);
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)WorkStation.ActualWidth,
                    (int)WorkStation.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);
                bmp.Render(WorkStation);
                BitmapEncoder encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();
            }
            SaveSubmit.Visibility = Visibility.Hidden;
            SaveDifferentWay.Visibility = Visibility.Hidden;
            nameOfFile.Visibility = Visibility.Hidden;
            lblHistory.Items.Add("File was saved by " + nameOfFile.Text + ".");
        }
        //Klávesové zkratky
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                Save_Click(sender, e);
            }
            else if (e.Key == Key.LeftCtrl && ellep == true)
            {
                fullel = true;
                TitleProject.Content = "Circle activated.";
            }
            else if (e.Key == Key.LeftCtrl && blElBool == true || blPoBool == true || blReBool == true)
            {
                holdPos = true;
                TitleProject.Content = "Holding positions.";
            }
            else if (e.Key == Key.LeftCtrl)
            {
                fullRect = true;
                TitleProject.Content = "CTRL activated.";
            }
            else if (e.Key == Key.Escape)
            {
                line = false;
                recta = false;
                roundedRe = false;
                roRe = false;
                ellep = false;
                fullRect = false;
                mal = false;
                li = false;
                el = false;
                re = false;
                dark = false;
                fullel = false;
                triangle = false;
                tr = false;
                loadPr = false;
                fillBool = false;
                blReBool = false;
                blElBool = false;
                blPoBool = false;
                moveText = false;
                holdPos = false;
                highlighterBool = false;
                highlighterBoolHelp = false;
                moveImageBool = false;
                dashedLine = false;
                helpDashedLine = false;
                arrow = false;
                helpArrow = false;
                blockText = false;
                textArea = false;
                textAreaSec = false;
                savePlease = false;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                fullel = false;
                fullRect = false;
                holdPos = false;
                TitleProject.Content = jmeno;
            }
        }

        public void Page1_Click(object sender, RoutedEventArgs e) 
        {
            Button btn = (Button)sender;
            hintPage = Convert.ToInt32(btn.Content);
            infoProj.Content = "Page " + hintPage;
            WorkStation.Children.Clear();
            if (hintPage == 0)
            {
                for (int i = 0; i < elements0.Length - 1; i++)
                {
                    if (elements0[i] != null)
                    {
                        WorkStation.Children.Add(elements0[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 1)
            {
                for (int i = 0; i < elements1.Length - 1; i++)
                {
                    if (elements1[i] != null)
                    {
                        WorkStation.Children.Add(elements1[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 2)
            {
                for (int i = 0; i < elements2.Length - 1; i++)
                {
                    if (elements2[i] != null)
                    {
                        WorkStation.Children.Add(elements2[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 3)
            {
                for (int i = 0; i < elements3.Length - 1; i++)
                {
                    if (elements3[i] != null)
                    {
                        WorkStation.Children.Add(elements3[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 4)
            {
                for (int i = 0; i < elements4.Length - 1; i++)
                {
                    if (elements4[i] != null)
                    {
                        WorkStation.Children.Add(elements4[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 5)
            {
                for (int i = 0; i < elements5.Length - 1; i++)
                {
                    if (elements5[i] != null)
                    {
                        WorkStation.Children.Add(elements5[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 6)
            {
                for (int i = 0; i < elements6.Length - 1; i++)
                {
                    if (elements6[i] != null)
                    {
                        WorkStation.Children.Add(elements6[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 7)
            {
                for (int i = 0; i < elements7.Length - 1; i++)
                {
                    if (elements7[i] != null)
                    {
                        WorkStation.Children.Add(elements7[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 8)
            {
                for (int i = 0; i < elements8.Length - 1; i++)
                {
                    if (elements8[i] != null)
                    {
                        WorkStation.Children.Add(elements8[i]);
                    }
                    else { }
                }
            }
            else if (hintPage == 9)
            {
                for (int i = 0; i < elements9.Length - 1; i++)
                {
                    if (elements9[i] != null)
                    {
                        WorkStation.Children.Add(elements9[i]);
                    }
                    else { }
                }
            }
        }

        public void PagesUpload(int hint, UIElement element)
        {
            if (hint == 0)
            {
                elements0[index1] = element;
                index1++;
            }
            else if (hint == 1)
            {
                elements1[index2] = element;
                index2++;
            }
            else if (hint == 2)
            {
                elements2[index3] = element;
                index3++;
            }
            else if (hint == 3)
            {
                elements3[index4] = element;
                index4++;
            }
            else if (hint == 4)
            {
                elements4[index5] = element;
                index5++;
            }
            else if (hint == 5)
            {
                elements5[index6] = element;
                index6++;
            }
            else if (hint == 6)
            {
                elements6[index7] = element;
                index7++;
            }
            else if (hint == 7)
            {
                elements7[index8] = element;
                index8++;
            }
            else if (hint == 8)
            {
                elements8[index9] = element;
                index9++;
            }
            else if (hint == 9)
            {
                elements9[index10] = element;
                index10++;
            }
        }

        public UIElement[] SearchElement(int hint)
        {
            if (hint == 0)
                return elements0;
            else if (hint == 1)
                return elements1;
            else if (hint == 2)
                return elements2;
            else if (hint == 3)
                return elements3;
            else if (hint == 4)
                return elements4;
            else if (hint == 5)
                return elements5;
            else if (hint == 6)
                return elements6;
            else if (hint == 7)
                return elements7;
            else if (hint == 8)
                return elements8;

            return elements9;
        }
    }
    public class Pages
    {
        public List<UIElement> Element = new List<UIElement>();
        public bool Checked { get; set; }
        public bool CheckIfCanBeSaveByName(string nameOfFile)
        {
            //Stejná metoda jako je v kódu nad ním, ale tato slouží pro testování
            SaveFileDialog save = new SaveFileDialog();
            save.InitialDirectory = @"E:\VS2019WPF\GrafickyEditor\GrafickyEditor\bin\Debug";
            save.Filter = "Image obr(*.obr) | *.obr | Image jpeg(*.jpg) | *.jpg | Image png(*.png) | *.png";
            if (nameOfFile != null)
            {
                if (nameOfFile.Contains(".obr"))
                {
                    save.FileName = nameOfFile;
                }
                else
                    save.FileName = nameOfFile + ".obr";
            }
            else
            {
                save.FileName = "name.obr";
            }
            if (save.FileName == nameOfFile + ".obr" || save.FileName == nameOfFile + ".png" || save.FileName == nameOfFile + ".jpg")
            {
                return true;
            }
            return false;
        }
        public Pages(bool Checked)
        {
            this.Checked = Checked;
        }
        public Pages() {}
    }
}
