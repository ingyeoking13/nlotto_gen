using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using nlotto_gen.Helpers;
using nlotto_gen.Models;
using nlotto_gen.Services;
using Windows.UI.Xaml.Controls;

namespace nlotto_gen.ViewModels
{
    public class MainViewModel : Observable
    {
        public ObservableCollection<Game_Model> _display_games { get; set; }
        private Random random = new Random(DateTime.Now.Millisecond);

        private int[] game;
        public MainViewModel()
        {
            game_create = new RelayCommand(ExecuteClick);
            _display_games = static_Game.games;
        }
        private void ExecuteClick()
        {
            {
                _display_games = new ObservableCollection<Game_Model>();

                game = new int[6];
                Random random = new Random(DateTime.Now.Millisecond);
                for (int t = 0; t < 5; t++)
                {
                    bool[] check = new bool[46];
                    _display_games.Add(new Game_Model());
                    _display_games[t]._background_color = "#"+ static_Game.mColors[random.Next(0, static_Game.mColors.Length)];

                    int dup = 0;
                    int cnt = 0;
                    while (cnt < 6)
                    {
                        int tmp = random.Next(1, 46);
                        if (check[tmp] == false)
                        {
                            check[tmp] = true;
                            game[cnt] = tmp;
                            cnt++;
                        }
                        else dup++;
                    }
                    Array.Sort(game);
                    _display_games[t]._display_game_id += string.Format("{0} 게임 ", t+1);
                    for (int i = 0; i < 6; i++) _display_games[t]._display_game += $"{game[i]} ";
                }
            }
            OnPropertyChanged("_display_games");
            static_Game.games = _display_games;
        }

        public RelayCommand game_create { get; set; }
    }
}
