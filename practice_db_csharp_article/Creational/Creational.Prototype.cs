/* Specify the kind of objects to create using a prototypical instance, and create new objects by copying this prototype. */
/* Frequency: 3/5 */

namespace Creational.Prototype 
{
    public static void Main()
    {
        ConcretePrototype1 p1 = new ConcretePrototype1("I");
        ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
        Console.WriteLine("Cloned {0}", c1.GetType().Name);
        
        ConcretePrototype1 p2 = new ConcretePrototype1("K");
        ConcretePrototype1 c2 = (ConcretePrototype2)p2.Clone();
        Console.WriteLine("Cloned {0}", c2.GetType().Name);
    }
    
    abstract class Prototype
    {
        private string _id;
        
        public Prototype(string id)
        {
            _id = id;
        }
        
        public string Id
        {
            return _id;
        }
        
        public abstract Prototype Clone();
    }
    
    class ConcretePrototype1 : Prototype
    {
        public ConcretePrototype1(string id) : base(id)
        {}
        
        public override Prototype Clone() 
        {
            return (Prototype)this.CloneMemberwise();
        }
    }
    
    class ConcretePrototype2 : Prototype
    {
        public ConcretePrototype2(string id) : base(id)
        {}
        
        public override Prototype Clone() 
        {
            return (Prototype)this.CloneMemberwise();
        }
    }
}