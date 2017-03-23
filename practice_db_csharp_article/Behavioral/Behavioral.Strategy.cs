/*
Definition
Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets the algorithm vary independently from clients that use it.

Frequency of use:Medium high

Participants

    The classes and objects participating in this pattern are:

1. Strategy  (SortStrategy)
    declares an interface common to all supported algorithms. Context uses this interface to call the algorithm defined by a ConcreteStrategy
2. ConcreteStrategy  (QuickSort, ShellSort, MergeSort)
    implements the algorithm using the Strategy interface
3. Context  (SortedList)
    is configured with a ConcreteStrategy object
    maintains a reference to a Strategy object
    may define an interface that lets Strategy access its data.

Structural code in C#

This structural code demonstrates the Strategy pattern which encapsulates functionality in the form of an object. This allows clients to dynamically change algorithmic strategies.


*/

namespace Behavioral.Strategy
{
    class MainApp
    {
        static void Main()
        {
            Context context;
            
            context = new Context(new ConcreteStrategyA());
            context.ContextInterface();
            
            context = new Context(new ConcreteStrategyB());
            context.ContextInterface();
            
            context = new Context(new ConcreteStrategyC());
            context.ContextInterface();
        }
    }
    
    abstract class Strategy
    {
        public abstract void AlgorithmInterface();
    }
    
    class ConcreteStrategyA : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("Called ConcreteStrategyA.AlgorithmInterface()");
        }
    }
    
    class ConcreteStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("Called ConcreteStrategyB.AlgorithmInterface()");
        }
    }
    
    class ConcreteStrategyC : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("Called ConcreteStrategyC.AlgorithmInterface()");
        }
    }
    
    class Context
    {
        private Strategy _strategy;
        
        public Context(Strategy strategy)
        {
            this._strategy = strategy;
        }
        
        public void ContextInterface()
        {
            _strategy.AlgorithmInterface();
        }
    }
}

/*
Real-world code in C#

This real-world code demonstrates the Strategy pattern which encapsulates sorting algorithms in the form of sorting objects. This allows clients to dynamically change sorting strategies including Quicksort, Shellsort, and Mergesort.
*/

namespace Behavioral.Strategy.RealWorld
{
    class MainApp
    {
        static void Main()
        {
            SortedList studentRecords = new SortedList();
            
            studentRecords.Add("Samual");
            studentRecords.Add("Jimmy");
            studentRecords.Add("Sandra");
            studentRecords.Add("Vivek");
            studentRecords.Add("Anna");
            
            studentRecords.SetSortStrategy(new QuickSort());
            studentRecords.Sort();
            
            studentRecords.SetSortStrategy(new ShellSort());
            studentRecords.Sort();
            
            studentRecords.SetSortStrategy(new MergeSort());
            studentRecords.Sort();
        }
    }

    abstract class SortStrategy
    {
        public abstract void Sort(List<string> list);
    }
    
    class QuickSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            list.Sort(); // Default sort is QuickSort
            Console.WriteLine("QuickSorted list");
        }
    }
    
    class ShellSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            //list.ShellSort(); not-implemented
            Console.WriteLine("ShellSorted list");
        }
    }
    
    class MergeSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            //list.MergeSort(); not-implement
            Console.WriteLine("MergeSorted list");
        }
    }
    
    class SortedList
    {
        private List<string> _list = new List<string>();
        private SortStrategy _sortstrategy;
        
        public void SetSortStrategy(SortStrategy sortstrategy)
        {
            this._sortstrategy = sortstrategy;
        }
        
        public void Add(string name)
        {
            _list.Add(name);
        }
        
        public void Sort()
        {
            _sortstrategy.Sort(_list);
            
            foreach(string name in _list)
            {
                Console.WriteLine(" " + name);
            }
            
            Console.WriteLine();
        }
    }
}