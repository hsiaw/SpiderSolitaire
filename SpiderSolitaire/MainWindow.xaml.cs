using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace SpiderSolitaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int _hghtCard = 150;
        public static int _wdthCard = 130;
        public MainWindow()
        {
            InitializeComponent();
            this.Width = Properties.Settings.Default.Size.Width;
            this.Height = Properties.Settings.Default.Size.Height;
            this.Top = Properties.Settings.Default.Position.Height;
            this.Left = Properties.Settings.Default.Position.Width;

            this.Closed += (o, e) =>
            {
                Properties.Settings.Default.Size = new System.Drawing.Size((int)this.Width, (int)this.Height);
                Properties.Settings.Default.Position = new System.Drawing.Size((int)this.Left, (int)this.Top);
                Properties.Settings.Default.Save();
            };

            Title = "Spider Solitaire";
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.btnNew.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void newGame(object sender, RoutedEventArgs e)
        {
            
            this.playingArea.Content = new playingArea();

            

        }

    }
}
