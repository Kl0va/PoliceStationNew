using NPOI.XWPF.UserModel;
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
using Windows.Storage;
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
    public sealed partial class AllEmp : Page
    {
        private static List<Models.Employee> Employees = new List<Models.Employee>();
        public AllEmp()
        {
            this.InitializeComponent();
            Load();
        }

        public async void Load()
        {
            Employees.Clear();
            Task<List<Models.Employee>> getEmployees = ApiWork.GetAllEmployees();
            await getEmployees.ContinueWith(t =>
            {
                foreach (Models.Employee employee in getEmployees.Result)
                {
                    Employees.Add(employee);
                }
            });
            Thread.Sleep(100);
            foreach (Models.Employee employee1 in Employees)
            {
                EmpGrid.Items.Add(employee1);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddEmp));
        }

        private void EmpGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Check.editEmp = (Models.Employee)EmpGrid.SelectedItem;
            Frame.Navigate(typeof(AddEmp));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var fil1 = await KnownFolders.MusicLibrary.GetFolderAsync("Отчет по работникам");
            await fil1.DeleteAsync();
            var file = await KnownFolders.MusicLibrary.CreateFolderAsync("Отчет по работникам");
            var fil = await file.CreateFileAsync("Emp.docx");
            XWPFDocument doc = new XWPFDocument();
            foreach (Models.Employee employee in Employees)
            {

                var p0 = doc.CreateParagraph();
                p0.Alignment = ParagraphAlignment.CENTER;
                XWPFRun r0 = p0.CreateRun();
                r0.FontFamily = "Standard";
                r0.FontSize = 14;
                r0.IsBold = true;
                r0.SetText(employee.email);
            }

            doc.Write(fil.OpenStreamForWriteAsync().Result);
        }
    }
}
