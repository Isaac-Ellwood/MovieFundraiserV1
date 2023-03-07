using System;
using System.Collections.Generic;

namespace MovieFundraiserV1
{
    class Program
    {
        static void Main(string[] args)
        {
            const float TICKETPRICE = 10.50f;

            List<string> snacksAvailable = new List<string>() {"popcorn","corn","chips"};
            List<float> snackPrices = new List<float>() { 1f, 2f, 1.5f};
            
            List<string> drinksAvailable = new List<string>() {"sprite","cocoa cola","fanta"};
            List<float> drinkPrices = new List<float>() { 3f, 2f, 2.5f};

            // test input data
            Console.WriteLine("<<<<<<<<<<TICKET HOLDER TESTING>>>>>>>>>");
            TicketHolder testTH = new TicketHolder("Isaac", 12, 3);

            List<int> s = new List<int>() {1,0};
            List<int> sQ = new List<int>() {12,1};

            testTH.AddSnacks(s,sQ);
            
            List<int> d = new List<int>() {0,2};
            List<int> dQ = new List<int>() {1,1};

            testTH.AddDrinks(d,dQ);

            testTH.SetCredit(true);

            // output
            Console.WriteLine($"{testTH.GenerateReciept(TICKETPRICE,snackPrices,drinkPrices,snacksAvailable,drinksAvailable)}");

            //ticket manager test
            Console.WriteLine("<<<<<<<<<<TICKET MANAGER TESTING>>>>>>>>>");

            TicketManager tm = new TicketManager();

            string name = "AIsaac";
            int age = 13;
            int tickets = 151;

            if (tm.CheckAge(age))
            {
                if (tm.CheckAvailableSeats(tickets))
                {
                    tm.AddTicketHolder(new TicketHolder(name, age, tickets));
                    Console.WriteLine($"Successfully purchased {tickets} tickets for {name}");
                    Console.WriteLine($"there are now {tm.CalculateAvailableSeats()} Tickets available");
                }
                else
                {
                    Console.WriteLine($"there are only {tm.CalculateAvailableSeats()} Tickets available");
                }
            }
            else
            {
                Console.WriteLine("You are too young to watch this movie");
            }

            tm.AddSnacksDrinksOrder(s, sQ, d, dQ);

            Console.WriteLine($"{tm.TotalSnacksOrdered()}");
        }
    }
}
