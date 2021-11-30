using Flurl.Http;
using PoliceStationNew.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace PoliceStationNew.XAML.People
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UserCreateStatement : Page
    {
        public UserCreateStatement()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Appealtext.Text.Length < 10 || Name.Text.Length < 5)
                {
                    ContentDialog contentDialog = new ContentDialog
                    {
                        Title = "Ошибка",
                        Content = "Неправильно заполнены поля",
                        CloseButtonText = "Ок"
                    };
                    await contentDialog.ShowAsync();
                }
                else
                {
                    var appeal = new Appeal("На рассмотрении", Appealtext.Text, Name.Text, Check.ID);
                    var response = await "https://police-api-russia.herokuapp.com/input_appeal".PostJsonAsync(appeal).ReceiveString();
                    Frame.Navigate(typeof(UserStatements));
                }
            }
            catch
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = "Используйте другое название",
                    CloseButtonText = "Ок"
                };
                await contentDialog.ShowAsync();
            }
        }
    }
}
