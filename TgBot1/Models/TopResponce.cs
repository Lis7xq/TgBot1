using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicaBot.Models
{

    public class TopResponce
    {
        public List<Trackss> trackss { get; set; }
    }

    public  class  Trackss
    {
        
        public string sTrack { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }

        public string Genre { get; set; }
        public string Mood { get; set; }
        public string Style { get; set; }

        public string DescriptionEN { get; set; }

        public string TrackThumb { get; set; }

        public string MusicVidViews { get; set; }
        public string MusicVidLikes { get; set; }

        public string TotalPlays { get; set; }

        public string strTrackThumb { get; set; }
        public string strMusicVid { get; set; }
    }
}
