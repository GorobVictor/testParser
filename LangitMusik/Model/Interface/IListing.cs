using System.Collections.Generic;

namespace LangitMusik.Model.Interface {
    /// <summary>
    /// Содержание массива фото
    /// </summary>
    public interface IListing {
        List<Song> Songs { get; set; }
    }
}
