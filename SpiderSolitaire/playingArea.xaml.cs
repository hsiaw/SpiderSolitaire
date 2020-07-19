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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpiderSolitaire
{
    /// <summary>
    /// Interaction logic for playingArea.xaml
    /// </summary>
    public partial class playingArea : UserControl
    {
        public static playingArea instance;
        private const int CARDBACK = 0;
        column[] cardColumns = new column[10];
        public playingArea()
        {
            InitializeComponent();
            instance = this;
            this.Loaded += PlayingArea_Loaded;
        }

        private void PlayingArea_Loaded(object sender, RoutedEventArgs e)
        {
            
            int _hghtCard = 150;
            int _wdthCard = 130;

            var c = new Canvas();
            this.Content = c;
            for (int i = 0; i < cardColumns.Length; i++)
            {
                cardColumns[i] = new column();
                c.Children.Add(cardColumns[i]);
                Canvas.SetLeft(cardColumns[i], i * (_wdthCard + 10));
            }



            int[] deck = new int[104];
            for (int i = 0; i < deck.Length; i++)
            {
                deck[i] = i;
            }

            shuffle(deck);

            int cvalue;
            for(int i = 0; i < 54; i++)
            {
                cvalue = deck[i] % 52;
                cardColumns[i % 10].cards.Add(new Cards.Card(cvalue));
            }

         }


        public void move(Cards.Card c, column dest)
        {   
            foreach (column x in cardColumns)
            {
                if (x.isin(c))
                {
                    x.remove(c);
                }
            }
            dest.add(c);        
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
    }
}
