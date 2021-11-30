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
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace PoliceStationNew.XAML.EmployeePage.Personnel
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AddForm : Page
    {
        public AddForm()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            Type.Items.Add("Высшее образование");
            Type.Items.Add("Среднее образование");
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FormName.Text.Length <= 5 || Type.SelectedValue.ToString() == "")
                {
                    ContentDialog contentDialog = new ContentDialog
                    {
                        Title = "Ошибка",
                        Content = "Неверно заполнены поля",
                        CloseButtonText = "Ок"
                    };
                    await contentDialog.ShowAsync();
                }
                else
                {
                    var form = new Formation(FormName.Text, Type.SelectedValue.ToString());
                    var response = await "https://police-api-russia.herokuapp.com/input_formation".PostJsonAsync(form).ReceiveString();
                    Frame.Navigate(typeof(AllForm));
                }
            }
            catch
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = "Неверно заполнены поля",
                    CloseButtonText = "Ок"
                };
                await contentDialog.ShowAsync();
            }
        }
    }
}
