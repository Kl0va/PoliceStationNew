using Flurl.Http;
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

namespace PoliceStationNew.XAML.EmployeePage
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class EmpStatements : Page
    {
        private static List<Statement> statements = new List<Statement>();
        public EmpStatements()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            statements.Clear();
            Task<List<Statement>> getStatements = ApiWork.GetAllStatements();
            await getStatements.ContinueWith(t =>
            {
                foreach (Statement statement in getStatements.Result)
                {
                    if (statement.employee_id == Check.ID)
                    {
                        statements.Add(statement);
                    }
                }
            });
            Thread.Sleep(100);
            foreach (Statement statement1 in statements)
            {
                StatementGrid.Items.Add(statement1);
            }
        }

        private static string appId;
        private async void StatementGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (Statement statement in statements)
                {
                    if ((Statement)StatementGrid.SelectedItem == statement)
                    {
                        appId = statement.appeal_id;
                    }
                }
                var appeal = new Appeal("Рассмотрено", "", appId, 0);
                var response1 = await "https://police-api-russia.herokuapp.com/edit_status".PostJsonAsync(appeal).ReceiveString();
            }
            catch
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Ошибка",
                    Content = "Технические шоколадки",
                    CloseButtonText = "Ок"
                };
                await contentDialog.ShowAsync();
            }
        }
    }
}
