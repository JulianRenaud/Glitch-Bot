using System;

namespace Glitch_Bot.External_Classes
{
    internal class CardBuilder
    {
        public int[] cardNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public string[] cardSuits = { "Clubs", "Spades", "Diamonds", "Hearts" };

        public int UserNumber { get; internal set; }
        public int BotNumber { get; internal set; }
        public string UserCard { get; internal set; }
        public string BotCard { get; internal set; }

        public CardBuilder()
        {
            var Random = new Random();
            int UserNumbers = Random.Next(0, this.cardNumbers.Length - 1);
            int BotNumbers = Random.Next(0, this.cardNumbers.Length - 1);
            int UserSuit = Random.Next(0, this.cardSuits.Length - 1);
            int BotSuit = Random.Next(0, this.cardSuits.Length - 1);

            this.UserNumber = this.cardNumbers[UserNumbers];
            this.BotNumber = this.cardNumbers[BotNumbers];
            if (UserNumbers == 11)
            {
                this.UserCard = "Jack of " + this.cardSuits[UserSuit];
            }
            else if (UserNumbers == 12)
            {
                this.UserCard = "Queen of " + this.cardSuits[UserSuit];

            }
            else if (UserNumbers == 13)
            {
                this.UserCard = "King of " + this.cardSuits[UserSuit];
            }
            else
            {
                this.UserCard = this.cardNumbers[UserNumbers] + " of " + this.cardSuits[UserSuit];
            }
            if (BotNumber == 11) 
            {
                this.BotCard = "Jack of " + this.cardSuits[BotSuit];
            }
            else if (BotNumber == 12)
            {
                this.BotCard = "Queen of " + this.cardSuits[BotSuit];
            }
            else if (BotNumber == 13)
            {
                this.BotCard = "King of " + this.cardSuits[BotSuit];
            }
            else
            {
                this.BotCard = this.cardNumbers[BotNumbers] + " of " + this.cardSuits[BotSuit];
            }
            


        }

    }
}
