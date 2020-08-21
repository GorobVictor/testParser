using System;
using System.Collections.Generic;
using System.Text;

namespace LangitMusik.Model.Interface {
    /// <summary>
    /// Песня
    /// </summary>
    interface ISong {
        (int, int) Duration { get; set; }
    }
}
