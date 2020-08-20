using LangitMusik.Model.Abstract;
using LangitMusik.Model.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LangitMusik.Model {
    public class Artist : Parsing, IMainInformation, IListing {
        public Artist(int id = 0, string name = null, string photo = null, List<Song> songs = null, Bitmap photoImg = null) {
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

        public async override Task<IMainInformation> ParsingJson(string id) {
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/artist/detail/{id}"));
            var artist = new Artist(
                json["artistId"].ToObject<int>(),
                json["artistName"].ToString(),
                $"https://www.langitmusik.co.id/image.do?fileuid={json["artistSImgPath"].ToString()}",
                new List<Song>(),
                await SetBitmap($"https://www.langitmusik.co.id/image.do?fileuid={json["artistSImgPath"].ToString()}"));
            return artist;
        }
    }
}
