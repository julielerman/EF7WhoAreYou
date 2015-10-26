using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CookieCounterApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        private BingeViewModel _binge;
        public MainPage()
        {
            this.InitializeComponent();
            _binge = new BingeViewModel();
            NomText.Visibility = Visibility.Collapsed;
            // Refresh bing list after a new binge is added
            _binge.BingeCompleted += (s) =>
            { ReloadHistory(); };
            this.BingePane.DataContext = _binge;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadHistory();
        }
        private void ReloadHistory()
        {
            this.BingeList.ItemsSource = BingeService.GetLast5Binges();
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {

            _binge.HandleClick();

        }
        private void Play_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _binge.StartNewBinge();
        }

        private void AddCookies_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WorthIt_Click(object sender, RoutedEventArgs e)
        {
            _binge.StoreBinge(true);
        }

        private void NotWorthIt_Click(object sender, RoutedEventArgs e)
        {
            _binge.StoreBinge(false);
        }



        private void Image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var pt = e.GetCurrentPoint(this.MainGrid);
            NomText.Margin = new Thickness(pt.Position.X, pt.Position.Y, 0, 0);


            NomText.Visibility = Visibility.Visible;
            _binge.HandleClick();
        }

        private void Image_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            NomText.Visibility = Visibility.Collapsed;
        }


    }
}
