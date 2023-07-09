using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeatherTracker.Models;
using WeatherTracker.Data;
using System.Windows.Input;

namespace WeatherTracker.ViewModels
{
    public class MainViewModel: DependencyObject
    {
        UserControl CitiesList;
        UserControl CurrentCity;
        public DelegateCommand<object> bOpen { get; private set; }

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
            Services.DB_address.UpdateWeather();
            CitiesList = new Views.CitiesListView();
            CurrentPage = CitiesList;
            bOpen = new DelegateCommand<object>((obj) => { OnbOpen(obj); });
        }

        private void OnbOpen(object obj)
        {
            if(obj is ViewModels.MainViewModel _obj)
            {
                if(_obj.CurrentPage is Views.CitiesListView citiesListVies)
                {
                    Data.City currentCity = (Data.City)citiesListVies.dataGrid_CitiesList.SelectedItem;
                    citiesListVies.dataGrid_CitiesList.UnselectAll();
                    if (currentCity != null)
                    {
                        CurrentCity = new Views.CurrentCityView(currentCity.id_city);
                        CurrentPage = CurrentCity;
                    }
                }
            }
        }
        public ICommand bBack
        {
            get
            {
                return new RelayCommand(() =>
                {
                    CurrentPage = CitiesList;
                });
            }
        }


    }
}
