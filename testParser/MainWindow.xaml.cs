using LangitMusik.Model;
using LangitMusik.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testParser {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void btn_Go_Click(object sender, RoutedEventArgs e) {
            btn_Go.IsEnabled = false;
            IListing obj = null;
            if (txt_Search.Text.IndexOf("playlist") != -1)
                obj = (Playlist)await new Playlist().GetObject(txt_Search.Text.Split('/')
                                .Where(x => x.Replace(" ", "") != "" && x != null).Last());
            else if (txt_Search.Text.IndexOf("album") != -1)
                obj = (Album)await new Album().GetObject(txt_Search.Text.Split('/')
                                .Where(x => x.Replace(" ", "") != "" && x != null).Last());
            if (obj != null)
                listview_Viewer.ItemsSource = obj.Songs.ToList();
            btn_Go.IsEnabled = true;
        }
        /// <summary>
        /// Обработка нажатия двойного клика во строке
        /// </summary>
        private async void listview_Viewer_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as Song;
            if (item == null)
                return;
            item.Album = (Album)await item.Album.GetObject(item.Album.Id);
            item.Artist = (Artist)await item.Artist.GetObject(item.Artist.Id);
            for (int i = 0; i < item.Album.Songs.Count(); i++) {
                item.Album.Songs[i].Album = item.Album;
                item.Album.Songs[i].Artist = item.Artist;
            }
            DetailView detailView = new DetailView(item);
            detailView.Show(); 
            
        }
    }
}
