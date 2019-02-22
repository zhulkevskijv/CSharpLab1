using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab_01.Tools;

namespace Lab_01.ViewModels
{
    internal class HoroscopeViewModel : BaseViewModel
    {
        #region Fields

        private string _westHoroscope;
        private string _chineseHoroscope;
        private string _date;
        private string _age;

        #region Commands

        private RelayCommand<object> _submitDateCommand;

        #endregion

        #endregion

        #region Properties

        public string WestHoroscope
        {
            get => _westHoroscope;
            set
            {
                _westHoroscope = value;
                OnPropertyChanged();
            }
        }

        public string ChineseHoroscope
        {
            get => _chineseHoroscope;
            set
            {
                _chineseHoroscope = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> SubmitDateCommand
        {
            get
            {
                return _submitDateCommand ??
                       (_submitDateCommand = new RelayCommand<object>(CalculateDate, o => CanExecuteCommand()));
            }
        }

        #endregion

        public bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_date);
        }

        private async void CalculateDate(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Thread.Sleep(2000));
            LoaderManager.Instance.HideLoader();
            int age = CalculateAge();
            if (age > 135)
            {
                ShowErrorMessageBox("You're too old to be alive.");
                return;
            }

            if (age < 0)
            {
                ShowErrorMessageBox("You aren't born yet");
                return;
            }
            Age = age + "";
            ChineseHoroscope = CalculateChineseHoroscope();
            WestHoroscope = CalculateWestHoroscope();
            if (Convert.ToDateTime(Date).Day == DateTime.Today.Day &&
                Convert.ToDateTime(Date).Month == DateTime.Today.Month)
                MessageBox.Show("Happy birthday! ;)");
        }

        #region AdditionalMethodsForCalculating
        private void ShowErrorMessageBox(string message)
        {
            Age = "";
            WestHoroscope = "";
            ChineseHoroscope = "";
            MessageBox.Show(message);
        }
        private int CalculateAge()
        {
            DateTime date = Convert.ToDateTime(Date);
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - date.Year;
            if (date.Month > currentDate.Month)
                return age - 1;
            if (date.Month == currentDate.Month)
            {
                if (date.Day > currentDate.Day)
                    return age - 1;
                if (date.Day < currentDate.Day)
                    return age;
                if (date.Day == currentDate.Day)

                    return age;
            }

            return age;
        }

        private string CalculateWestHoroscope()
        {
            var date = Convert.ToDateTime(Date);
            if ((date.Day >= 21 && date.Month == 3) || (date.Day <= 20 && date.Month == 4))
                return "Aries";
            if ((date.Day >= 21 && date.Month == 4) || (date.Day <= 22 && date.Month == 5))
                return "Taurus";
            if ((date.Day >= 22 && date.Month == 5) || (date.Day <= 21 && date.Month == 6))
                return "Gemini";
            if ((date.Day >= 22 && date.Month == 6) || (date.Day <= 22 && date.Month == 7))
                return "Cancer";
            if ((date.Day >= 23 && date.Month == 7) || (date.Day <= 22 && date.Month == 8))
                return "Leo";
            if ((date.Day >= 23 && date.Month == 8) || (date.Day <= 23 && date.Month == 9))
                return "Virgo";
            if ((date.Day >= 24 && date.Month == 9) || (date.Day <= 23 && date.Month == 10))
                return "Libra";
            if ((date.Day >= 24 && date.Month == 10) || (date.Day <= 22 && date.Month == 11))
                return "Scorpio";
            if ((date.Day >= 23 && date.Month == 11) || (date.Day <= 21 && date.Month == 12))
                return "Sagittarius";
            if ((date.Day >= 22 && date.Month == 12) || (date.Day <= 20 && date.Month == 1))
                return "Capricorn";
            if ((date.Day >= 21 && date.Month == 1) || (date.Day <= 19 && date.Month == 2))
                return "Aquarius";
            return "Pisces";
        }

        private string CalculateChineseHoroscope()
        {
            var remainder = Convert.ToDateTime(Date).Year % 12;
            switch (remainder)
            {
                case 0:
                    return "Monkey";
                case 1:
                    return "Rooster";
                case 2:
                    return "Dog";
                case 3:
                    return "Pig";
                case 4:
                    return "Rat";
                case 5:
                    return "Ox";
                case 6:
                    return "Tiger";
                case 7:
                    return "Rabbit";
                case 8:
                    return "Dragon";
                case 9:
                    return "Snake";
                case 10:
                    return "Horse";
                case 11:
                    return "Goat";
                default:
                    return "Lol";
            }
        }


        #endregion

    }
}