using nlotto_gen.Helpers;
using nlotto_gen.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace nlotto_gen.ViewModels
{
    public class DataPageViewModel : Observable
    {
        public ObservableCollection<string> query_ans { get; set; } = new ObservableCollection<string>();
        public string _name { get; set; }
        public DataPageViewModel()
        {
            Insert = new RelayCommand(InsertExecute);
            Query = new RelayCommand(QueryExecute);
            Drop = new RelayCommand(DropExecute);
        }
        public void InsertExecute()
        {
            DataAccess.Insert(_name);
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
