using LangitMusik.Model.Abstract;
using LangitMusik.Model.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;

namespace LangitMusik.Model {
    public class NewSong : Parsing, IMainInformation, ISong {
        public NewSong(int id = 0, string name = null, string description = null, string photo = null, NewAlbum album = null, NewArtist artist = null, (int, int) duration = default, Bitmap photoImg = null) {
            _id = id;
            _name = name;
            _photo = photo;
            _album = album;
            _artist = artist;
            _duration = duration;
            _photoImg = photoImg;
            _description = description;
        }

        private int _id { get; set; }
        private string _name { get; set; }
        private string _description { get; set; }
        private string _photo { get; set; }
        private Bitmap _photoImg { get; set; }
        private NewAlbum _album { get; set; }
        private NewArtist _artist { get; set; }
        private (int, int) _duration { get; set; }
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
        public string Photo {
            get {
                if (_photo != null) return _photo;
                else throw new NullReferenceException();
            }
            set { _photo = value; }
        }
        public NewAlbum Album {
            get {
                if (_album != null) return _album;
                else throw new NullReferenceException();
            }
            set { _album = value; }
        }
        public NewArtist Artist {
            get {
                if (_artist != null) return _artist;
                else throw new NullReferenceException();
            }
            set { _artist = value; }
        }
        public Bitmap PhotoImg {
            get {
                if (_photoImg != null) return _photoImg;
                else throw new NullReferenceException();
            }
            set { _photoImg = value; }
        }

        public string strDuration {
            get {
                string duration = "";
                if (_duration.Item1 < 10)
                    duration += $"0{_duration.Item1}:";
                else duration += $"{_duration.Item1}:";
                if (_duration.Item2 < 10)
                    duration += $"0{_duration.Item2}";
                else duration += _duration.Item2;
                return duration;
            }
        }

        public (int, int) Duration { get => _duration; set => _duration = value; }

        /// <summary>
        /// Парсинг песни
        /// </summary>
        /// <param name="id">id песни</param>
        /// <returns></returns>
        public async override Task<IMainInformation> ParsingJson(string id) {
            var json = JArray.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/song/set?songId={id}"));
            var album = (NewAlbum)await new NewAlbum().GetObject(json[0]["albumId"].ToString());
            var artist = (NewArtist)await new NewArtist().GetObject(json[0]["artistId"].ToString());
            var result = new NewSong(
                json[0]["songId"].ToObject<int>(),
                json[0]["songName"].ToString(),
                json[0]["songName"].ToString(),
                album.Photo,
                album,
                artist,
                (json[0]["playtime"].ToObject<int>() / 60, json[0]["playtime"].ToObject<int>() % 60),
                null);
            return result;
        }
    }
}
