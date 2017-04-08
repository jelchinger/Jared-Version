using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ContainsLocation { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int FileId { get; set; }


    }
}
