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
                string fn = Transliteration.CyrillicToLatin(FullName, Language.Russian);
                string org = Transliteration.CyrillicToLatin(Organization, Language.Russian);
                string adr = Transliteration.CyrillicToLatin(Adress, Language.Russian);
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
