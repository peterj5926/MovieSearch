using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch.Library
{
    public class Show : MediaLibary
    {
        public int Season { get; set; }
        public int Episode { get; set; }
        public string[] Writers { get; set; }

        public override void Display()
        {

            Console.WriteLine();
            Console.WriteLine($"Show ID: {Id}");
            Console.WriteLine($"Title:   {Title}");
            Console.WriteLine($"Season:  {Season}");
            Console.WriteLine($"Episode: {Episode}");
            Console.WriteLine($"Writer:  {string.Join(",", Writers)}");
            Console.WriteLine();
        }

       
    }
}
