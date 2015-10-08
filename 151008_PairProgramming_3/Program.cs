using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _151008_PairProgramming_3
{
    class Program
    {
        static void Main(string[] args)
        {          
                                    
            //Movies Info Lists

            List<string> listA = new List<string>();
            listA.Add("movieA"); //movie name
            listA.Add("cust1"); //customer name
            listA.Add("2015, 07, 02"); // checkout
            listA.Add("2015, 07, 05"); //due
            listA.Add("2015, 07, 03"); //returned            
            
            List<string> listB = new List<string>();
            listB.Add("movieB"); //movie name
            listB.Add("cust2"); //customer name
            listB.Add("2015, 10, 02"); // checkout
            listB.Add("2015, 10, 12"); //due
            listB.Add("2015, 10, 14"); //returned
            
            List<string> listC = new List<string>();
            listC.Add("movieC"); //movie name 
            listC.Add("cust3"); //customer name 
            listC.Add("2015, 10, 02"); // checkout 
            listC.Add("2015, 10, 05"); //due 
            listC.Add(" "); // returned 
                        
            //Customers Dictionary
            Dictionary<string, List<string>> customers = new Dictionary<string, List<string>>();
            customers.Add("cust1", listA);
            customers.Add("cust2", listB);
            customers.Add("cust3", listC);

            //Movies Dictionary
            Dictionary<string, List<string>> movies = new Dictionary<string, List<string>>();
            movies.Add("movieA", listA);
            movies.Add("movieB", listB);
            movies.Add("movieC", listC);

            //file input
                       
            StreamReader reader = new StreamReader("..\\..\\Input.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] lineArr;
                    List<string> lineList = new List<string>();

                    lineArr = line.Split(';');
                    lineList = lineArr.ToList<string>();
                    movies.Add(lineArr[0], lineList);                    
                    //lineList.Add(IsOverdue(lineList[3], lineList[4]));
                    
                    line = reader.ReadLine();
                }
            }

            //Call to interact through console
            Interact(movies, customers);
                    
        }

        static Dictionary<string, List<string>> Interact(Dictionary<string, List<string>> movies, Dictionary<string, List<string>> customers)
        {
            Console.WriteLine("What action would you like to take? Enter C to search by customer. Enter M for search by movie. Enter S for an overdue summary.");
            string answer = Console.ReadLine();
            answer = answer.ToLower();

            if (answer == "m")
            {
                Console.Write("Movie name: ");
                string name = Console.ReadLine();
                //movie search
                if (movies.ContainsKey(name))
                {
                    foreach (KeyValuePair<string, List<string>> movie in movies)
                    {
                        string movieKey = movie.Key;
                        if (name == movieKey)
                        {
                            Console.WriteLine("{0} was found.", name);
                            Console.WriteLine("Enter A for the customer name. Enter B for the checkout date. Enter C for the due date. Enter D for the date returned. Enter E to check if overdue.");
                            string command = (Console.ReadLine()).ToLower();
                            switch (command)
                            {
                                case "a":
                                    Console.WriteLine(movie.Value[1]);
                                    Interact(movies, customers);
                                    break;

                                case "b":
                                    Console.WriteLine(movie.Value[2]);
                                    Interact(movies, customers);
                                    break;

                                case "c":
                                    Console.WriteLine(movie.Value[3]);
                                    Interact(movies, customers);
                                    break;

                                case "d":
                                    if (movie.Value[4] != " ")
                                    {
                                        Console.WriteLine(movie.Value[4]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("This movie has not been returned yet.");
                                    }
                                    Interact(movies, customers);
                                    break;

                                case "e":
                                    Console.WriteLine(IsOverdue(movie.Value[3], movie.Value[4]));
                                    Interact(movies, customers);
                                    break;

                                default:
                                    Console.WriteLine("Please enter a valid command.");
                                    Interact(movies, customers);
                                    break;
                            }
                        }

                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Movie not found.");
                    Interact(movies, customers);
                }                               
            }
            else if (answer == "c")
            {
                Console.Write("Customer name: ");
                string name = Console.ReadLine();
                //customer search
                if (customers.ContainsKey(name))
                {
                    foreach (KeyValuePair<string, List<string>> customer in customers)
                    {
                        string custKey = customer.Key;
                        if (name == custKey)
                        {
                            Console.WriteLine("{0} was found.", name);
                            Console.WriteLine("Enter A for the movie name. Enter B for the checkout date. Enter C for the due date. Enter D for the date returned. Enter E to check if overdue.");
                            string command = (Console.ReadLine()).ToLower();
                            switch (command)
                            {
                                case "a":
                                    Console.WriteLine(customer.Value[0]);
                                    Interact(movies, customers);
                                    break;

                                case "b":
                                    Console.WriteLine(customer.Value[2]);
                                    Interact(movies, customers);
                                    break;

                                case "c":
                                    Console.WriteLine(customer.Value[3]);
                                    Interact(movies,customers);
                                    break;

                                case "d":
                                    if (customer.Value[4] != " ")
                                    {
                                        Console.WriteLine(customer.Value[4]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("This movie has not been returned yet.");
                                    }
                                    Interact(movies, customers);
                                    break;

                                case "e":
                                    Console.WriteLine(IsOverdue(customer.Value[3], customer.Value[4]));
                                    Interact(movies, customers);
                                    break;

                                default:
                                    Console.WriteLine("Please enter a valid command.");
                                    Interact(movies, customers);
                                    break;
                            }
                        }

                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Movie not found.");
                    Interact(movies, customers);
                }
            }
            else //summary
            {
                Console.WriteLine("Overdue accounts: ");
                foreach (KeyValuePair<string, List<string>> movie in movies)
                {
                    if (IsOverdue(movie.Value[3], movie.Value[4]) == "overdue: fees applied")
                    {
                        Console.WriteLine("{0} : {1}", movie.Value[1], movie.Value[0]);                        
                    }
                    else
                    {
                        continue;
                    }                    
                }
                Interact(movies, customers);
            }
            return movies;                       
        }

        static string IsOverdue(string dueDate, string returnDate)
        {
            if (returnDate == " ")
            {
                DateTime due = DateTime.Parse(dueDate);
                if (due >= DateTime.Now)
                {
                    return "not overdue";
                }
                else
                {
                    return "overdue: fees applied";
                }
            }
            else
            {
                DateTime due = DateTime.Parse(dueDate);
                DateTime returned = DateTime.Parse(returnDate);

                if (due < returned)
                {
                    return "overdue: fees applied";
                }
                else
                {
                    return "not overdue";
                }
            }

        }
    }
}
