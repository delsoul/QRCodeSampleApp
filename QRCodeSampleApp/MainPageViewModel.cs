using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NickBuhro.Translit;
using Xamarin.Forms;

namespace QRCodeSampleApp
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string FullName { get; set; }
        public string Organization { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }

        public bool BarcodeIsVisible { get; set; } = false;

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
                string org = translitor.Translit(Organization);
                string adr = translitor.Translit(Adress);
                BarcodeValue = $"MECARD:N:{fn};ORG:{org};TEL:{PhoneNumber};EMAIL:{Email};ADR:{adr};;";
                BarcodeIsVisible = true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ой-ой", ex.Message, "Я СМИРИЛСЯ");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
