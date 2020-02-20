using System;
using System.ComponentModel;
using MapEX.ViewModels;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapEX
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            map.MapClicked += OnMapClicked;
            CrossGeolocator.Current.PositionChanged += OnLocationChanged;
        }

        void OnLocationChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveToLocation();
            if (map.Pins.Count > 0)
            {
                (BindingContext as MainPageViewModel).OnLocationChanged(map, e);
            }
        }

        async void MoveToLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(1)));
        }

        void OnEditorCompleted(object sender, EventArgs e)
        {
            string temp = ((Editor)sender).Text;
            (BindingContext as MainPageViewModel).OnEditorChanged(temp);
        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {

            map.Pins.Add(new Pin
            {
                Label = (BindingContext as MainPageViewModel).Txt,
                Address = $"{e.Position.Latitude.ToString()}, {e.Position.Longitude.ToString()}",
                Type = PinType.Place,
                Position = e.Position
            });


        }
    }
}
