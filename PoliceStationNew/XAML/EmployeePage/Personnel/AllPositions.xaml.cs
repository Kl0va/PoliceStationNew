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

namespace PoliceStationNew.XAML.EmployeePage.Personnel
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AllPositions : Page
    {
        private static List<Position> positions = new List<Position>();
        public AllPositions()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            positions.Clear();
            Task<List<Position>> getPosition = ApiWork.GetAllPositions();
            await getPosition.ContinueWith(t =>
            {
                foreach (Position position in getPosition.Result)
                {
                    positions.Add(position);
                }
            });
            Thread.Sleep(100);
            foreach (Position position1 in positions)
            {
                PosGrid.Items.Add(position1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPosition));
        }

        private void PosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check.editPos = (Position)PosGrid.SelectedItem;
            Frame.Navigate(typeof(AddPosition));
        }
    }
}
