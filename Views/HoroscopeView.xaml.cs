using System.Windows.Controls;

namespace Lab_01
{
    /// <summary>
    /// Логика взаимодействия для HoroscopeView.xaml
    /// </summary>
    public partial class HoroscopeView : UserControl
    {
        public HoroscopeView()
        {
            InitializeComponent();
            DataContext = new HoroscopeViewModel();
        }
    }
}
