using System;
using System.Collections.Generic;
using MapEX.Models;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace MapEX.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        //public ICommand ClickCommand { get; private set; }
        //public ICommand EditorCommand { get; private set; }
        public IEditorModel EditorModel { get; private set; }
        public ILocationModel LocationModel { get; private set; }

        public string Txt
        {
            get
            {
                if(EditorModel.Name != null)
                    return EditorModel.Name.ToString();
                return "Default Name";
            }
        }
       
        public string Pin
        {
            get
            {
                if(LocationModel.ClosestPin != null)
                {
                    return $"Closest Pin: '{LocationModel.ClosestPin.Label}'. {LocationModel.DistanceToClosestPin.ToString()} KM away";
                }
                else
                {
                    return "Location of the closest pin unknown";
                }
            }
        }

        public MainPageViewModel()
        {
            EditorModel = new EditorModel();
            LocationModel = new LocationModel();
            StartListening();

        }
        async void StartListening()
        {
            await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(100), 0.001, false, null);

        }

        async void ComposeEmail(string subject, string body, List<string> recipients)
        {
            var mail = new EmailMessage
            {
                Subject = subject,
                Body = body,
                To = recipients
            };

            await Email.ComposeAsync(mail);
        }

        //EVENT HANDLERS

        public void OnLocationChanged(Xamarin.Forms.Maps.Map map, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            double ClosestDistance = Double.MaxValue;
            Pin closestPin = new Pin();
            for (int i = 0; i < map.Pins.Count; ++i)
            {
                double dist = Xamarin.Essentials.Location.CalculateDistance(map.Pins[i].Position.Latitude, map.Pins[i].Position.Longitude, e.Position.Latitude, e.Position.Longitude, DistanceUnits.Kilometers);
                if (dist < ClosestDistance)
                {
                    ClosestDistance = dist;
                    closestPin = map.Pins[i];
                }
            }

            LocationModel.SetClosestPin(closestPin);
            LocationModel.SetDistanceToClosestPin(ClosestDistance);
            NotifyPropertyChanged("Pin");
            if (ClosestDistance < 0.5)
                ComposeEmail($"I am near {closestPin.Label}", $"Coordinates of a pin: '{closestPin.Position.Latitude.ToString()}, {closestPin.Position.Longitude.ToString()}'. Coordinates of a phone: '{e.Position.Latitude.ToString()}, {e.Position.Longitude.ToString()}'", new List<string> { "test@test.ru" });
        }

        public void OnEditorChanged(string name)
        {

            EditorModel.SetName(name);

            NotifyPropertyChanged("Txt");
        }
    }
}
