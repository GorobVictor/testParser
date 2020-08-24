using LangitMusik.Model;
using LangitMusik.Model.Interface;
using SampleProject.Backend.Model;
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
            if (txt_Search.Text.IndexOf("playlist") != -1) {
                var obj = (NewPlaylist)await new NewPlaylist().GetObject(txt_Search.Text.Split('/')
                                .Where(x => x.Replace(" ", "") != "" && x != null).Last());
                if (obj != null) {
                    Playlist playlist = new Playlist() {
                        Id = obj.Id.ToString(),
                        Description = obj.Description,
                        Title = obj.Name,
                        ImageLink = obj.Photo,
                        AllTracks = obj.Songs.Select(x => new Track() {
                            Id = x.Id.ToString(),
                            Description = x.Description,
                            Title = x.Name,
                            ArtistId = x.Artist.Id.ToString(),
                            Artist = x.Artist.Name,
                            AlbumId = x.Album.Id.ToString(),
                            Album = x.Album.Name,
                            Duration = x.strDuration,
                        }).ToList()
                    };
                    listview_Viewer.ItemsSource = playlist.AllTracks.ToList();
                }
            }
            else if (txt_Search.Text.IndexOf("album") != -1) {
                var obj = (NewAlbum)await new NewAlbum().GetObject(txt_Search.Text.Split('/')
                                .Where(x => x.Replace(" ", "") != "" && x != null).Last());
                if (obj != null) {
                    Album album = new Album() {
                        Id = obj.Id.ToString(),
                        ReleaseDate = obj.ReleaseDate,
                        Artist = obj.Artist.Name,
                        Title = obj.Name,
                        ImageLink = obj.Photo,
                        AllTracks = obj.Songs.Select(x => new Track() {
                            Id = x.Id.ToString(),
                            Description = x.Description,
                            Title = x.Name,
                            ArtistId = x.Artist.Id.ToString(),
                            Artist = x.Artist.Name,
                            AlbumId = x.Album.Id.ToString(),
                            Album = x.Album.Name,
                            Duration = x.strDuration,
                        }).ToList()
                    };
                    listview_Viewer.ItemsSource = album.AllTracks.ToList();
                }
            }
            btn_Go.IsEnabled = true;
        }
        /// <summary>
        /// Обработка нажатия двойного клика во строке
        /// </summary>
        private async void listview_Viewer_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var click = ((FrameworkElement)e.OriginalSource).DataContext as Track;
            if (click == null)
                return;
            var item = new NewSong(Convert.ToInt32(click.Id), click.Title, click.Description, null,
                new NewAlbum(Convert.ToInt32(click.AlbumId), click.Album), new NewArtist(Convert.ToInt32(click.ArtistId), click.Artist),
                (Convert.ToInt32(click.Duration.Remove(2)), Convert.ToInt32(click.Duration.Substring(3))));
            item.Album = (NewAlbum)await item.Album.GetObject(item.Album.Id);
            item.Artist = (NewArtist)await item.Artist.GetObject(item.Artist.Id);
            for (int i = 0; i < item.Album.Songs.Count(); i++) {
                item.Album.Songs[i].Album = item.Album;
                item.Album.Songs[i].Artist = item.Artist;
            }
            item.Album.PhotoImg = await item.Album.SetBitmap(item.Album.Photo);
            item.Artist.PhotoImg = await item.Artist.SetBitmap(item.Artist.Photo);
            DetailView detailView = new DetailView(item);
            detailView.Show();

        }
    }
}
