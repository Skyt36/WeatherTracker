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

namespace WeatherTracker.Views
{
    /// <summary>
    /// Логика взаимодействия для CurrentCityView.xaml
    /// </summary>
    public partial class CurrentCityView : UserControl
    {

        public CurrentCityView()
        {
            InitializeComponent();
            DataContext = new ViewModels.CurrentCityViewModel();
        }

        public CurrentCityView(int id_city)
        {
            InitializeComponent();
            DataContext = new ViewModels.CurrentCityViewModel(id_city);
        }
    }
}
