using LangitMusik.Model;
using LangitMusik.Model.Abstract;
using SampleProject.Backend.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace testParser {
    /// <summary>
    /// Логика взаимодействия для DetailView.xaml
    /// </summary>
    public partial class DetailView : Window {
        public DetailView(NewSong song) {
            InitializeComponent();
            labl_NameSong.Content = song.Name;
            labl_NameAlbum.Content = song.Album.Name;
            labl_NameArtist.Content = song.Artist.Name;
            img_ImageAlbum.Source = 
                Imaging.CreateBitmapSourceFromHBitmap(song.Album.PhotoImg.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            img_ImageArtist.Source = 
                Imaging.CreateBitmapSourceFromHBitmap(song.Artist.PhotoImg.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            
            
            listview_Viewer.ItemsSource = song.Album.Songs.Select(x => new Track() {
                Id = x.Id.ToString(),
                Description = x.Description,
                Title = x.Name,
                ArtistId = x.Artist.Id.ToString(),
                Artist = x.Artist.Name,
                AlbumId = x.Album.Id.ToString(),
                Album = x.Album.Name,
                Duration = x.strDuration,
            }).ToList();
        }
    }
}
