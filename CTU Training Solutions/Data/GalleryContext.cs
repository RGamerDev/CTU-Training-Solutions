using CTU_Training_Solutions.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTU_Training_Solutions.Data
{
    class GalleryContext
    {
        public ObservableCollection<Gallery> galleries;

        public GalleryContext()
        {
            galleries = new ObservableCollection<Gallery>()
            {
                new Gallery{ImageSource = "ms-appx:///Assets/Event pictures\\download.jpg"},
            };
        }

        //public void Display(MapControl mc, Func<Gallery, string> image)
        //{
        //    foreach (var item in galleries)
        //    {
        //        MapIcon mapIcon = new MapIcon()
        //        {
        //            Title = title.Invoke(item),
        //            Location = new Geopoint(new BasicGeoposition { Latitude = geopoint.Invoke(item).Position.Latitude, Longitude = geopoint.Invoke(item).Position.Longitude })
        //        };

        //        mc.MapElements.Add(mapIcon);
        //    }
        //}
    }
}
