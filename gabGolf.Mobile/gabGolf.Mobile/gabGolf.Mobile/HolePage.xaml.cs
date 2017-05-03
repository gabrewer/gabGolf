using gabGolf.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace gabGolf.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HolePage : ContentPage
    {
        public HolePage()
        {
            InitializeComponent();
            BindingContext = new HolePageViewModel();
        }
    }

    class HolePageViewModel : INotifyPropertyChanged
    {
        private string currentClub;
        private Position currentLocation;
        private List<Shot> shots;
        private Shot previousShot;

        public HolePageViewModel()
        {
            currentClub = "";
            currentLocation = null;
            shots = new List<Shot>();
            previousShot = null;

            SaveShotCommand = new Command(SaveShot);
            SavePuttCommand = new Command(SavePutt);
        }

        public string CurrentClub
        {
            get { return currentClub; }
            set { currentClub = value; OnPropertyChanged(); }
        }

        public Position CurrentPosition
        {
            get { return currentLocation; }
            set { currentLocation = value; OnPropertyChanged(); }
        }

        public List<Shot> Shots
        {
            get { return shots; }
            set { shots = value; OnPropertyChanged(); }
        }

        public ICommand SaveShotCommand { get; }
        public ICommand SavePuttCommand { get; }

        private async void SaveShot()
        {
            Shot newShot = new Shot()
            {
                Club = CurrentClub,
                StartPosition = CurrentPosition,
                EndPosition = null
            };

            await RecordShot(newShot);
            previousShot = newShot;
        }

        private async void SavePutt()
        {
            Shot newPutt = new Shot()
            {
                Club = "Putter",
                StartPosition = CurrentPosition,
                EndPosition = null
            };

            await RecordShot(newPutt);
            previousShot = newPutt;
        }

        private async Task RecordShot(Shot shot)
        {
            var position = await GetPosition();
            if (previousShot != null)
            {
                previousShot.EndPosition = CurrentPosition;
            }
            Shots.Add(shot);
            previousShot = shot;
        }

        private async Task<Position> GetPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(new TimeSpan(0,0,10));
            return position;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
