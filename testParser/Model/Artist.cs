using PropertyChanged;

namespace SampleProject.Backend.Model
{
    /// <summary>
    /// You dont need to fill all field.Only if you have same on api\info ypu get from site
    /// </summary>
    [AddINotifyPropertyChangedInterface]//attribute what dedicated to fix all the problems with INotifyPropertyChanged interface(you dont need to call RaisePropertyChanged() on every property)
    public class Artist : ModelBase
    {
        public string Id { get; set; }
        public string Fans { get; set; }

        public string Description { get; set; }
        public string ImageLink { get; set; }

        public string ArtistName { get; set; }
        public string Album { get; set; }
    }
}
