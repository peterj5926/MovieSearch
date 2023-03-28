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

            string file2 = "shows.csv";
            StreamReader sr2 = new StreamReader(file2);
            sr2.ReadLine();
            Shows = new List<Show>();
            while (!sr2.EndOfStream)
            {
                string line = sr2.ReadLine();
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
            sr2.Close();

            string file3 = "videos.csv";
            StreamReader sr3 = new StreamReader(file3);
            sr3.ReadLine();
            Videos = new List<Video>();
            while (!sr3.EndOfStream)
            {
                string line = sr3.ReadLine();
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
            sr3.Close();

        }

    }

}

