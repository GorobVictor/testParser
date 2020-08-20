using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using LangitMusik.Model.Abstract;
using LangitMusik.Model.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangitMusik.Model {
    public class Album : Parsing, IMainInformation, IListing {
        public Album(int id = 0, string name = null, string photo = null, List<Song> songs = null, Bitmap photoImg = null) {
            _id = id;
            _name = name;
            _photo = photo;
            _songs = songs;
            _photoImg = photoImg;
        }

        private int _id { get; set; }
        private string _name { get; set; }
        private string _photo { get; set; }
        private Bitmap _photoImg { get; set; }
        private List<Song> _songs { get; set; }
        private Artist _artist { get; set; }
        public int Id { get { return _id; } set { _id = value; } }
        public string Name {
            get {
                if (_name != null) return _name;
                else throw new NullReferenceException();
            }
            set { _name = value; }
        }
        public string Photo {
            get {
                if (_photo != null) return _photo;
                else throw new NullReferenceException();
            }
            set { _photo = value; }
        }
        public List<Song> Songs {
            get {
                if (_songs != null) return _songs;
                else throw new NullReferenceException();
            }
            set { _songs = value; }
        }
        public Bitmap PhotoImg {
            get {
                if (_photoImg != null) return _photoImg;
                else throw new NullReferenceException();
            }
            set { _photoImg = value; }
        }
        public Artist Artist {
            get {
                if (_artist != null) return _artist;
                else throw new NullReferenceException();
            }
            set { _artist = value; }
        }
        public async override Task<IMainInformation> ParsingJson(string id) {
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/detail/album2/{id}"));
            var album = new Album(
                json["albumId"].ToObject<int>(),
                json["albumName"].ToString(),
                $"https://www.langitmusik.co.id/image.do?fileuid={json["albumSImgPath"].ToString()}",
                new List<Song>(),
                await SetBitmap($"https://www.langitmusik.co.id/image.do?fileuid={json["albumSImgPath"].ToString()}"));
            json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/song/album/{id}/1/100"));
            foreach (var js in (JArray)json["dataList"])
                album.Songs.Add(new Song(
                                js["songId"].ToObject<int>(),
                                js["songName"].ToString(),
                                album.Photo, album,
                                new Artist(js["artistId"].ToObject<int>(), js["artistName"].ToString()),
                                (js["playtime"].ToObject<int>() / 60, js["playtime"].ToObject<int>() % 60)));
            if (album.Songs.Count() > 0)
                album.Artist = album.Songs[0].Artist;
            return album;
        }
    }
}
