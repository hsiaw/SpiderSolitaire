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
        public column()
        {
            InitializeComponent();
            this.Loaded += Column_Loaded;

            this.Drop += (o, e) =>
            {
                playingArea.instance.move((Cards.Card)e.OriginalSource, this);   

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
            Canvas can = new Canvas();
            foreach (var c in cards)
            {
                can.Children.Add(c);
            }
            this.Content = can;
        }
    }
}
