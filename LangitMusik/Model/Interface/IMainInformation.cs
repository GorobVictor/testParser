using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangitMusik.Model.Interface {
    /// <summary>
    /// Основная информацич(id, название, фото)
    /// </summary>
    public interface IMainInformation {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Photo { get; set; }
        Bitmap PhotoImg { get; set; }
    }
}
