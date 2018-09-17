using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nlotto_gen.Models
{
    public class Game_Model
    {
        public string _display_game { get; set; }//showing six game number in string
        public string _display_game_id { get; set; }//showing game id
        public string _background_color { get; set; }//list view item background color
    }
}
