/*
Definition:

Decouple an abstraction from its implementation so that the two can vary independently.

Frequency of use:Medium

Participants:

    The classes and objects participating in this pattern are:

1. Abstraction   (BusinessObject)
    defines the abstraction's interface.
    maintains a reference to an object of type Implementor.
2. RefinedAbstraction   (CustomersBusinessObject)
    extends the interface defined by Abstraction.
3. Implementor   (DataObject)
    defines the interface for implementation classes. This interface doesn't have to correspond exactly to Abstraction's interface; in fact the two interfaces can be quite different. Typically the Implementation interface provides only primitive operations, and Abstraction defines higher-level operations based on these primitives.
4. ConcreteImplementor   (CustomersDataObject)
    implements the Implementor interface and defines its concrete implementation.

*/

using System;
 
namespace Structural.Bridge
{
  /// <summary>
  /// MainApp startup class for Structural
  /// Bridge Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      Abstraction ab = new RefinedAbstraction();
 
      // Set implementation and call
      ab.Implementor = new ConcreteImplementorA();
      ab.Operation();
 
      // Change implemention and call
      ab.Implementor = new ConcreteImplementorB();
      ab.Operation();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Abstraction' class
  /// </summary>
  class Abstraction
  {
    protected Implementor implementor;
 
    // Property
    public Implementor Implementor
    {
      set { implementor = value; }
    }
 
    public virtual void Operation()
    {
      implementor.Operation();
    }
  }
 
  /// <summary>
  /// The 'Implementor' abstract class
  /// </summary>
  abstract class Implementor
  {
    public abstract void Operation();
  }
 
  /// <summary>
  /// The 'RefinedAbstraction' class
  /// </summary>
  class RefinedAbstraction : Abstraction
  {
    public override void Operation()
    {
      implementor.Operation();
    }
  }
 
  /// <summary>
  /// The 'ConcreteImplementorA' class
  /// </summary>
  class ConcreteImplementorA : Implementor
  {
    public override void Operation()
    {
      Console.WriteLine("ConcreteImplementorA Operation");
    }
  }
 
  /// <summary>
  /// The 'ConcreteImplementorB' class
  /// </summary>
  class ConcreteImplementorB : Implementor
  {
    public override void Operation()
    {
      Console.WriteLine("ConcreteImplementorB Operation");
    }
  }
}

/*
Real-world code in C#

This real-world code demonstrates the Bridge pattern in which a BusinessObject abstraction is decoupled from the implementation in DataObject. The DataObject implementations can evolve dynamically without changing any clients.
*/

using System;
using System.Collections.Generic;
 
namespace Structural.Bridge.RealWorld
{
  /// <summary>
  /// MainApp startup class for Real-World 
  /// Bridge Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Create RefinedAbstraction
      Customers customers = new Customers("Chicago");
 
      // Set ConcreteImplementor
      customers.Data = new CustomersData();
 
      // Exercise the bridge
      customers.Show();
      customers.Next();
      customers.Show();
      customers.Next();
      customers.Show();
      customers.Add("Henry Velasquez");
 
      customers.ShowAll();
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'Abstraction' class
  /// </summary>
  class CustomersBase
  {
    private DataObject _dataObject;
    protected string group;
 
    public CustomersBase(string group)
    {
      this.group = group;
    }
 
    // Property
    public DataObject Data
    {
      set { _dataObject = value; }
      get { return _dataObject; }
    }
 
    public virtual void Next()
    {
      _dataObject.NextRecord();
    }
 
    public virtual void Prior()
    {
      _dataObject.PriorRecord();
    }
 
    public virtual void Add(string customer)
    {
      _dataObject.AddRecord(customer);
    }
 
    public virtual void Delete(string customer)
    {
      _dataObject.DeleteRecord(customer);
    }
 
    public virtual void Show()
    {
      _dataObject.ShowRecord();
    }
 
    public virtual void ShowAll()
    {
      Console.WriteLine("Customer Group: " + group);
      _dataObject.ShowAllRecords();
    }
  }
 
  /// <summary>
  /// The 'RefinedAbstraction' class
  /// </summary>
  class Customers : CustomersBase
  {
    // Constructor
    public Customers(string group)
      : base(group)
    {
    }
 
    public override void ShowAll()
    {
      // Add separator lines
      Console.WriteLine();
      Console.WriteLine("------------------------");
      base.ShowAll();
      Console.WriteLine("------------------------");
    }
  }
 
  /// <summary>
  /// The 'Implementor' abstract class
  /// </summary>
  abstract class DataObject
  {
    public abstract void NextRecord();
    public abstract void PriorRecord();
    public abstract void AddRecord(string name);
    public abstract void DeleteRecord(string name);
    public abstract void ShowRecord();
    public abstract void ShowAllRecords();
  }
 
  /// <summary>
  /// The 'ConcreteImplementor' class
  /// </summary>
  class CustomersData : DataObject
  {
    private List<string> _customers = new List<string>();
    private int _current = 0;
 
    public CustomersData()
    {
      // Loaded from a database 
      _customers.Add("Jim Jones");
      _customers.Add("Samual Jackson");
      _customers.Add("Allen Good");
      _customers.Add("Ann Stills");
      _customers.Add("Lisa Giolani");
    }
 
    public override void NextRecord()
    {
      if (_current <= _customers.Count - 1)
      {
        _current++;
      }
    }
 
    public override void PriorRecord()
    {
      if (_current > 0)
      {
        _current--;
      }
    }
 
    public override void AddRecord(string customer)
    {
      _customers.Add(customer);
    }
 
    public override void DeleteRecord(string customer)
    {
      _customers.Remove(customer);
    }
 
    public override void ShowRecord()
    {
      Console.WriteLine(_customers[_current]);
    }
 
    public override void ShowAllRecords()
    {
      foreach (string customer in _customers)
      {
        Console.WriteLine(" " + customer);
      }
    }
  }
}