using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QRCodeSampleApp
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string FullName { get; set; } = "Гаязов Рифат Равилович";
        public string PhoneNumber { get; set; } = "+78005553535";
        public string Email { get; set; } = "gayazovrr@kamaz.ru";
        public string BarcodeValue { get; set; }
        public ICommand GenerateQRCommand { get; set; }

        public MainPageViewModel()
        {

            GenerateQRCommand = new Command(async () =>  await GenerateQR());
        }

        public async Task GenerateQR()
        {
            try
            {
                Translitor translitor = new Translitor();
                string fn = translitor.Translit(FullName);
                BarcodeValue = $"MECARD:N:{fn};ORG:;TEL:{PhoneNumber};EMAIL:{Email};ADR:;;";
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ой-ой", ex.Message, "Я СМИРИЛСЯ");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
