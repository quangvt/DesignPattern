/*
Definition
Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.

Frequency of use:High

Participants

    The classes and objects participating in this pattern are:

1. Subject  (Stock)
    knows its observers. Any number of Observer objects may observe a subject
    provides an interface for attaching and detaching Observer objects.
2. ConcreteSubject  (IBM)
    stores state of interest to ConcreteObserver
    sends a notification to its observers when its state changes
3. Observer  (IInvestor)
    defines an updating interface for objects that should be notified of changes in a subject.
4. ConcreteObserver  (Investor)
    maintains a reference to a ConcreteSubject object
    stores state that should stay consistent with the subject's
    implements the Observer updating interface to keep its state consistent with the subject's
    
Structural code in C#

This structural code demonstrates the Observer pattern in which registered objects are notified of and updated with a state change.
*/

namespace Behavioral.Observer
{
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
          // Configure Observer pattern
          ConcreteSubject s = new ConcreteSubject();

          s.Attach(new ConcreteObserver(s, "X"));
          s.Attach(new ConcreteObserver(s, "Y"));
          s.Attach(new ConcreteObserver(s, "Z"));

          // Change subject and notify observers
          s.SubjectState = "ABC";
          s.Notify();

          // Wait for user
          Console.ReadKey();
        }
    }
    
    abstract class Subject
    {
        list<Observer> _observers = new list<Observer>();
        
        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }
        
        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }
        
        public void Notify()
        {
            foreach(Observer o in _observer)
            {
                o.Update();
            }
        }
    }
    
    class ConcreteSubject : Subject
    {
        private string _subjectState;
        
        public string SubjectState
        {
            get { return _subjectState; }
            set { _subjectState = value; }
        }
    }
    
    abstract class Observer
    {
        public abstract void Update();
    }
    
    class ConcreteObserver : Observer
    {
        private string _name;
        private string _status;
        private ConcreteSubject _subject;
        
        public ConcreteObserver(ConcreteSubject subject, 
            string name)
        {
            _subject = subject;
            _name = name;
        }
        
        public override void Update()
        {
            _status = _subject.SubjectStatus;
            Console.WriteLine("Observer {0}'s new state is {1}",
        _name, _observerState);
        }
        
        public void Subject
        {
            get { return _subjec; }
            set { _subject = value; }
        }
    }

/*
Real-world code in C#

This real-world code demonstrates the Observer pattern in which registered investors are notified every time a stock changes value.
*/

namespace Behavioral.Observer.RealWorld
{
    abstract class Stock
    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();
        
        public Stock(string symbol, double price)
        {
            _symbol = symbol;
            _price = price;
        }
        
        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }
        
        public void Detach(IInvestor investor)
        {
            _investor.Remove(investor);
        }
        
        public double Price
        {
            get { return _price; }
            set {
                if(_price != value){
                    _price = value;
                    Notify();
                }
            }
        }
        
        private void Notify()
        {
            Foreach(IInvestor i in _investors)
            {
                i.Update(this);
            }
        }
    }
    
    class IBM : Stock
    {
        public IBM (string symbol, double price)
            : base (symbol, price)
        {
            
        }
    }
    
    interface IInvestor
    {
        void Update(Stock stock);
    }
    
    class Investor : IInvestor
    {
        private string _name;
        private Stock _stock;
        
        public Investor(string name)
        {
            _name = name;
        }
        
        public Update(Stock stock)
        {
            Console.WriteLine("Notified {0} of {1}'s " +
                "change to {2:C}", _name, stock.Symbol, stock.Price);
            _stock = stock;
        }
        
        public Stock Stock
        {
          get { return _stock; }
          set { _stock = value; }
        }
    }
}








}