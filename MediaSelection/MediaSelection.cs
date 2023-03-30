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
                Console.WriteLine("Available Media for Display");
                Console.WriteLine("1. Search for a Media Title");
                Console.WriteLine("2. Display all Movies");
                Console.WriteLine("3. Display all Shows");
                Console.WriteLine("4. Display all Videos");
                Console.WriteLine("5. Exit");
                choice = Input.GetIntWithPrompt("Please select a number to view: ", "That is not a number, Please try again");
                if (choice > 5 || choice < 1)
                {
                    Console.WriteLine();
                    Console.WriteLine("I'm sorry, that is not a Menu option!");
                }
                else if (choice == 1)
                {
                    loadMedia();
                    Console.WriteLine();
                    Console.WriteLine("What Title would you like to search?");
                    var titleSearch = Console.ReadLine();
                    List<MediaLibary> results = Libaries.Where(x => x.Title.Contains(titleSearch, StringComparison.OrdinalIgnoreCase)).ToList();
                    var mediaGroup = results.GroupBy(s => s.GetType().Name);
                    
                   
                    
                    
                    var mediacount = new List<int>() {results.Count(), 
                        results.Where(x => x.GetType().Name == "Movie").Count(),
                        results.Where(x => x.GetType().Name == "Show").Count(),
                        results.Where(x => x.GetType().Name == "Video").Count()};
                    
                    Console.WriteLine();
                   
                    Console.WriteLine($"There are {mediacount[0]} references that match your selection.");
                    if (mediacount[0] > 0)
                    {
                       
                        Console.WriteLine($"There are {mediacount[1]} Movies, {mediacount[2]} Shows, and {mediacount[3]} Videos that match your search. ");
                        int displayType = Input.GetIntWithPrompt("Press 1 for the Titles or 2 to display their full details: ", "Please try again:");
                        do
                        {
                            if (displayType > 2 || displayType < 1)
                            {
                                Console.WriteLine("Please press 1 for a list of Titles or 2 to display the full details of your media title search");
                                displayType = Input.GetIntWithPrompt("How would you like you results displayed?", "Please try again.");
                            }
                        }while (displayType > 2 || displayType < 1);

                        if (displayType == 1)
                        {
                            
                            foreach (var mgroup in mediaGroup)
                            {
                                Console.WriteLine();
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
                                Console.WriteLine();
                                Console.WriteLine("Media Reference: {0}", mgroup.Key);

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
           // Libaries = new List<MediaLibary>();
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
           // Libaries = new List<MediaLibary>();
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