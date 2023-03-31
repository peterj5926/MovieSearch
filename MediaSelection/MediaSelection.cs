using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.VisualBasic;
using MovieSearch.Helper;
using MovieSearch.Library;
using static System.Net.WebRequestMethods;

namespace MovieSearch.MediaSelection
{
    public class MediaSelection
    {
        public List<Movie> Movies { get; set; }
        public List<Show> Shows { get; set; }
        public List<Video> Videos { get; set; }
        public List<MediaLibary> Libaries { get; set; }



        public void Search()
        {
            int choice;
            bool isComplete = false;
            Console.WriteLine("Welcome to the Media Libary!");

            do
            {
                Console.WriteLine();
                Console.WriteLine("*** Media Menu Options ***");
                Console.WriteLine("1. Search for a Media Title");
                Console.WriteLine("2. Display all Movies");
                Console.WriteLine("3. Display all Shows");
                Console.WriteLine("4. Display all Videos");
                Console.WriteLine("5. Exit");
                choice = Input.GetIntWithPrompt("Please select a Menu option: ", "That is not a number, Please try again");
                if (choice > 5 || choice < 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("I'm sorry, that is not a Menu option!");                                                                 //We had just started abstract classes when I did this the first time so it was
                }                                                                                                                               // set up to load each class individually for display because we had talked about
                else if (choice == 1)                                                                                                           // not wasting memory on things that weren't needed. But to search all types of media
                {                                                                                                                               // I needed to load them all so I created a new method using the base class to access the
                    loadMedia();                                                                                                                // derived classes to only have one search across all media types. I saw you do this in
                    Console.WriteLine();                                                                                                        // a class example and wanted to try it. The example from the last class had three searches
                    Console.WriteLine("What Title would you like to search?");                                                                  // that then were added to a new list of the base media class. This skips that step and has
                    var titleSearch = Console.ReadLine();                                                                                       // one search for all media per the assignment instructions or note listed at the end.
                    List<MediaLibary> results = Libaries.Where(x => x.Title.Contains(titleSearch, StringComparison.OrdinalIgnoreCase)).ToList();// I know you said it didn't matter how we loaded it but I wanted to try something new. 
                    var mediaGroup = results.GroupBy(s => s.GetType().Name);                                                                    // You said to play around with the Linq so now that I could search all of the media at once   
                                                                                                                                                // I wanted to group the result together so I could count the number of results and the number 
                    var groupCount = from c in results                                                                                          // of results in each group. The mediaCount worked but say we added DVD's, I would have to 
                             group c by c.GetType().Name into g                                                                                 // change multiple lines of code to get the count and display the results. The groupCount 
                             select new {Reference = g.Key, Total = g.Count() };                                                                // I found would auto generate and incorporate whatever GetTypes it found which was cool.
                    int displayType = 0;                                                                                                        // A lot of 'if' statements were added because the grammer of the output was bugging me,
                    //var mediacount = new List<int>() {results.Count(),                                                                        // is and are's, singular and plurals, didn't match the results. Using a much smarter way  
                    //    results.Where(x => x.GetType().Name == "Movie").Count(),                                                              // to code was cool but the output sounded weird for the end user to read, lol.
                    //    results.Where(x => x.GetType().Name == "Show").Count(),
                    //    results.Where(x => x.GetType().Name == "Video").Count()};

                    if (mediaGroup.Count() != 1 )
                    {
                        Console.WriteLine($"There are {results.Count()} references that match your selection.");
                    }
                   
                    Console.WriteLine();
                    if (mediaGroup.Count() > 0)
                    {
                        foreach (var g in groupCount)
                            if (g.Total == 1)
                            {
                                Console.WriteLine("There is {0} {1} reference that match your search", g.Total, g.Reference);
                            }
                            else
                            {
                                Console.WriteLine("There are {0} {1} references that match your search", g.Total, g.Reference);
                            }
                        Console.WriteLine();
                        // Console.WriteLine($"There are {mediacount[1]} Movies, {mediacount[2]} Shows, and {mediacount[3]} Videos that match your search. ");  // This used the mediacount and would just be another line of code to update if
                        if (mediaGroup.Count() == 1)                                                                                                            // a new type of media was added or what if one was taken away. And it gave counts
                        {                                                                                                                                       // for results with no matches
                            displayType = Input.GetIntWithPrompt("Press 1 for the Title info or 2 to display it's full details: ", "Please try again:");
                            do
                            {
                                if (displayType > 2 || displayType < 1)
                                {
                                    Console.WriteLine("Please press 1 for a list of Title or 2 to display it's full details of your media title search");
                                    displayType = Input.GetIntWithPrompt("How would you like you results displayed?", "Please try again.");
                                }
                            } while (displayType > 2 || displayType < 1);
                        }
                        else
                        {
                            displayType = Input.GetIntWithPrompt("Press 1 for the Titles or 2 to display their full details: ", "Please try again:");
                            do
                            {
                                if (displayType > 2 || displayType < 1)
                                {
                                    Console.WriteLine("Please press 1 for a list of Titles or 2 to display the full details of your media title search");
                                    displayType = Input.GetIntWithPrompt("How would you like you results displayed?", "Please try again.");
                                }
                            } while (displayType > 2 || displayType < 1);
                        }
                        if (displayType == 1)
                        {
                            
                            foreach (var mgroup in mediaGroup)
                            {                                                                                               // I found this that uses the results grouping to display the results. 
                                Console.WriteLine();                                                                        // I wanted there to be options for the user on how the results were returned.
                                Console.WriteLine("Media Reference: {0}", mgroup.Key);

                                foreach (MediaLibary s in mgroup)
                                    Console.WriteLine("      {0}", s.Title);
                            }
                            //foreach (var media in results)
                            //{
                            //    Console.WriteLine($"{media.GetType().Name} Reference: {media.Title} ");
                                
                            //}
                        }
                        else if (displayType == 2)
                        {
                            
                            foreach (var mgroup in mediaGroup)
                            {
                                Console.WriteLine();                                                                        // I liked using the abstract methods through the base class for displaying  
                                Console.WriteLine("Media Reference: {0}", mgroup.Key);                                      // each classes display method with the grouping.

                                foreach (MediaLibary s in mgroup)
                                    s.Display();
                            }
                            //foreach (var media in results)
                            //{
                            //    Console.WriteLine($"{media.GetType().Name} Reference: ");
                            //    media.Display();
                            //}
                        }
                    }

                   
                }
                else if (choice == 2)
                {
                    string file = "movies.csv";                                                       
                    StreamReader sr = new StreamReader(file);                                     
                    sr.ReadLine();                                                                
                    Movies = new List<Movie>();
                  
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] movieDetails = line.Split(',');
                        Movies.Add(new Movie()
                        {
                            Id = (int)ulong.Parse(movieDetails[0]),
                            Title = movieDetails[1],
                            Genres = (string[])movieDetails[2].Split('|')
                        });

                    }
                    sr.Close();
                    foreach (Movie movie in Movies)
                    {
                        movie.Display();
                    }
                    Movies.Clear();
                    
                                                                                                  

                }
                else if (choice == 3)
                {
                    string file = "shows.csv";
                    StreamReader sr = new StreamReader(file);
                    sr.ReadLine();
                    Shows = new List<Show>();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] showDetails = line.Split(',');
                        Shows.Add(new Show()
                        {
                            Id = (int)ulong.Parse(showDetails[0]),
                            Title = showDetails[1],
                            Season = (int)ulong.Parse(showDetails[2]),
                            Episode = (int)ulong.Parse(showDetails[3]),
                            Writers = (string[])showDetails[4].Split('|')
                        });
                    }
                    sr.Close();
                    foreach (Show sho in Shows)
                    {
                        sho.Display();
                    }
                    Shows.Clear();


                }
                else if (choice == 4)
                {
                    string file = "videos.csv";
                    StreamReader sr = new StreamReader(file);
                    sr.ReadLine();
                    Videos = new List<Video>();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] videoDetails = line.Split(',');
                        Videos.Add(new Video()
                        {
                            Id = (int)ulong.Parse(videoDetails[0]),
                            Title = videoDetails[1],
                            Format = videoDetails[2],
                            Length = (int)ulong.Parse(videoDetails[3]),
                            Regions = Array.ConvertAll(videoDetails[4].Split('|'), s => int.Parse(s))
                        });

                    }
                    sr.Close();
                    foreach (Video v in Videos)
                    {
                        v.Display();
                    }
                    Videos.Clear();


                }
                else if (choice == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you for using the Media Libary, Good Bye!");
                    isComplete = true;
                }
            } while (!isComplete);

            
        }
        public void loadMedia()
        {
            string file = "movies.csv";
            StreamReader sr = new StreamReader(file);
            sr.ReadLine();
            Libaries = new List<MediaLibary>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] movieDetails = line.Split(',');
                Libaries.Add(new Movie()
                {
                    Id = (int)ulong.Parse(movieDetails[0]),
                    Title = movieDetails[1],
                    Genres = (string[])movieDetails[2].Split('|')
                });

            }
            sr.Close();

            string file2 = "shows.csv";
            StreamReader sr2 = new StreamReader(file2);
            sr2.ReadLine();
           
            while (!sr2.EndOfStream)
            {
                string line = sr2.ReadLine();
                string[] showDetails = line.Split(',');
                Libaries.Add(new Show()
                {
                    Id = (int)ulong.Parse(showDetails[0]),
                    Title = showDetails[1],
                    Season = (int)ulong.Parse(showDetails[2]),
                    Episode = (int)ulong.Parse(showDetails[3]),
                    Writers = (string[])showDetails[4].Split('|')
                });
            }
            sr2.Close();

            string file3 = "videos.csv";
            StreamReader sr3 = new StreamReader(file3);
            sr3.ReadLine();
           
            while (!sr3.EndOfStream)
            {
                string line = sr3.ReadLine();
                string[] videoDetails = line.Split(',');
                Libaries.Add(new Video()
                {
                    Id = (int)ulong.Parse(videoDetails[0]),
                    Title = videoDetails[1],
                    Format = videoDetails[2],
                    Length = (int)ulong.Parse(videoDetails[3]),
                    Regions = Array.ConvertAll(videoDetails[4].Split('|'), s => int.Parse(s))
                });

            }
            sr3.Close();

        }

    }

}

//Trash Can
//List<Movie> movies = Movies.Where(x => x.Title.Contains(titleSearch, StringComparison.OrdinalIgnoreCase)).ToList();
//List<Show> shows = Shows.Where(x => x.Title.Contains(titleSearch,StringComparison.OrdinalIgnoreCase)).ToList();
//List<Video> videos = Videos.Where(x => x.Title.Contains(titleSearch, StringComparison.OrdinalIgnoreCase)).ToList();

//List<MediaLibary> results = new List<MediaLibary>();
//List<MediaLibary> results
//results.AddRange(movies);
//results.AddRange(shows);
//results.AddRange(videos);

//var count = results.Count();
//var mcount = results.Where(x => x.GetType().Name == "Movie").Count();
//var scount = results.Where(x => x.GetType().Name == "Show").Count();
//var vcount = results.Where(x => x.GetType().Name == "Video").Count();
//Console.WriteLine($"There are {count} references that match your selection.");
//Console.WriteLine($"There are {mcount} Movies, {scount} Shows, and {vcount} Videos that match your search. ");
//var mc = results.GroupBy(c => c.GetType().Name).Count();