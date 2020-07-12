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
        private const int CARDBACK = 0;
        public int clicked;
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
            var canvas = new Canvas();
            //this.playingArea = new playingArea();
            
            this.playingArea.Content = new playingArea();

            //canvas.Children.Add(new Label() { Content = "Wendy's Spider Solitaire" });



            int[] deck = new int[104];
            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = i;
            }

            shuffle(deck);

        }

        private static void shuffle(int[] d)
        {
            Random rand = new Random();
            for (int i = 0; i < d.Length; i++)
            {
                int r = i + rand.Next(d.Length - i);

                int temp = d[r];
                d[r] = d[i];
                d[i] = temp;
            }
        }

        

        public class node
        {
            public node next;
            public node prev;
            public int value;
            public node()
            {
                next = null;
                prev = null;
                value = 0;
            }
            public node(int x)
            {
                next = null;
                prev = null;
                value = x;
            }
        }

        public class column
        {
            node head;
            node tail;
            public column()
            {
                head = null;
                tail = null;
            }

            public void add(int x)
            {
                node n = new node(x);
                tail.next = n;
                tail = n;
            }
        }
    }
}
