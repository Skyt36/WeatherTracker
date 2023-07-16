using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WeatherTracker.ViewModels
{
    class CurrentCityViewModel : DependencyObject
    {
        private int id_city;
        private IQueryable<Data.Weather> info;

        public DateTime DateTimeStart
        {
            get { return (DateTime)GetValue(DateTimeStartProperty); }
            set { SetValue(DateTimeStartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateTimeStart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeStartProperty =
            DependencyProperty.Register("DateTimeStart", typeof(DateTime), typeof(CurrentCityViewModel), new PropertyMetadata(DateTime.Now));

        public DateTime DateTimeEnd
        {
            get { return (DateTime)GetValue(DateTimeEndProperty); }
            set { SetValue(DateTimeEndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateTimeEnd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeEndProperty =
            DependencyProperty.Register("DateTimeEnd", typeof(DateTime), typeof(CurrentCityViewModel), new PropertyMetadata(DateTime.Now));


        public string CityInfo
        {
            get { return (string)GetValue(CityInfoProperty); }
            set { SetValue(CityInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CityInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CityInfoProperty =
            DependencyProperty.Register("CityInfo", typeof(string), typeof(CurrentCityViewModel), new PropertyMetadata(""));


        public ICollection<Data.Weather> currentInfo
        {
            get { return (ICollection<Data.Weather>)GetValue(currentInfoProperty); }
            set { SetValue(currentInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentInfoProperty =
            DependencyProperty.Register("currentInfo", typeof(ICollection<Data.Weather>), typeof(CurrentCityViewModel), new PropertyMetadata(null));



        public ICommand bShow
        {
            get
            {
                return new RelayCommand(() =>
                {
                    currentInfo = info.Where(x => x.last_update >= DateTimeStart && x.last_update <= DateTimeEnd).OrderByDescending(x => x.last_update).ToList();
                });
            }
        }
        public CurrentCityViewModel()
        {
        }

        public CurrentCityViewModel(int id_city)
        {
            this.id_city = id_city;
            info = Services.DB_address.GetInfo(id_city);
            var name = Services.DB_address.GetName(id_city);
            var last_update = info.Max(x => x.last_update);
            DateTimeStart = info.Min(x => x.last_update);
            DateTimeEnd = last_update;
            CityInfo = "Город " + name + " Последнее обновление " + last_update;
        }
    }
}
