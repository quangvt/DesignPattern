/*
Definition
Provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation.

Frequency of use:High

Participants

    The classes and objects participating in this pattern are:

1. Iterator  (AbstractIterator)
    defines an interface for accessing and traversing elements.
2. ConcreteIterator  (Iterator)
    implements the Iterator interface.
    keeps track of the current position in   the traversal of the aggregate.
3. Aggregate  (AbstractCollection)
    defines an interface for creating an  Iterator object
4. ConcreteAggregate  (Collection)
    implements the Iterator creation interface to return an instance of the proper ConcreteIterator
Structural code in C#

This structural code demonstrates the Iterator pattern which provides for a way to traverse (iterate) over a collection of items without detailing the underlying structure of the collection.

*/

namespace Behavioral.Iterator
{
    public static void Main()
    {
        var a = new ConcreteAggregate();
        a[0] = "0";
        a[1] = "1";
        a[2] = "2";
        a[3] = "3";
        
        // ConcreteAggregate items = a.CreateIterator();
        Iterator items = a.CreateIterator();
        
        object item = items.First();
        while (item != null)
        {
            Console.WriteLine(item);
            item = items.Next();
        }
    }
    
    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }
    
    class ConcreteAggregate : Aggregate
    {
        private _item = new ArrayList();
        
        public override Iterator CreateIterator()
        {
            return new CreateIterator(this);
        }
        
        public int Count
        {
            get { return _item.length; }
        }
        
        public this[index]
        {
            get { return _item[index]; }
            set { _item[index] = value; }
        }
    }
    
    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object Current();
    }
    
    class ConcreteIterator : Iterator
    {
        private ConcreteAggregate _concreteAggregate;
        private int _index = 0;
        
        public ConcreteIterator(ConcreteAggregate concreteAggregate)
        {
            this._concreteAggregate = concreteAggregate;
        }
        
        public override object First()
        {
            return _concreteAggregate[0];
        }
        
        public override object Next()
        {
            object ret = null;
            if (_index < concreteAggregate.length - 1)
            {
                return _concreteAggregate[++_index];
            }
        }
        
        public override bool IsDone()
        {
            return (_index >= concreteAggregate.length) ? true : false;
        }
        
        public override bool Current()
        {
            return _concreteAggregate[_index];
        }
    }
}

/*
Real-world code in C#

This real-world code demonstrates the Iterator pattern which is used to iterate over a collection of items and skip a specific number of items each iteration.
*/

namespace Behavioral.Iterator.RealWorld
{
    /// <summary>
    /// MainApp startup class for Real-World 
    /// Iterator Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Build a collection
            Collection collection = new Collection();
            collection[0] = new Item("Item 0");
            collection[1] = new Item("Item 1");
            collection[2] = new Item("Item 2");
            collection[3] = new Item("Item 3");
            collection[4] = new Item("Item 4");
            collection[5] = new Item("Item 5");
            collection[6] = new Item("Item 6");
            collection[7] = new Item("Item 7");
            collection[8] = new Item("Item 8");
 
            // Create iterator
            Iterator iterator = collection.CreateIterator();
 
            // Skip every other item
            iterator.Step = 2;
 
            Console.WriteLine("Iterating over collection:");
 
            for (Item item = iterator.First();
                !iterator.IsDone; item = iterator.Next())
            {
                Console.WriteLine(item.Name);
            }
 
            // Wait for user
            Console.ReadKey();
        }
    }
 
    /// <summary>
    /// A collection item
    /// </summary>
    class Item
    {
        private string _name;
 
        // Constructor
        public Item(string name)
        {
            this._name = name;
        }
 
        // Gets name
        public string Name
        {
            get { return _name; }
        }
    }
 
    /// <summary>
    /// The 'Aggregate' interface
    /// </summary>
    interface IAbstractCollection
    {
        Iterator CreateIterator();
    }
 
    /// <summary>
    /// The 'ConcreteAggregate' class
    /// </summary>
    class Collection : IAbstractCollection
    {
        private ArrayList _items = new ArrayList();
 
        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }
 
        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }
 
        // Indexer
        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Add(value); }
        }
    }
 
    /// <summary>
    /// The 'Iterator' interface
    /// </summary>
    interface IAbstractIterator
    {
        Item First();
        Item Next();
        bool IsDone { get; }
        Item CurrentItem { get; }
    }
 
    /// <summary>
    /// The 'ConcreteIterator' class
    /// </summary>
    class Iterator : IAbstractIterator
    {
        private Collection _collection;
        private int _current = 0;
        private int _step = 1;
 
        // Constructor
        public Iterator(Collection collection)
        {
            this._collection = collection;
        }
 
        // Gets first item
        public Item First()
        {
            _current = 0;
            return _collection[_current] as Item;
        }
 
        // Gets next item
        public Item Next()
        {
            _current += _step;
            if (!IsDone)
                return _collection[_current] as Item;
            else
                return null;
        }
 
        // Gets or sets stepsize
        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }
 
        // Gets current iterator item
        public Item CurrentItem
        {
            get { return _collection[_current] as Item; }
        }
 
        // Gets whether iteration is complete
        public bool IsDone
        {
            get { return _current >= _collection.Count; }
        }
    }
}