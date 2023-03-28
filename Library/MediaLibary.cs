using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch.Library
{
    public abstract class MediaLibary
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public abstract void Display();

       
    }


}
