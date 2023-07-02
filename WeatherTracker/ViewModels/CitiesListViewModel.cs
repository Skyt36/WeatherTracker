using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeatherTracker.Services;

namespace WeatherTracker.ViewModels
{
    public class CitiesListViewModel : DependencyObject
    {


        public string CityName
        {
            get { return (string)GetValue(CityNameProperty); }
            set { SetValue(CityNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CityName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CityNameProperty =
            DependencyProperty.Register("CityName", typeof(string), typeof(CitiesListViewModel), new PropertyMetadata(""));


        public ICollection<Data.City> CitiesList
        {
            get { return (ICollection<Data.City>)GetValue(CitiesListProperty); }
            set { SetValue(CitiesListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CitiesList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CitiesListProperty =
            DependencyProperty.Register("CitiesList", typeof(ICollection<Data.City>), typeof(CitiesListViewModel), new PropertyMetadata(null));


        public ICommand bAdd_Click
        {
            get
            {
                return new RelayCommand(() => {
                    DB_address.addCity(CityName);
                    CitiesList = DB_address.GetAllCities();
                });
            }
        }
        public ICommand bUpdate_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    CitiesList = DB_address.GetAllCities();
                });
            }
        }
        
        public CitiesListViewModel()
        {
            CitiesList = DB_address.GetAllCities();
        }
    }
}
