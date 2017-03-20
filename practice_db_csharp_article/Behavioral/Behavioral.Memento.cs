/*
Definition
Without violating encapsulation, capture and externalize an object's internal state so that the object can be restored to this state later.

Frequency of use:Low

Participants

    The classes and objects participating in this pattern are:

1. Memento  (Memento)
    stores internal state of the Originator object. The memento may store as much or as little of the originator's internal state as necessary at its originator's discretion.
    protect against access by objects of other than the originator. Mementos have effectively two interfaces. Caretaker sees a narrow interface to the Memento -- it can only pass the memento to the other objects. Originator, in contrast, sees a wide interface, one that lets it access all the data necessary to restore itself to its previous state. Ideally, only the originator that produces the memento would be permitted to access the memento's internal state.
2. Originator  (SalesProspect)
    creates a memento containing a snapshot of its current internal state.
    uses the memento to restore its internal state
3. Caretaker  (Caretaker)
    is responsible for the memento's safekeeping
    never operates on or examines the contents of a memento.

Structural code in C#

This structural code demonstrates the Memento pattern which temporary saves and restores another object's internal state.
*/

namespace Behavioral.Memento
{
    class MainApp
    {
        public static void Main()
        {
            Originator o = new Originator();
            o.State = "On";
            
            Caretaker c = new Caretaker();
            c.Memento = o.CreateMemento();
            
            o.State = "Off";
            
            o.SetMemento(c.Memento):
            
            Console.ReadKey();
        }
    }
    class Originator
    {
        private string _state;
        
        public string State
        {
            get { return _state; }
            set {
                _state = value;
                Console.WriteLine("State = " + _state);
            }
        }
        
        public Memento CreateMemento()
        {
            return (new Memento(_state));
        }
        
        public void SetMemento(Memento memento)
        {
            Console.WriteLine("Restoring state ...");
            State = memento.State;
        }
    }
    
    class Memento
    {
        private string _state;
        
        public Memento(string state)
        {
            this._state = state;
        }
        
        public string State
        {
            get { return _state; }
        }
    }
    
    class Caretaker
    {
        private Memento _memento;
        
        public Memento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}

/*
Real-world code in C#

This real-world code demonstrates the Memento pattern which temporarily saves and then restores the SalesProspect's internal state
    */

namespace Behavioral.Memento.RealWorld
{
    class MainApp
    {
        static void Main()
        {
            SalesProspect s = new SalesProspect();
            s.Name = "Cristiano Ronando";
            s.Phone = "(111) 222-33333";
            s.Budget = 100000.0;
            
            // Store internal state
            ProspectMemory m = new ProspectMemory();
            m.Memento = s.SaveMemento();
            
            // Continue chaing originator
            s.Name = "Leo Messi";
            s.Phone = "(000) 222-55555";
            s.Budget = 200000.0;
            
            // Restore saved state
            s.RestoreMemento(m.Memento);
            
            
        }
    }
    class SalesProspect
    {
        private string _name;
        private string _phone;
        private double _budget;
        
        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                Console.WriteLine("Name: " + _name);
            }
        }
        
        public string Phone
        {
            get { return _phone; }
            set {
                _phone = value;
                Console.WriteLine("Name: " + _phone);
            }
        }
        
        public string Budget
        {
            get { return _budget; }
            set {
                _budget = value;
                Console.WriteLine("Name: " + _budget);
            }
        }
        
        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            return new Memento(_name, _phone, _budget);
        }
        
        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.Name = memento.Name;
            this.Phone = memento.Phone;
            this.Budget = memento.Budget;
        }
    }
    
    class Memento
    {
        private string _name;
        private string _phone;
        private double _budget;
        
        public Memento(string name, string phone, double budget)
        {
            this._name = name;
            this._phone = phone;
            this._budget = budget;
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        
        public double Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
    }
    
    class ProspectMemory
    {
        private Memento _memento;
        
        public Memento Memento
        {
            set { _memento = value; }
            get { return _memento; }
        }
    }
}
    
