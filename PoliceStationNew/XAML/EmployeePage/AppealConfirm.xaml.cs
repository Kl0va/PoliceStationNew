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
    public sealed partial class AppealConfirm : Page
    {
        private static List<Article> articles = new List<Article>();
        public AppealConfirm()
        {
            this.InitializeComponent();
            Load();
        }
        public async void Load()
        {
            AppealName.Text = Check.confAppeal.appeal_name;
            AppealText.Text = Check.confAppeal.text;
            articles.Clear();
            Task<List<Article>> getArticles = ApiWork.GetAllArticles();
            await getArticles.ContinueWith(t =>
            {
                foreach (Article article in getArticles.Result)
                {
                    articles.Add(article);
                }
            });
            Thread.Sleep(100);
            foreach (Article article1 in articles)
            {
                State.Items.Add(article1.number);
            }
        }

        private static int art;
        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            foreach(Article article in articles)
            {
                if(int.Parse(State.SelectedValue.ToString()) == article.number)
                {
                    art = article.id;
                }
            }
            var statement = new Statement(Check.confAppeal.appeal_name,Check.ID,art);
            var response = await "https://police-api-russia.herokuapp.com/input_statement".PostJsonAsync(statement).ReceiveString();
            var appeal = new Appeal("Одобрено", AppealText.Text, Check.confAppeal.appeal_name, Check.confAppeal.declarant_id);
            var response1 = await "https://police-api-russia.herokuapp.com/edit_appeal".PostJsonAsync(appeal).ReceiveString();
        }
    }
}
