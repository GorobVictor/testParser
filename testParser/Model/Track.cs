using PropertyChanged;
using System.Data;

namespace SampleProject.Backend.Model
{
    /// <summary>
    /// You dont need to fill all field.Only if you have same on api\info ypu get from site
    /// </summary>
    [AddINotifyPropertyChangedInterface]//attribute what dedicated to fix all the problems with INotifyPropertyChanged interface(you dont need to call RaisePropertyChanged() on every property)
    public class Track : ModelBase
    {
       
        public string Id { get; set; }
        public string Title { get; set; }
        public string AlbumId { get; set; }
        public string Duration { get; set; }
        public string ArtistId { get; set; }
        public string ImageLink { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Description { get; set; }
        public Track()
        {

        }
        public Track(DataRow i)
        {
            if (i.Table.Columns[1].ToString() == "Name")
            {
                Artist = i["Artist"]?.ToString();
                Title = i["Name"]?.ToString();
                Album = i["Album"]?.ToString();
                Id = i["TrackID"]?.ToString();
                Duration = i["Duration"]?.ToString();
            }
            else
            {
                Artist = i["Artist Name"]?.ToString()?.Replace("||", ",") ?? "";
                Title = i["Track Name"]?.ToString()?.Replace("||", ",") ?? "";
                Album = i["Album Name"]?.ToString()?.Replace("||", ",") ?? "";
                Id = i["Track ID"]?.ToString()?.Replace("||", ",") ?? "";
                Duration = i["Duration"]?.ToString()?.Replace("||", ",") ?? "";
            }
        }
    }
}
