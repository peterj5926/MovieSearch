using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch.Library
{
    public class Movie : MediaLibary
    {
        public string[] Genres { get; set; }

        public override void Display()
        {

            Console.WriteLine();
            Console.WriteLine($"Movie ID: {Id}");
            Console.WriteLine($"Title:    {Title}");
            Console.WriteLine($"Genres:   {string.Join(",", Genres)}");
            Console.WriteLine();

        }

       
    }
}
