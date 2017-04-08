using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2.Models;

namespace Angular2.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProjectContext context)
        {
            context.Database.EnsureCreated();
            if(context.Photos.Any())
            {
                return;
            }
            else
            {
                var photos = new Photo[]
                {
                    new Photo{Name="PhotoOne",ContainsLocation=false, Latitude= 0.0, Longitude= 0.0, FileId=11},
                    new Photo{Name="PhotoTwo",ContainsLocation=false, Latitude= 0.0, Longitude= 0.0, FileId=12},
                    new Photo{Name="PhotoThree",ContainsLocation=false, Latitude= 0.0, Longitude= 0.0, FileId=13},
                    new Photo{Name="PhotoFour",ContainsLocation=false, Latitude= 0.0, Longitude= 0.0, FileId=14},
                    new Photo{Name="PhotoFive",ContainsLocation=false, Latitude= 0.0, Longitude= 0.0, FileId=15},
                    new Photo{Name="PhotoSix",ContainsLocation=false, Latitude= 0.0, Longitude= 0.0, FileId=16},
                    new Photo{Name="PhotoSever",ContainsLocation=false, Latitude= 0.0, Longitude= 0.0, FileId=17}
                };

                foreach(var p in photos)
                {
                    context.Photos.Add(p);
                }
                context.SaveChangesAsync();

                var audios = new Audio[]
                {
                     new Audio{Name="AudioOne",UniqueIdentifier=1},
                     new Audio{Name="AudioTwo",UniqueIdentifier=2},
                     new Audio{Name="AudioThree",UniqueIdentifier=3},
                     new Audio{Name="AudioFour",UniqueIdentifier=4},
                     new Audio{Name="AudioFive",UniqueIdentifier=5},
                     new Audio{Name="AudioSix",UniqueIdentifier=6}
                };

                foreach(var a in audios)
                {
                    context.Audios.Add(a);
                }
                context.SaveChangesAsync();

            }
        }
    }
}
