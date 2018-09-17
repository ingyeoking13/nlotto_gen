using System;

using nlotto_gen.ViewModels;

using Windows.UI.Xaml.Controls;

namespace nlotto_gen.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
