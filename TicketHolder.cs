using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieFundraiserV1
{
    class TicketHolder
    {
        //attributes or fields
        private string name;
        private int age;
        private int numberTickets;
        private bool credit;
        // stores the indexes of the snacks that have been ordered
        private List<int> snackOrder = new List<int>();
        private List<int> snackQuantity = new List<int>();
        // stores the indexes of the drinks that have been ordered
        private List<int> drinkOrder = new List<int>();
        private List<int> drinkQuantity = new List<int>();

        //constructor - cronstructs an object of this class
        public TicketHolder(string name, int age, int numberTickets)
        {
            this.name = name;
            this.age = age;
            this.numberTickets = numberTickets;
        }

        //returns the value in the private age variable
        public int GetAge()
        {
            return this.age;
        }

        //Sets the value of the private age variable
        public void SetAge(int newAge)
        {
            this.age = newAge;
        }

        public void SetCredit(bool newPaymentType)
        {
            credit = newPaymentType;
        }

        //add snacks and quantities to the snacksOrder list and snacksQuantities Lists
        public void AddSnacks(List<int> snacks, List<int> quantities)
        {
            snackOrder = snacks;
            snackQuantity = quantities;
        }

        //add drinks and quantities to the drinksOrder list and drinksQuantities Lists
        public void AddDrinks(List<int> drinks, List<int> quantities)
        {
            drinkOrder = drinks;
            drinkQuantity = quantities;
        }

        // returns string stating whether the ticket holder is paying cash or credit
        private string PaymentType()
        {
            string paymentType = "credit";
            if (credit == false)
            {
                paymentType = "cash";
            }
            return paymentType;
        }

        //returns the total cost of the purchased items
        public float CalculateTotalCost(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            float totalCost = 0.0f;

            //loop through snack order and calculate the cost for each snack
            for (int snackIndex = 0; snackIndex < snackOrder.Count; snackIndex++)
            {
                float snackPrice = sPrices[snackOrder[snackIndex]];

                //add the cost of each snack to totalCost
                totalCost += snackPrice * snackQuantity[snackIndex];
            }

            //loop through drink order and calculate the cost for each drink
            for (int drinkIndex = 0; drinkIndex < drinkOrder.Count; drinkIndex++)
            {
                float drinkPrice = dPrices[drinkOrder[drinkIndex]];

                //add the cost of each drink to totalCost
                totalCost += drinkPrice * drinkQuantity[drinkIndex];
            }

            totalCost += CalculateTicketCost(ticketPrice);

            return totalCost;
        }

        //calculate ticket cost
        private float CalculateTicketCost(float ticketPrice)
        {
            return numberTickets * ticketPrice;
        }

        private string SnackDrinkSummary(List<float> sPrices, List<float> dPrices, List <string> sAvailable, List<string> dAvailable)
        {
            string summary = "Snacks and Drinks:\n";

            //loop through snack order and quanitity, snack, total item cost to summary
            for (int snackIndex = 0; snackIndex < snackOrder.Count; snackIndex++)
            {
                summary += snackQuantity[snackIndex] + " x " + sAvailable[snackOrder[snackIndex]] + "\t$" + (snackQuantity[snackIndex] * sPrices[snackOrder[snackIndex]]) + "\n";
            }

            //loop through drink order and quanitity, drink, total item cost to summary
            for (int drinkIndex = 0; drinkIndex < drinkOrder.Count; drinkIndex++)
            {
                summary += drinkQuantity[drinkIndex] + " x " + sAvailable[drinkOrder[drinkIndex]] + "\t$" + (drinkQuantity[drinkIndex] * sPrices[drinkOrder[drinkIndex]]) + "\n";
            }

            return summary;
        }

        // checks if a surcharge is required
        private bool GetSurcharge()
        {
            return credit;
        }

        //return string displaying surcharge cost
        private string SurchargeSummary(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            string summary = "";

            if (credit)
            {
                summary += "Surcharge\t$" + CalculateSurcharge(sPrices, dPrices, ticketPrice);
            }

            return summary;
        }

        //returns surcharge amount
        private float CalculateSurcharge(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            // calculates surcharge
            float surcharge = CalculateTotalCost(sPrices, dPrices, ticketPrice) * 0.2f;

            return surcharge;
        }

        //calculate total amount to be paid
        private float CalculateTotalPayment(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {

            float totalPayment = CalculateTotalCost(sPrices, dPrices, ticketPrice);

            if (credit)
            {
                totalPayment += CalculateSurcharge(sPrices, dPrices, ticketPrice);
            }

            return totalPayment;
        }

        //return a summary of the total amount to be paid
        private string TotalPaymentSummary(List<float> sPrices, List<float> dPrices, float ticketPrice)
        {
            return "Total\t$" + CalculateTotalPayment(sPrices, dPrices, ticketPrice);
        }

        //returns a string diaplaying the reciept for the puchased items
        public string GenerateReciept()
        {


            return $"{PaymentType()}";
        }

        // returns a string collating all the values stored in the private variables
        public override string ToString()
        {
            return "";
        }
    }
}
