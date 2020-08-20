using LangitMusik.Model.Abstract;
using LangitMusik.Model.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LangitMusik.Model {
    public class Playlist : Parsing, IMainInformation, IListing {
        public Playlist(int id = 0, string name = null, string photo = null, List<Song> songs = null, Bitmap photoImg = null) {
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
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/playlist/details?playlistId={id}&page=1&limit=100"));
            var result = new Playlist(
                json["detail"]["playlistId"].ToObject<int>(),
                json["detail"]["playlistName"].ToString(),
                json["detail"]["playlistLImgPath"].ToString(),
                new List<Song>(),
                await SetBitmap(json["detail"]["playlistLImgPath"].ToString()));

            var listSound = (JArray)json["list"]["dataList"];
            result._songs = listSound.Select(x => new Song(
                x["songId"].ToObject<int>(),
                x["songName"].ToString(),
                null,
                new Album(x["albumId"].ToObject<int>(), x["albumName"].ToString()),
                new Artist(x["artistId"].ToObject<int>(), x["artistName"].ToString()),
                (x["playtime"].ToObject<int>() / 60, x["playtime"].ToObject<int>() % 60))).ToList();
            //for (int i = 0; i < result.Songs.Count(); i++) {
            //    result.Songs[i].Album = await ((Album)result.Songs[i].Album).GetObject(result.Songs[i].Album.Id);
            //    if (result.Songs[i].Album.Photo != null) {
            //        result.Songs[i].Photo = result.Songs[i].Album.Photo;
            //        result.Songs[i].PhotoImg = await SetBitmap(result.Songs[i].Album.Photo);
            //    }
            //}
            return result;
        }
    }
}
