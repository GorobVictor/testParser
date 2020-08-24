using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Backend.Model
{
   [AddINotifyPropertyChangedInterface]
    public class ModelBase
    {
        public bool IsSelected { get; set; } = true;
    }
}
