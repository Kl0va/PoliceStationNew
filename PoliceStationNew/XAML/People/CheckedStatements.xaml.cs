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

namespace PoliceStationNew.XAML.People
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class CheckedStatements : Page
    {
        private static List<Appeal> Appeal = new List<Appeal>();
        public CheckedStatements()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            Appeal.Clear();
            Task<List<Appeal>> getAppeals = ApiWork.GetAllAppeals();
            await getAppeals.ContinueWith(t =>
            {
                foreach (Appeal appeal in getAppeals.Result)
                {
                    if (appeal.declarant_id == Check.ID && appeal.status == "Рассмотрено")
                    {
                        Appeal.Add(appeal);
                    }
                }
            });
            Thread.Sleep(100);
            foreach (Appeal appeals in Appeal)
            {
                AppealGrid.Items.Add(appeals);
            }
        }
    }
}
