using System;
using System.Collections.Generic;

namespace MovieFundraiserV1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> snacksAvailable = new List<string>() {"popcorn","corn","chips"};
            List<float> snackPrices = new List<float>() { 1f, 2f, 1.5f};
            
            List<string> drinksAvailable = new List<string>() {"coke","cocoa cola","refined cola"};
            List<float> drinkPrices = new List<float>() { 100f, 2f, 2.5f};

            Console.WriteLine("Hello World!");
            TicketHolder testTH = new TicketHolder("aiSAAC", 17, 1);

            testTH.SetAge(12);

            List<int> s = new List<int>() {1,0};
            List<int> sQ = new List<int>() {12,1};

            testTH.AddSnacks(s,sQ);
            
            List<int> d = new List<int>() {0,2};
            List<int> dQ = new List<int>() {1,1};

            testTH.AddDrinks(d,dQ);

            Console.WriteLine($"{testTH.GenerateReciept()}");
            Console.WriteLine($"{testTH.CalculateTotalCost(snackPrices,drinkPrices)}");
        }
    }
}
