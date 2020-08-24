using PropertyChanged;
using System.Collections.Generic;

namespace SampleProject.Backend.Model
{
    /// <summary>
    /// You dont need to fill all field.Only if you have same on api\info ypu get from site
    /// </summary>
    [AddINotifyPropertyChangedInterface]//attribute what dedicated to fix all the problems with INotifyPropertyChanged interface(you dont need to call RaisePropertyChanged() on every property)
    public class Playlist : ModelBase
    {
        public string Id { get; set; }
        public List<Track> AllTracks { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
    }
}
