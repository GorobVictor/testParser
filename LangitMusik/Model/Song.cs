using LangitMusik.Model.Abstract;
using LangitMusik.Model.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;

namespace LangitMusik.Model {
    public class Song : Parsing, IMainInformation, ISong {
        public Song(int id = 0, string name = null, string photo = null, Album album = null, Artist artist = null, (int, int) duration = default, Bitmap photoImg = null) {
            _id = id;
            _name = name;
            _photo = photo;
            _album = album;
            _artist = artist;
            _duration = duration;
            _photoImg = photoImg;
        }

        private int _id { get; set; }
        private string _name { get; set; }
        private string _photo { get; set; }
        private Bitmap _photoImg { get; set; }
        private Album _album { get; set; }
        private Artist _artist { get; set; }
        private (int, int) _duration { get; set; }
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
        public Album Album {
            get {
                if (_album != null) return _album;
                else throw new NullReferenceException();
            }
            set { _album = value; }
        }
        public Artist Artist {
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

        public (int, int) Duration { get { return _duration; } set { _duration = value; } }

        public async override Task<IMainInformation> ParsingJson(string id) {
            var json = JArray.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/song/set?songId={id}"));
            var album = (Album)await new Album().GetObject(json[0]["albumId"].ToString());
            var artist = (Artist)await new Artist().GetObject(json[0]["artistId"].ToString());
            var result = new Song(
                json[0]["songId"].ToObject<int>(),
                json[0]["songName"].ToString(),
                album.Photo,
                album,
                artist,
                (json[0]["playtime"].ToObject<int>() / 60, json[0]["playtime"].ToObject<int>() % 60),
                await SetBitmap(album.Photo));
            return result;
        }
    }
}
