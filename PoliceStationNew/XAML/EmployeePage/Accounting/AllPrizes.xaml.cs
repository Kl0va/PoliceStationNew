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
    public sealed partial class AllPrizes : Page
    {
        private static List<Prize> Prizes = new List<Prize>();
        public AllPrizes()
        {
            this.InitializeComponent();
            Load();
        }

        public async void Load()
        {
            Prizes.Clear();
            Task<List<Prize>> getPrizes = ApiWork.GetAllPrizes();
            await getPrizes.ContinueWith(t =>
            {
                foreach (Prize prize in getPrizes.Result)
                {
                    Prizes.Add(prize);
                }
            });

            Thread.Sleep(100);
            foreach(Prize prize1 in Prizes)
            {
                PrizeGrid.Items.Add(prize1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPrize));
        }

        private void PrizeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check.editPrize = (Prize)PrizeGrid.SelectedItem;
            Frame.Navigate(typeof(AddPrize));
        }
    }
}
