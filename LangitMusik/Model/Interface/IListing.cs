using System.Collections.Generic;

namespace LangitMusik.Model.Interface {
    /// <summary>
    /// Содержание массива песен
    /// </summary>
    public interface IListing {
        List<NewSong> Songs { get; set; }
    }
}
