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
using WeatherTracker.Models;

namespace WeatherTracker.ViewModels
{
    public class CitiesListViewModel : DependencyObject
    {

        public DelegateCommand<object> bDelete_Click { get; private set; }
        public DelegateCommand<object> bOn_Click { get; private set; }
        public DelegateCommand<object> bOff_Click { get; private set; }
        public DelegateCommand<object> ItemDoubleClick { get; private set; }
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
                    DB_address.AddCity(CityName);
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
            bDelete_Click = new DelegateCommand<object>((obj) => { OnDelete_Click(obj); });
            bOn_Click = new DelegateCommand<object>((obj) => { OnbOn_Click(obj); });
            bOff_Click = new DelegateCommand<object>((obj) => { OnbOff_Click(obj); });
            CitiesList = DB_address.GetAllCities();
        }

        private void OnbOff_Click(object obj)
        {
            if (obj is Data.City _obj)
            {
                DB_address.SetActual(_obj?.name, false);
                CitiesList = DB_address.GetAllCities();
            }
        }

        private void OnbOn_Click(object obj)
        {
            if (obj is Data.City _obj)
            {
                DB_address.SetActual(_obj?.name, true);
                CitiesList = DB_address.GetAllCities();
            }
        }
        private void OnDelete_Click(object obj)
        {
            if(obj is Data.City _obj)
            {
                DB_address.DeleteCity(_obj?.name);
                CitiesList = DB_address.GetAllCities();
            }
        }
    }
}
