using PoliceStationNew.Models;
using PoliceStationNew.Moduls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
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

namespace PoliceStationNew.XAML.EmployeePage.Accounting
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AllVacations : Page
    {
        private static List<Vacation> Vacations = new List<Vacation>();
        public AllVacations()
        {
            this.InitializeComponent();
            Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddVac));
        }
        public async void Load()
        {
            Vacations.Clear();
            Task<List<Vacation>> getVacation = ApiWork.GetAllVacations();
            await getVacation.ContinueWith(t =>
            {
                foreach (Vacation vacation in getVacation.Result)
                {
                    Vacations.Add(vacation);
                }
            });

            Thread.Sleep(100);
            foreach (Vacation vacation1 in Vacations)
            {
                VacGrid.Items.Add(vacation1);
            }
        }

        private void VacGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check.editVac = (Vacation)VacGrid.SelectedItem;
            Frame.Navigate(typeof(AddVac));
        }
    }
}
