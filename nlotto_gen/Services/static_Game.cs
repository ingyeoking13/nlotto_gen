using nlotto_gen.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nlotto_gen.Services
{
    public static class static_Game
    {
        public static ObservableCollection<Game_Model> games { get; set; } = new ObservableCollection<Game_Model>();
        public static string[] mColors = {
        "FCE4EC", "F8BBD0", "F48FB1", "F06292", "EC407A", "E91E63", "D81B60",        //pinks
        "C2185B", "AD1457", "880E4F", "FF80AB", "FF4081", "F50057", "C51162",
        "F3E5F5", "E1BEE7", "CE93D8", "BA68C8", "AB47BC", "9C27B0", "8E24AA",        //purples
        "E8EAF6", "C5CAE9", "9FA8DA", "7986CB", "5C6BC0", "3F51B5", "3949AB",        //indigo
        "E3F2FD", "BBDEFB", "90CAF9", "64B5F6", "42A5F5", "2196F3", "1E88E5",        //blue
        "E1F5FE", "B3E5FC", "81D4fA", "4fC3F7", "29B6FC", "03A9F4", "039BE5",        //light blue
        "E0F7FA", "B2EBF2", "80DEEA", "4DD0E1", "26C6DA", "00BCD4", "00ACC1",        //cyan
        "E0F2F1", "B2DFDB", "80CBC4", "4DB6AC", "26A69A", "009688", "00897B",        //teal
        "E8F5E9", "C8E6C9", "A5D6A7", "81C784", "66BB6A", "4CAF50", "43A047",        //green
        "F9FBE7", "F0F4C3", "E6EE9C", "DCE775", "D4E157", "CDDC39", "C0CA33",        //lime
        "FFFDE7", "FFF9C4", "FFF590", "FFF176", "FFEE58", "FFEB3B", "FDD835",        //yellow
        "FFF8E1", "FFECB3", "FFE082", "FFD54F", "FFCA28", "FFC107", "FFB300",        //amber
        "FFF3E0", "FFE0B2", "FFCC80", "FFB74D", "FFA726", "FF9800", "FB8C00",        //orange
        "FBE9A7", "FFCCBC", "FFAB91", "FF8A65", "FF7043", "FF5722", "F4511E",        //deep orange
        "E64A19", "D84315", "BF360C", "FF9E80", "FF6E40", "FF3D00", "DD2600" };

    }
}
