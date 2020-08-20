using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangitMusik.Model.Interface {
    public interface IMainInformation {
        int Id { get; set; }
        string Name { get; set; }
        string Photo { get; set; }
        Bitmap PhotoImg { get; set; }
    }
}
