using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpiderSolitaire
{
    public class Cards
    {
        public enum Suit
        {
            Clubs = 0,
            Diamonds = 1,
            Hearts = 2,
            Spades = 3
        }
        // put cards in 2 d array, suit, rank (0-12 => 2-A)
        private BitmapSource[,] _bitmapCards;
        public BitmapSource[] _bitmapCardBacks;
        private static Cards _instance;

        public static int NumCardBacks => _instance._bitmapCardBacks.Length;



        public class Card : Image, IComparable
        {
            public Cards.Suit _suit;
            public int _denom;

            public Card(int value)
            {
                int suit = value / 13;
                _suit = (Cards.Suit)suit;
                _denom = value - suit * 13;
                Source = Cards.GetCard(_suit, _denom);
                Height = MainWindow._hghtCard;
                AllowDrop = true;
                this.MouseDown += (o, e) =>
                {
                    Debug.WriteLine("here i am in mousedown");
                };
                this.MouseUp += (o, e) =>
                {
                    Debug.WriteLine("here i am in mouseup");
                };
                this.MouseMove += (o, e) =>
                {
                    DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
                };
            }

            protected override void OnDragEnter(DragEventArgs e)
            {
                base.OnDragEnter(e);
                Debug.WriteLine($"here i am in drag enter");
            }

            protected override void OnDragLeave(DragEventArgs e)
            {
                base.OnDragLeave(e);
                Debug.WriteLine($"here i am in {nameof(OnDragLeave)}");
            }

            // A=12, K=11, Q=10, J = 9. Pts = denom - 9
            public int Points { get { return _denom >= 9 ? _denom - 8 : 0; } }

            public int CompareTo(object obj)
            {
                if (obj as Card != null)
                {
                    var other = (Card)obj;
                    if (_suit == other._suit)
                    {
                        return _denom.CompareTo(other._denom);
                    }
                    return _suit.CompareTo(other._suit);
                }
                throw new InvalidOperationException();
            }
            public override string ToString()
            {
                return $"{_denom} of {_suit}";
            }
        }

        public Cards()
        {
            _bitmapCards = new BitmapSource[4, 13];
            var hmodCards = LoadLibraryEx("cards.dll", IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
            if (hmodCards == IntPtr.Zero)
            {
                throw new FileNotFoundException("Couldn't find cards.dll");
            }
            // the cards are resources from 1 - 52.
            // here is a func to load an int rsrc and return it as a BitmapSource
            Func<int, BitmapSource> GetBmpSrc = (rsrc) =>
            {
                // we first load the bitmap as a native resource, and get a ptr to it
                var bmRsrc = LoadBitmap(hmodCards, rsrc);
                // now we create a System.Drawing.Bitmap from the native bitmap
                var bmp = System.Drawing.Bitmap.FromHbitmap(bmRsrc);
                // we can now delete the LoadBitmap
                DeleteObject(bmRsrc);
                // now we get a handle to a GDI System.Drawing.Bitmap
                var hbmp = bmp.GetHbitmap();
                // we can create a WPF Bitmap source now
                var bmpSrc = Imaging.CreateBitmapSourceFromHBitmap(
                    hbmp,
                    palette: IntPtr.Zero,
                    sourceRect: Int32Rect.Empty,
                    sizeOptions: BitmapSizeOptions.FromEmptyOptions());

                // we're done with the GDI bmp
                DeleteObject(hbmp);
                return bmpSrc;
            };
            // now we call our function for the cards and the backs
            for (Suit suit = Suit.Clubs; suit <= Suit.Spades; suit++)
            {
                for (int denom = 0; denom < 13; denom++)
                {
                    // 0 -12 => 2,3,...j,q,k,a
                    int ndx = 1 + 13 * (int)suit + (denom == 12 ? 0 : denom + 1);
                    _bitmapCards[(int)suit, denom] = GetBmpSrc(ndx);
                }
            }
            //The card backs are from 53 - 65
            _bitmapCardBacks = new BitmapSource[65 - 53 + 1];
            for (int i = 53; i <= 65; i++)
            {
                _bitmapCardBacks[i - 53] = GetBmpSrc(i);
            }
        }

        /// <summary>
        /// Return a BitmapSource
        /// </summary>
        /// <param name="nSuit"></param>
        /// <param name="nDenom">0-12 = 2,3,4,J,Q,K,A</param>
        /// <returns></returns>
        public static BitmapSource GetCard(Suit nSuit, int nDenom)
        {
            if (_instance == null)
            {
                _instance = new Cards();
            }
            if (nDenom < 0 || nDenom > 12)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _instance._bitmapCards[(int)nSuit, nDenom];
        }

        internal static ImageSource GetCardBack(int i)
        {
            if (_instance == null)
            {
                _instance = new Cards();
            }
            return _instance._bitmapCardBacks[i];
        }
        public const int LOAD_LIBRARY_AS_DATAFILE = 2;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFileReserved, uint dwFlags);

        [DllImport("User32.dll")]
        public static extern IntPtr LoadBitmap(IntPtr hInstance, int uID);

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
    }
    

    
}
