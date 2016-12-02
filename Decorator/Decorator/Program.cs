using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramCode
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Card
    {
        private int ID, PIN, triesAvailible;
        private double balance;
        private bool active;

        public Card(int ID)
        {
            balance = 0.0;
            this.ID = ID;
            PIN = 0000;
        }
        public void SetPIN(int PIN)
        {
            this.PIN = PIN;
        }
        public bool CheckPIN(int PIN)
        {
            return (this.PIN == PIN);
        }
        public void Block()
        {
            active = false;
        }
        public int GetID()
        {
            return ID;
        }
        public void SetBalance(double balance)
        {
            this.balance = balance;
        }
        public double GetBalance()
        {
            return balance;
        }

    }

    class Account
    {
        private string holderName;
        private int ID, cardAmount;
        private bool active;
        private Card[] cards;

        public void BlockCard(int ID)
        {
            foreach (Card card in cards)
            {
                if (card.GetID() == ID)
                {
                    card.Block();
                    break;
                }
            }
        }
        public Card[] GetCards()
        {
            Card[] returnCards = new Card[cardAmount];
            int i = 0;
            foreach (Card card in cards)
            {
                returnCards[i] = card;
                i++;
            }
            return returnCards;
        }
        public int GetID()
        {
            return ID;
        }
    }

    class AccountManager
    {
        private Account[] accounts;
        public void BlockAccount(int ID)
        {
            foreach (Account account in accounts)
            {
                if (account.GetID() == ID)
                {
                    foreach (Card card in account.GetCards())
                    {
                        card.Block();
                    }
                    break;
                }
            }
        }
    }

    abstract class ATM
    {
        private string name;
        protected Card card;
        private int PIN;

        abstract public void InsertCard(Card card);
        abstract public void RemoveCard();
        abstract public void Withdraw(double value);
        abstract public void Replenish(double value);
        abstract public bool CheckPIN(int PIN);
        abstract public Card GetCard();
    }

    class GeneralATM : ATM
    {
        public override void InsertCard(Card card)
        {
            this.card = card;
        }
        public override void RemoveCard()
        {
            card = null;
        }
        public override bool CheckPIN(int PIN)
        {
            return card.CheckPIN(PIN);
        }
        public override void Withdraw(double value)
        {
            card.SetBalance(card.GetBalance() - value);
        }
        public override void Replenish(double value)
        {
            card.SetBalance(card.GetBalance() + value);
        }
        public override Card GetCard()
        {
            return card;
        }
    }

    abstract class ATMDecorator : ATM
    {
        protected ATM ATMImage;

        public void SetATM(ATM ATMImage)
        {
            this.ATMImage = ATMImage;
            card = ATMImage.GetCard();
        }
        public override void InsertCard(Card card)
        {
            ATMImage.InsertCard(card);
        }
        public override void RemoveCard()
        {
            ATMImage.RemoveCard();
        }
        public override void Withdraw(double value)
        { }
        public override void Replenish(double value)
        { }
        public override Card GetCard()
        {
            return card;
        }
        public override bool CheckPIN(int PIN)
        {
            return ATMImage.CheckPIN(PIN);
        }
    }

    class NativeATM : ATMDecorator
    {
        public override void Withdraw(double value)
        {
            GetCard().SetBalance(GetCard().GetBalance() - value);
        }
        public override void Replenish(double value)
        {
            GetCard().SetBalance(GetCard().GetBalance() + value);
        }
    }

    class OtherATM : ATMDecorator
    {
        private double fine = 5;
        public override void Withdraw(double value)
        {
            GetCard().SetBalance(GetCard().GetBalance() - value - value * fine * 0.01);
        }

        public override void Replenish(double value)
        {
            GetCard().SetBalance(GetCard().GetBalance() + value - value * fine * 0.01);
        }
    }
}

namespace SequencesDiagramCode
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountManager AM = new AccountManager();

            foreach (Card card in AM.account.GetCards())
            {
                AM.account.ID = card.GetID();
                card.Block(AM.account.ID);
            }
        }
    }
    class AccountManager
    {
        public Account account;

    }

    class Account
    {
        public int ID;
        private Card[] cards = new Card[0];
        public Card[] GetCards()
        {
            return cards;
        }
    }

    class Card
    {
        private int ID;
        public int GetID()
        {
            return ID;
        }
        public void Block(int ID)
        {

        }
    }
}