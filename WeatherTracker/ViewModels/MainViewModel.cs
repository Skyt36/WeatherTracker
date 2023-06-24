using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeatherTracker.ViewModels
{
    public class MainViewModel: DependencyObject
    {
        UserControl CitiesList;
        UserControl CurrentCity;


        public UserControl CurrentPage
        {
            get { return (UserControl)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(UserControl), typeof(MainViewModel), new PropertyMetadata(null));


        public MainViewModel()
        {
            CitiesList = new Views.CitiesListView();
            CurrentPage = CitiesList;
        }
    }
}
