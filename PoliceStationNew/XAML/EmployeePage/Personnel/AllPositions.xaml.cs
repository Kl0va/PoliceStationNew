using PoliceStationNew.Models;
using PoliceStationNew.Moduls;
using Syncfusion.UI.Xaml.Charts;
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
        private static List<Models.Position> positions = new List<Models.Position>();
        public AllPositions()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            ColumnSeries series = new ColumnSeries();
            positions.Clear();
            Task<List<Models.Position>> getPosition = ApiWork.GetAllPositions();
            await getPosition.ContinueWith(t =>
            {
                foreach (Models.Position position in getPosition.Result)
                {
                    positions.Add(position);
                }
            });
            Thread.Sleep(100);
            foreach (Models.Position position1 in positions)
            {
                PosGrid.Items.Add(position1);
            }
            series.ItemsSource = positions;
            series.XBindingPath = "name";
            series.YBindingPath = "salary";
            PosChart.Series.Add(series);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPosition));
        }

        private void PosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check.editPos = (Models.Position)PosGrid.SelectedItem;
            Frame.Navigate(typeof(AddPosition));
        }
    }
}
