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
    public sealed partial class AddPosition : Page
    {
        public AddPosition()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            if(Check.editPos != null)
            {
                PosName.Text = Check.editPos.name;
                Salary.Text = Check.editPos.salary.ToString();
            }
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PosName.Text.Length <= 5 || Salary.Text.Length <= 3)
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
                    if (Check.editPos != null)
                    {
                        var pos = new Position(Check.editPos.id,PosName.Text, float.Parse(Salary.Text));
                        var response = await "https://police-api-russia.herokuapp.com/edit_position".PostJsonAsync(pos).ReceiveString();
                    }
                    else
                    {
                        var pos = new Position(PosName.Text, float.Parse(Salary.Text));
                        var response = await "https://police-api-russia.herokuapp.com/input_position".PostJsonAsync(pos).ReceiveString();
                    }
                    Frame.Navigate(typeof(AllPositions));
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
