/*
Definition
Avoid coupling the sender of a request to its receiver by giving more than one object a chance to handle the request. Chain the receiving objects and pass the request along the chain until an object handles it.

Frequency of use:Medium low

Participants

    The classes and objects participating in this pattern are:

1. Handler   (Approver)
    defines an interface for handling the requests
    (optional) implements the successor link
2. ConcreteHandler   (Director, VicePresident, President)
    handles requests it is responsible for
    can access its successor
    if the ConcreteHandler can handle the request, it does so; otherwise it forwards the request to its successor
3. Client   (ChainApp)
    initiates the request to a ConcreteHandler object on the chain
*/

namespace Behavioral.Chain
{
    static void Main()
    {
        Handler h1 = new ConcreteHandler1();
        Handler h2 = new ConcreteHandler2();
        Handler h3 = new ConcreteHandler3();
        h1.SetSuccessor(h2);
        h2.SetSuccessor(h3);
    }
    
    abstract class Handler
    {
        protected Hanlder successor;
        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;    
        }
        public abstract void HandleRequest(int request);
    }
    
    class ConcreteHandler1 : Handler
    {
        public override void HandleRequest(int request)
        {
            if(request >= 0 && request < 10)
            {
                Console.WriteLine("{0} handled request {1}",
                    this.GetType().Name, request);
            }
            else if(success != null)
            {
                successor.HandleRequest(request);
            }
        }
    }
    
    class ConcreteHandler2 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
            {
                Console.WriteLine("{0} handled request {1}",
                    this.GetType().Name, request);
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }
    
    class ConcreteHandler3 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 20 && request < 30)
            {
                Console.WriteLine("{0} handled request {1}",
                    this.GetType().Name, request);
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }
}

/*
Real-world code in C#

This real-world code demonstrates the Chain of Responsibility pattern in which several linked managers and executives can respond to a purchase request or hand it off to a superior. Each position has can have its own set of rules which orders they can approve.

*/

namespace Behavioral.Chain.RealWorld
{
    static void Main()
    {
      // Setup Chain of Responsibility
      Approver larry = new Director();
      Approver sam = new VicePresident();
      Approver tammy = new President();
 
      larry.SetSuccessor(sam);
      sam.SetSuccessor(tammy);
 
      // Generate and process purchase requests
      Purchase p = new Purchase(2034, 350.00, "Assets");
      larry.ProcessRequest(p);
 
      p = new Purchase(2035, 32590.10, "Project X");
      larry.ProcessRequest(p);
 
      p = new Purchase(2036, 122100.00, "Project Y");
      larry.ProcessRequest(p);
 
      // Wait for user
      Console.ReadKey();
    }
    
    abstract class Approver
    {
        protected Approver successor;
        public void SetSuccessor(Approver successor)
        {
            this.successor = successor;
        }
        public abstract void ProcessRequest(Purchase purchase);
    }
    
    class Director : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if(purchase.Amount < 10000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                    this.GetType().Name, purchase.Number);
            } else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }
    
    class VicePresident : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 25000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }
  
    class President : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                this.GetType().Name, purchase.Number);
            }
            else
            {
                Console.WriteLine("Request# {0} requires an executive       meeting!", purchase.Number);
            }
        }
    }
    
    class Purchase
    {
        private int _number;
        private double _amount;
        private string _purpose;

        // Constructor
        public Purchase(int number, double amount, string purpose)
        {
            this._number = number;
            this._amount = amount;
            this._purpose = purpose;
        }

        // Gets or sets purchase number
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        // Gets or sets purchase amount
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // Gets or sets purchase purpose
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
    }
}