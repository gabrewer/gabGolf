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

namespace gabGolf.Mobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRoundPage : ContentPage
    {
        public NewRoundPage()
        {
            InitializeComponent();
            BindingContext = new NewRoundPageViewModel(this.Navigation);
        }
    }

    class NewRoundPageViewModel : INotifyPropertyChanged
    {
        private String courseName;
        private DateTime datePlayed;
        private INavigation navigation;

        public NewRoundPageViewModel(INavigation nav)
        {
            navigation = nav;

            courseName = "";
            datePlayed = DateTime.Now;
            StartRoundCommand = new Command(StartRound);
        }

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; OnPropertyChanged(); }
        }

        public DateTime DatePlayed
        {
            get { return datePlayed; }
            set { datePlayed = value; OnPropertyChanged(); }
        }

        public ICommand StartRoundCommand { get; }

        private async void StartRound()
        {
            Round newRound = new Round()
            {
                Course = CourseName,
                PlayedAt = datePlayed
            };

            await navigation.PushAsync(new HolePage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
