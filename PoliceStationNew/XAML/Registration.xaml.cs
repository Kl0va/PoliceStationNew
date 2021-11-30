using Flurl.Http;
using PoliceStationNew.Models;
using PoliceStationNew.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace PoliceStationNew.XAML
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Registration : Page
    {
        public Registration()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            String email = Email.Text;
            Check.reg = true;
            Task<List<Declarant>> getDeclarants = ApiWork.GetAllDeclarants();
            await getDeclarants.ContinueWith(t =>
            {
                foreach (Declarant declarant1 in getDeclarants.Result)
                {
                    if (email == declarant1.email)
                    {
                        Check.reg = false;
                    }
                }
            });



            if (Check.reg) 
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Password.Text));
                var declarant = new Declarant(Name.Text, Fam.Text, Middle.Text, Ser.Text, Num.Text, Number.Text, Email.Text,Convert.ToBase64String(hash));
                var response = await "https://police-api-russia.herokuapp.com/input_declarant".PostJsonAsync(declarant).ReceiveString();
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = "Пользователь с таким Email уже существует",
                    CloseButtonText = "Ок"
                };
                await contentDialog.ShowAsync();
            }
        }
    }
}
