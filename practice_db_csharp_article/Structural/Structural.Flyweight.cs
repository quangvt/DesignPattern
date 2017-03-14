/*
Definition
Use sharing to support large numbers of fine-grained objects efficiently.

Frequency of use:Low

Participants

    The classes and objects participating in this pattern are:

1. Flyweight   (Character)
    declares an interface through which flyweights can receive and act on extrinsic state.
2. ConcreteFlyweight   (CharacterA, CharacterB, ..., CharacterZ)
    implements the Flyweight interface and adds storage for intrinsic state, if any. A ConcreteFlyweight object must be sharable. Any state it stores must be intrinsic, that is, it must be independent of the ConcreteFlyweight object's context.
3. UnsharedConcreteFlyweight   ( not used )
    not all Flyweight subclasses need to be shared. The Flyweight interface enables sharing, but it doesn't enforce it. It is common for UnsharedConcreteFlyweight objects to have ConcreteFlyweight objects as children at some level in the flyweight object structure (as the Row and Column classes have).
4. FlyweightFactory   (CharacterFactory)
    creates and manages flyweight objects
    ensures that flyweight are shared properly. When a client requests flyweight, the FlyweightFactory objects assets an existing instance or creates one, if none exists.
5. Client   (FlyweightApp)
    maintains a reference to flyweight(s).
    computes or stores the extrinsic state of flyweight(s).
    
Structural code in C#

This structural code demonstrates the Flyweight pattern in which a relatively small number of objects is shared many times by different clients.
*/

using System;
using System.Collections;
 
namespace Structural.Flyweight
{
  /// <summary>
  /// MainApp startup class for Structural 
  /// Flyweight Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Arbitrary extrinsic state
      int extrinsicstate = 22;
 
      FlyweightFactory factory = new FlyweightFactory();
 
      // Work with different flyweight instances
      Flyweight fx = factory.GetFlyweight("X");
      fx.Operation(--extrinsicstate);
 
      Flyweight fy = factory.GetFlyweight("Y");
      fy.Operation(--extrinsicstate);
 
      Flyweight fz = factory.GetFlyweight("Z");
      fz.Operation(--extrinsicstate);
 
      UnsharedConcreteFlyweight fu = new
        UnsharedConcreteFlyweight();
 
      fu.Operation(--extrinsicstate);
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'FlyweightFactory' class
  /// </summary>
  class FlyweightFactory
  {
    private Hashtable flyweights = new Hashtable();
 
    // Constructor
    public FlyweightFactory()
    {
      flyweights.Add("X", new ConcreteFlyweight());
      flyweights.Add("Y", new ConcreteFlyweight());
      flyweights.Add("Z", new ConcreteFlyweight());
    }
 
    public Flyweight GetFlyweight(string key)
    {
      return ((Flyweight)flyweights[key]);
    }
  }
 
  /// <summary>
  /// The 'Flyweight' abstract class
  /// </summary>
  abstract class Flyweight
  {
    public abstract void Operation(int extrinsicstate);
  }
 
  /// <summary>
  /// The 'ConcreteFlyweight' class
  /// </summary>
  class ConcreteFlyweight : Flyweight
  {
    public override void Operation(int extrinsicstate)
    {
      Console.WriteLine("ConcreteFlyweight: " + extrinsicstate);
    }
  }
 
  /// <summary>
  /// The 'UnsharedConcreteFlyweight' class
  /// </summary>
  class UnsharedConcreteFlyweight : Flyweight
  {
    public override void Operation(int extrinsicstate)
    {
      Console.WriteLine("UnsharedConcreteFlyweight: " +
        extrinsicstate);
    }
  }
}

/*
Real-world code in C#

This real-world code demonstrates the Flyweight pattern in which a relatively small number of Character objects is shared many times by a document that has potentially many characters.
*/

using System;
using System.Collections.Generic;
 
namespace Structural.Flyweight.RealWorld
{
  /// <summary>
  /// MainApp startup class for Real-World 
  /// Flyweight Design Pattern.
  /// </summary>
  class MainApp
  {
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    static void Main()
    {
      // Build a document with text
      string document = "AAZZBBZB";
      char[] chars = document.ToCharArray();
 
      CharacterFactory factory = new CharacterFactory();
 
      // extrinsic state
      int pointSize = 10;
 
      // For each character use a flyweight object
      foreach (char c in chars)
      {
        pointSize++;
        Character character = factory.GetCharacter(c);
        character.Display(pointSize);
      }
 
      // Wait for user
      Console.ReadKey();
    }
  }
 
  /// <summary>
  /// The 'FlyweightFactory' class
  /// </summary>
  class CharacterFactory
  {
    private Dictionary<char, Character> _characters =
      new Dictionary<char, Character>();
 
    public Character GetCharacter(char key)
    {
      // Uses "lazy initialization"
      Character character = null;
      if (_characters.ContainsKey(key))
      {
        character = _characters[key];
      }
      else
      {
        switch (key)
        {
          case 'A': character = new CharacterA(); break;
          case 'B': character = new CharacterB(); break;
          //...
          case 'Z': character = new CharacterZ(); break;
        }
        _characters.Add(key, character);
      }
      return character;
    }
  }
 
  /// <summary>
  /// The 'Flyweight' abstract class
  /// </summary>
  abstract class Character
  {
    protected char symbol;
    protected int width;
    protected int height;
    protected int ascent;
    protected int descent;
    protected int pointSize;
 
    public abstract void Display(int pointSize);
  }
 
  /// <summary>
  /// A 'ConcreteFlyweight' class
  /// </summary>
  class CharacterA : Character
  {
    // Constructor
    public CharacterA()
    {
      this.symbol = 'A';
      this.height = 100;
      this.width = 120;
      this.ascent = 70;
      this.descent = 0;
    }
 
    public override void Display(int pointSize)
    {
      this.pointSize = pointSize;
      Console.WriteLine(this.symbol +
        " (pointsize " + this.pointSize + ")");
    }
  }
 
  /// <summary>
  /// A 'ConcreteFlyweight' class
  /// </summary>
  class CharacterB : Character
  {
    // Constructor
    public CharacterB()
    {
      this.symbol = 'B';
      this.height = 100;
      this.width = 140;
      this.ascent = 72;
      this.descent = 0;
    }
 
    public override void Display(int pointSize)
    {
      this.pointSize = pointSize;
      Console.WriteLine(this.symbol +
        " (pointsize " + this.pointSize + ")");
    }
 
  }
 
  // ... C, D, E, etc.
 
  /// <summary>
  /// A 'ConcreteFlyweight' class
  /// </summary>
  class CharacterZ : Character
  {
    // Constructor
    public CharacterZ()
    {
      this.symbol = 'Z';
      this.height = 100;
      this.width = 100;
      this.ascent = 68;
      this.descent = 0;
    }
 
    public override void Display(int pointSize)
    {
      this.pointSize = pointSize;
      Console.WriteLine(this.symbol +
        " (pointsize " + this.pointSize + ")");
    }
  }
}