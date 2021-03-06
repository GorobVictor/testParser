﻿using LangitMusik.Model.Abstract;
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
    public class NewPlaylist : Parsing, IMainInformation, IListing {
        public NewPlaylist(int id = 0, string name = null, string description = null, string photo = null, List<NewSong> songs = null, Bitmap photoImg = null) {
            _id = id;
            _name = name;
            _photo = photo;
            _songs = songs;
            _photoImg = photoImg;
            _description = description;
        }

        private int _id { get; set; }
        private string _name { get; set; }
        private string _description { get; set; }
        private string _photo { get; set; }
        private Bitmap _photoImg { get; set; }
        private List<NewSong> _songs { get; set; }
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
        /// <summary>
        /// Парсинг плейлиста
        /// </summary>
        /// <param name="id">id плейлиста</param>
        /// <returns></returns>
        public async override Task<IMainInformation> ParsingJson(string id) {
            var json = JObject.Parse(await _client.GetStringAsync($"https://www.langitmusik.co.id/rest/playlist/details?playlistId={id}&page=1&limit=100"));
            var result = new NewPlaylist(
                json["detail"]["playlistId"].ToObject<int>(),
                json["detail"]["playlistName"].ToString(),
                json["detail"]["introduce"].ToString(),
                json["detail"]["playlistLImgPath"].ToString(),
                new List<NewSong>(),
                null);

            var listSound = (JArray)json["list"]["dataList"];
            result._songs = listSound.Select(x => new NewSong(
                x["songId"].ToObject<int>(),
                x["songName"].ToString(),
                x["songName"].ToString(),
                null,
                new NewAlbum(x["albumId"].ToObject<int>(), x["albumName"].ToString()),
                new NewArtist(x["artistId"].ToObject<int>(), x["artistName"].ToString()),
                (x["playtime"].ToObject<int>() / 60, x["playtime"].ToObject<int>() % 60))).ToList();

            #region Скачивание фото
            //for (int i = 0; i < result.Songs.Count(); i++) {
            //    result.Songs[i].Album = await ((Album)result.Songs[i].Album).GetObject(result.Songs[i].Album.Id);
            //    if (result.Songs[i].Album.Photo != null) {
            //        result.Songs[i].Photo = result.Songs[i].Album.Photo;
            //        result.Songs[i].PhotoImg = await SetBitmap(result.Songs[i].Album.Photo);
            //    }
            //}
            #endregion
            return result;
        }
    }
}
