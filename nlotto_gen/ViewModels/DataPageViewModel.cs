using HtmlAgilityPack;
using nlotto_gen.Helpers;
using nlotto_gen.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace nlotto_gen.ViewModels
{
    public class DataPageViewModel : Observable
    {
        /* 2018 9 22 825 번째 게임 */
        private DateTime dateUTC = new DateTime(2018, 09, 22, 12, 00, 00, 00, DateTimeKind.Utc);
        private const int constantGameNumber=825;

        private TimeSpan constantGameDuration = new TimeSpan(7, 0, 0, 0);
        DateTime nowDateUTC = DateTime.UtcNow;
        int curGameNumber = constantGameNumber;
        //Int32 dataSize{get;set;}

        public ObservableCollection<string> query_ans { get; set; } = new ObservableCollection<string>();
        public string _name { get; set; }
        public string _crawl { get; set; }
        public DataPageViewModel()
        {
            Insert = new RelayCommand(InsertExecute);
            Query = new RelayCommand(QueryExecute);
            Drop = new RelayCommand(DropExecute);

            while (nowDateUTC > dateUTC + constantGameDuration)
            {
                dateUTC.Add(constantGameDuration);
                curGameNumber++;
            }
            //dataSize = DataAccess.count;
        }
        public async void InsertExecute()
        {
            ContentDialog cd = new ContentDialog();
            StackPanel ss = new StackPanel();
            ss.Orientation = Orientation.Horizontal;
            ss.Children.Add(new ProgressRing()
            {
                IsActive = true
            });

            try
            {
                HttpClient httpClient = new HttpClient();
                cd.Title = "작업중";
                var result = (await httpClient.GetStringAsync(new Uri("http://m.nlotto.co.kr/common.do?method=main")));
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(result);

                OnPropertyChanged("_crawl");
                cd.ShowAsync();

                List<int> Game = await DataAccess.ListofTask(curGameNumber);

                foreach (int i in Game)
                {

                    result = await httpClient.GetStringAsync(new Uri("http://www.nlotto.co.kr/gameResult.do?method=byWin&drwNo=" + i.ToString()));
                    htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(result);
                    var list = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("number"));
                    var number_node = htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("result_title"));
                    var number = number_node.ElementAt(0).SelectNodes("strong");

                    var childs = list.ElementAt(0).SelectNodes("img");

                    string[] a = new string[6];
                    int cnt = 0;
                    foreach (var j in childs)
                    {
                        a[cnt++] = j.GetAttributeValue("alt", "");
                        _crawl += a[cnt - 1];
                        _crawl += ", ";
                    }
                    if (ss.Children.Count==2) ss.Children.RemoveAt(1);

                    ss.Children.Add(new TextBlock()
                    {
                        Text = string.Format("{0} / {1}", i.ToString(), curGameNumber)
                    }
                    );

                    cd.Content = ss;
                    _crawl = _crawl.Remove(_crawl.Length - 2);
                    _crawl += Environment.NewLine;
                    DataAccess.Insert(Int32.Parse(a[0]), Int32.Parse(a[1]), Int32.Parse(a[2]), Int32.Parse(a[3]), Int32.Parse(a[4]), Int32.Parse(a[5]), Int32.Parse(number.ElementAt(0).InnerHtml));
                }
                OnPropertyChanged("_crawl");
                cd.Hide();
                //dataSize = DataAccess.count;
                httpClient.Dispose();
            }
            catch
            {
                cd.Hide();
            }
        }
        public async void QueryExecute()
        {
            List<string> aa = await DataAccess.Query("select * from MyTable;");
            query_ans.Clear();
            foreach (var i in aa) query_ans.Add(i);
            OnPropertyChanged("query_ans");
        }

        public void DropExecute()
        {
            DataAccess.Drop();
            query_ans.Clear();
            OnPropertyChanged("query_ans");
        }

        public RelayCommand Insert { get; set; }
        public RelayCommand Query { get; set; }
        public RelayCommand Drop { get; set; }
    }
}
