using System;
using System.Collections.Generic;

namespace VideoGameDBServices.Models
{
    public partial class Games
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SystemName { get; set; }
        public string Year { get; set; }
        public string Rating { get; set; }
        public string DeveloperName { get; set; }
        public string PublisherName { get; set; }
        public string Players { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }

    }
}
