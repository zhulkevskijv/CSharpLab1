using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using JetBrains.Annotations;
using Lab_01.Tools;

namespace Lab_01
{
    internal class HoroscopeViewModel:INotifyPropertyChanged
    {
        #region Fields
        private string _westHoroscope;
        private string _chineseHoroscope;
        private string _date;
        private int _age;

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

        public int Age
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
                return _submitDateCommand ?? (_submitDateCommand = new RelayCommand<object>(CalculateDate,o=>CanExecuteCommand()));
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
            Age = (DateTime.Today - Convert.ToDateTime(Date)).Days / 365;
            if (Age > 135)
                MessageBox.Show("You're too old to be alive.");
            ChineseHoroscope = CalculateChineseHoroscope();
            WestHoroscope = CalculateWestHoroscope();
        }

        private string CalculateWestHoroscope()
        {
            var date = Convert.ToDateTime(Date);
            if ((date.Day >= 21 && date.Month == 3) || (date.Day <= 20 && date.Month == 4))
                return "Aries";
            if ((date.Day >= 21 && date.Month == 4) || (date.Day <= 22 && date.Month == 5))
                return "Taurus";
            if ((date.Day >= 22 && date.Month == 5)||(date.Day <= 21 && date.Month == 6))
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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
