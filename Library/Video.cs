using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSearch.Library
{
    public class Video : MediaLibary
    {
        public string Format { get; set; }
        public int Length { get; set; }
        public int[] Regions { get; set; }

        public override void Display()
        {

            Console.WriteLine();
            Console.WriteLine($"Video ID: {Id}");
            Console.WriteLine($"Title:    {Title}");
            Console.WriteLine($"Format:   {Format}");
            Console.WriteLine($"Length:   {Length}");
            Console.WriteLine($"Format:   {string.Join(",", Regions)}");
            Console.WriteLine();
        }

        
    }
}
