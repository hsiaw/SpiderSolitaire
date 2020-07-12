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
            Width = 1600;
            Height = 800;
            Title = "Spider Solitaire";
        }

        private void newGame(object sender, RoutedEventArgs e)
        {
            var canvas = new Canvas();

            canvas.Children.Add(new Label() { Content = "Wendy's Spider Solitaire" });
            this.Content = canvas;

            var x = new Cards.Card(4);
            var y = new Cards.Card(5);
            canvas.Children.Add(x);
            canvas.Children.Add(y);
            Canvas.SetLeft(x, 400);

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
