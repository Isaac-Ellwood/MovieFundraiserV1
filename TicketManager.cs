using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFundraiserV1
{
    class TicketManager
    {
        //attributes
        private List<TicketHolder> ticketHolders = new List<TicketHolder>();

        float ticketPrice;

        List<string> snacksAvailable = new List<string>() { "popcorn", "corn", "chips" };
        List<float> snackPrices = new List<float>() { 1f, 2f, 1.5f };

        List<string> drinksAvailable = new List<string>() { "coke", "cocoa cola", "refined cola" };
        List<float> drinkPrices = new List<float>() { 100f, 2f, 2.5f };

        int ageLimit = 12;
        const int SEATLIMIT = 150;

        //constructs a ticketmanager object
        public TicketManager()
        {

        }

        //Adds a ticket holder into ticketHolders list
        public void AddTicketHolder(TicketHolder ticketHolder)
        {
            ticketHolders.Add(ticketHolder);
        }

        //stores the snacks and drinks ordered by the most recent ticket holder
        public void AddSnacksDrinksOrder(List<int> sOrder, List<int> sQuantity, List<int> dOrder, List<int> dQuantity)
        {
            ticketHolders[ticketHolders.Count-1].AddSnacks(sOrder,sQuantity);
            ticketHolders[ticketHolders.Count-1].AddSnacks(dOrder, dQuantity);
        }

        //returns true if purchasers age meets age requirements else it returns false
        public bool CheckAge(int buyerAge)
        {
            if (buyerAge < ageLimit)
            {
                return false;
            }

            return true;
        }

        //calculates the amount of available seats
        public int CalculateAvailableSeats()
        {
            int sumTickets = 0;

            foreach (TicketHolder ticketHolder in ticketHolders)
            {
                sumTickets += ticketHolder.GetTickets();
            }

            return SEATLIMIT - sumTickets;
        }

        //returns true if enough seats are available
        public bool CheckAvailableSeats(int ticketAmount)
        {
            if (ticketAmount > CalculateAvailableSeats())
            {
                return false;
            }

            return true;
        }

        //adds new snack and snack prices to the snack and price lists
        public void AddSnack(string snack, float price)
        {
            snacksAvailable.Add(snack);
            snackPrices.Add(price);
        }

        //sets new age limit
        public void SetAgeLimit()
        {

        }

        //sets a new value to the ticket price
        public void ChangeTicketPrice(float newTicketPrice)
        {
            ticketPrice = newTicketPrice;
        }

        //calculates ticket gross profit
        public float CalculateTicketGrossProfit()
        {
            int sumTicketSold = 0;

            foreach (TicketHolder ticketHolder in ticketHolders)
            {
                sumTicketSold += ticketHolder.GetTickets();
            }

            return sumTicketSold * ticketPrice;
        }

        private List<int> SumItemsSold(string itemType)
        {
            List<int>sumItemsSold = new List<int>();
            foreach (TicketHolder ticketHolder in ticketHolders)
            {
                List<int> orderedItems, itemQuantities = new List<int>();
                int itemAvailableListLength = 0;

                if (itemType == "snacks")
                {
                    orderedItems = ticketHolder.GetSnackOrder();
                    itemQuantities = ticketHolder.GetSnackQuantity();
                    itemAvailableListLength = snackPrices.Count;
                }
                else
                {
                    orderedItems = ticketHolder.GetDrinkOrder();
                    itemQuantities = ticketHolder.GetDrinkQuantity();
                    itemAvailableListLength = drinkPrices.Count;
                }

                //loop through ordered items
                for (int orderIndex = 0; orderIndex < orderedItems.Count; orderIndex++)
                {
                    //loop through available items
                    for (int itemIndex = 0; itemIndex < itemAvailableListLength; itemIndex++)
                    {
                        //check if ticketHolder has purchased item
                        if (itemIndex == orderedItems[orderIndex])
                        {
                            //adds quantity to sumItemsSold
                            sumItemsSold[itemIndex] += itemQuantities[orderIndex];
                        }
                    }
                }
            }

            //returns list
            return sumItemsSold;
        }

        public float CalculateItemGrossProfit()
        {
            //calculate gross profit by multiplying the sum of the gross profit by it's price
            float grossProfit = 0;

            // makes lists 
            List<int> sumSnacksSold = SumItemsSold("snacks");
            List<int> sumDrinksSold = SumItemsSold("drinks");

            // loop through SumSnacksSold
            for (int sumSnackIndex = 0; sumSnackIndex < snacksAvailable.Count; sumSnackIndex++)
            {
                grossProfit += sumSnacksSold[sumSnackIndex] * drinkPrices[sumSnackIndex];
            }

            // loop through SumDrinksSold
            for (int sumDrinkIndex = 0; sumDrinkIndex < drinksAvailable.Count; sumDrinkIndex++)
            {
                grossProfit += sumDrinksSold[sumDrinkIndex] * drinkPrices[sumDrinkIndex];
            }

            return grossProfit;
        }

        //calculate gross profit for snack and drink sales
        public float CalculateSnackDrinkGrossProfit()
        {
            //collections storing total quantity sold for snacks and drinks
            List<int> SumSnacksSold = new List<int>();
            List<int> SumDrinksSold = new List<int>();


            //loop through ticketHolders, calculating sum of quantitys sold for each snack
            foreach (TicketHolder ticketHolder in ticketHolders)
            {
                //store the ticketholders ordered snacks and their quantities
                List<int> orderedSnacks = ticketHolder.GetSnackOrder();
                List<int> quantitySnacks = ticketHolder.GetSnackQuantity();

                //loop through ordered snacks
                for (int orderIndex = 0; orderIndex < orderedSnacks.Count; orderIndex++)
                {
                    //loop through available snacks
                    for (int snackIndex = 0; snackIndex < snacksAvailable.Count; snackIndex++)
                    {
                        //check if ticketHolder has purchased snack
                        if (snackIndex == orderedSnacks[orderIndex])
                        {
                            //adds quantity to SumSnacksSold
                            SumSnacksSold[snackIndex] += quantitySnacks[orderIndex];
                        }
                    }
                }

                //store the ticketholders ordered drinks and their quantities
                List<int> ordereddrinks = ticketHolder.GetDrinkOrder();
                List<int> quantitydrinks = ticketHolder.GetDrinkQuantity();

                //loop through ordered snacks
                for (int orderIndex = 0; orderIndex < ordereddrinks.Count; orderIndex++)
                {
                    //loop through available drinks
                    for (int drinkIndex = 0; drinkIndex < drinksAvailable.Count; drinkIndex++)
                    {
                        //check if ticketHolder has purchased drink
                        if (drinkIndex == ordereddrinks[orderIndex])
                        {
                            //adds quantity to SumdrinksSold
                            SumDrinksSold[drinkIndex] += quantitydrinks[orderIndex];
                        }
                    }
                }
            }

            //calculate gross profit by multiplying the sum of the gross profit by it's price
            float grossProfit = 0;

            // loop through SumSnacksSold
            for (int sumSnackIndex = 0; sumSnackIndex < SumSnacksSold.Count; sumSnackIndex++)
            {
                grossProfit += SumSnacksSold[sumSnackIndex] * drinkPrices[sumSnackIndex];
            }

            // loop through SumDrinksSold
            for (int sumDrinkIndex = 0; sumDrinkIndex < SumDrinksSold.Count; sumDrinkIndex++)
            {
                grossProfit += SumDrinksSold[sumDrinkIndex] * drinkPrices[sumDrinkIndex];
            }

            return grossProfit;
        }

        private float CalculateSnackDrinkTotalProfit()
        {
            //
            return 100 * (CalculateItemGrossProfit() / 180);
        }

        //calculates and returns total profit
        public float CalculateTotalProfit()
        {
            return CalculateItemGrossProfit() + (CalculateItemGrossProfit() - CalculateSnackDrinkTotalProfit());
        }


        public string TotalSnacksOrdered()
        {
            string summary = "----- Total Ordered Snacks ------\n";

            for (int snackIndex = 0; snackIndex < snacksAvailable.Count; snackIndex++)
            {
                summary += snacksAvailable[snackIndex] + "\tX\t" + SumItemsSold("snacks")[snackIndex] + "\n";
            }

            return summary;
        }

        public string DisplayTotalProfit()
        {
            return $"${CalculateTotalProfit()}";
        }

        // returns a string collating all the values stored in the private variables
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
