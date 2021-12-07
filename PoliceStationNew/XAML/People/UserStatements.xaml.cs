using Flurl.Http;
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

namespace PoliceStationNew.XAML.People
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UserStatements : Page
    {
        private static List<Appeal> Appeal = new List<Appeal>();
        private static Declarant declarant1;
        public UserStatements()
        {
            this.InitializeComponent();
            Load();
        }

        private void addDocument_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserCreateStatement));
        }

        
        public async void Load()
        {
            Appeal.Clear();
            Task<List<Declarant>> getDeclarants = ApiWork.GetAllDeclarants();
            await getDeclarants.ContinueWith(t =>
            {
                foreach (Declarant declarant in getDeclarants.Result)
                {
                    if (Check.email == declarant.email)
                    {
                        Check.ID = declarant.id;
                    }
                }
            });




            Task<List<Appeal>> getAppeals = ApiWork.GetAllAppeals();
            await getAppeals.ContinueWith(t =>
            {
                foreach (Appeal appeal in getAppeals.Result)
                {
                    if (appeal.declarant_id == Check.ID)
                    {
                        Appeal.Add(appeal);                      
                    }
                }
            });
            Thread.Sleep(100);
            foreach (Appeal appeals in Appeal)
            {
                StatementGrid.Items.Add(appeals);
            }
        }

        private async void StatementGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.Appeal ap = (Appeal)StatementGrid.SelectedItem;
            if (ap.status == "На рассмотрении")
            {
                var response = await "https://police-api-russia.herokuapp.com/delete_appeal".PostJsonAsync(ap).ReceiveString();
                Frame.Navigate(typeof(UserStatements));
            }
        }

        private async void Report_Click(object sender, RoutedEventArgs e)
        {
            int count_checked = 0;
            int count_added = 0;
            int count_checking = 0;
            var fil1 = await KnownFolders.MusicLibrary.GetFolderAsync("Отчет по заявлениям");
            await fil1.DeleteAsync();
            var file = await KnownFolders.MusicLibrary.CreateFolderAsync("Отчет по заявлениям");
            var fil = await file.CreateFileAsync("Appeal.docx");
            XWPFDocument doc = new XWPFDocument();
            foreach(Appeal appeals1 in Appeal)
            {
                if (appeals1.status == "На рассмотрении")
                {
                    count_added += 1;
                }
                else if (appeals1.status == "Рассмотрено")
                {
                    count_checked += 1;
                }
                else if (appeals1.status == "Одобрено")
                {
                    count_checking += 1;
                }
            }
                var p0 = doc.CreateParagraph();
                var p1 = doc.CreateParagraph();
                var p2 = doc.CreateParagraph();
                p0.Alignment = ParagraphAlignment.CENTER;
                p2.Alignment = ParagraphAlignment.CENTER;
                p1.Alignment = ParagraphAlignment.CENTER;
                XWPFRun r0 = p0.CreateRun();
                XWPFRun r1 = p1.CreateRun();
                XWPFRun r2 = p2.CreateRun();
                r0.FontFamily = "Standard";
                r1.FontFamily = "Standard";
                r2.FontFamily = "Standard";
                r0.FontSize = 14;
                r1.FontSize = 14;
                r2.FontSize = 14;
                r0.IsBold = true;
                r1.IsBold = true;
                r2.IsBold = true;
                r0.SetText($"Заявлений рассмотрено - {count_checked} \n");
                r1.SetText($"Заявлений одобрено - {count_checking}  \n");
                r2.SetText($"Заявлений ещё не одобрено - {count_added}");
                doc.Write(fil.OpenStreamForWriteAsync().Result);
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text == "")
            {
                Appeal.Clear();
                Task<List<Appeal>> getAppeals = ApiWork.GetAllAppeals();
                await getAppeals.ContinueWith(t =>
                {
                    foreach (Appeal appeal in getAppeals.Result)
                    {
                        if (appeal.declarant_id == Check.ID)
                        {
                            Appeal.Add(appeal);
                        }
                    }
                });
                Thread.Sleep(100);
                foreach (Appeal appeals in Appeal)
                {
                    StatementGrid.Items.Add(appeals);
                }
            }
            else
            {
                Appeal.Clear();
                string SearchText = Search.Text;
                Task <List<Appeal>> appealTask = ApiWork.GetAllAppeals();
                await appealTask.ContinueWith(task =>
                {
                    foreach(Appeal appeals in task.Result)
                    {
                        if(appeals.appeal_name == SearchText)
                        {
                            Appeal.Add(appeals);
                        }
                    }
                });
                foreach (Appeal appeals in Appeal)
                {
                    StatementGrid.Items.Clear();
                    StatementGrid.Items.Add(appeals);
                }
            }
        }
    }
}
