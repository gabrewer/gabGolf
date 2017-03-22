using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace gabGolf.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void OnNewReoundClicked(object o, EventArgs e)
        {
            Navigation.PushAsync(new NewRoundPage());
        }

        public void OnScoresClicked(object o, EventArgs e)
        {
            Navigation.PushAsync(new ScoresPage());
        }
    }
}
