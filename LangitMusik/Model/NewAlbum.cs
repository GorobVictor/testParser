using LangitMusik.Model.Abstract;
using LangitMusik.Model.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LangitMusik.Model {
    public class NewAlbum : Parsing, IMainInformation, IListing {
        public NewAlbum(int id = 0, string name = null, string description = null, string photo = null, List<NewSong> songs = null, Bitmap photoImg = null, string releaseDate = null) {
            _id = id;
            _name = name;
            _photo = photo;
            _songs = songs;
            _photoImg = photoImg;
            _description = description;
            _releaseDate = releaseDate;
        }

        private int _id { get; set; }
        private string _name { get; set; }
        private string _description { get; set; }
        private string _releaseDate { get; set; }
        private string _photo { get; set; }
        private Bitmap _photoImg { get; set; }
        private List<NewSong> _songs { get; set; }
        private NewArtist _artist { get; set; }
        public int Id { get { return _id; } set { _id = value; } }
        public string Name {
            get {
                if (_name != null) return _name;
                else throw new NullReferenceException();
            }
            set { _name = value; }
        }
        public string Description {
            get {
                if (_description != null) return _description;
                else throw new NullReferenceException();
            }
            set { _description = value; }
        }
        public string ReleaseDate {
            get {
                if (_releaseDate != null) return _releaseDate;
                else throw new NullReferenceException();
            }
            set { _releaseDate = value; }
        }
        public string Photo {
            get {
                if (_photo != null) return _photo;
                else throw new NullReferenceException();
            }
            set { _photo = value; }
        }
        public List<NewSong> Songs {
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
        public NewArtist Artist {
            get {
                if (_artist != null) return _artist;
                else throw new NullReferenceException();
            }
            set { _artist = value; }
        }

        /// <summary>
        /// Парсинг альбома
        /// </summary>
        /// <param name="id">id альбома</param>
        /// <returns></returns>
        public async override Task<IMainInformation> ParsingJson(string id) {
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/detail/album2/{id}"));
            var album = new NewAlbum(
                json["albumId"].ToObject<int>(),
                json["albumName"].ToString(),
                json["albumReview"].ToString(),
                $"https://www.langitmusik.co.id/image.do?fileuid={json["albumSImgPath"].ToString()}",
                new List<NewSong>(),
                null,
                json["issueDate"].ToString());
            json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/song/album/{id}/1/100"));
            foreach (var js in (JArray)json["dataList"])
                album.Songs.Add(new NewSong(
                                js["songId"].ToObject<int>(),
                                js["songName"].ToString(),
                                js["songName"].ToString(),
                                album.Photo, album,
                                new NewArtist(js["artistId"].ToObject<int>(), js["artistName"].ToString()),
                                (js["playtime"].ToObject<int>() / 60, js["playtime"].ToObject<int>() % 60)));
            if (album.Songs.Count() > 0)
                album.Artist = album.Songs[0].Artist;
            return album;
        }
    }
}
