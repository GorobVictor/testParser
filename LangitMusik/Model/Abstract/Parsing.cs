using AngleSharp.Html.Parser;
using LangitMusik.Model.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangitMusik.Model.Abstract {
    public abstract class Parsing {
        protected static HttpClient _client = new HttpClient();
        protected static WebClient _webclient = new WebClient();
        protected static IHtmlParser _parser = new HtmlParser();
        public abstract Task<IMainInformation> ParsingJson(string id);
        public async Task<IMainInformation> GetObject(string id) {
            int _try = 0;
            while (_try < 5)
                try {
                    return await ParsingJson(id);
                }
                catch (Exception ex) {
                    if (ex.Message.IndexOf("404") != -1)
                        return null;
                    else if (ex.Message.IndexOf("501") != -1) {
                        _try++;
                        continue;
                    }
                    else if (ex.Message.IndexOf("503") != -1) {
                        _try++;
                        continue;
                    }
                    else return null;
                }
            return null;
        }
        public async Task<IMainInformation> GetObject(int id) => await GetObject(id.ToString());
        public async Task<Bitmap> SetBitmap(string url) {
            try {
                Bitmap result = null;
                var img = await _webclient.OpenReadTaskAsync(new Uri(url));
                await Task.Run(() => { result = new Bitmap(img); });
                return result;
            }
            catch { return new Bitmap("Properties\\Why.jpg"); }
        }
    }
}
