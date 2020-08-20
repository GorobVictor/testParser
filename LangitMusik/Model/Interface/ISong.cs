using System;
using System.Collections.Generic;
using System.Text;

namespace LangitMusik.Model.Interface {
    interface ISong {
        (int, int) Duration { get; set; }
    }
}
