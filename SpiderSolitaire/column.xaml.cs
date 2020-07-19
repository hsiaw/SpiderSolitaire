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
    /// Interaction logic for column.xaml
    /// </summary>
    public partial class column : UserControl
    {
        public List<Cards.Card> cards = new List<Cards.Card>();
        private Canvas can;

        public column()
        {
            InitializeComponent();
            this.Loaded += Column_Loaded;
            can = new Canvas();
            this.Content = can;
            //Canvas current = 

            this.Drop += (o, e) =>
            {
                var from = (Cards.Card)e.Data.GetData(typeof(Cards.Card));
                //if(from.cvalue == cards.las)
                playingArea.instance.move(from, this);   

            };

        }

        private void Column_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        public void add(Cards.Card x)
        {
            cards.Add(x);
            refresh();
        }

        public void remove(Cards.Card x)
        {

            cards.Remove(x);
            
            refresh();
        }

        public void refresh()
        {
            int height = 0;

            can.Children.Clear();

            foreach (Cards.Card c in cards)
            {
                can.Children.Add(c);
                height += 30;
                Canvas.SetTop(c, height);
            }
            this.Content = can;
        }

        public bool isin(Cards.Card x)
        {
            return cards.Contains(x);
        }
    }
}
 