/*
Definition
Provide a surrogate or placeholder for another object to control access to it.

Frequency of use:Medium high

Participants

    The classes and objects participating in this pattern are:

1. Proxy   (MathProxy)
    maintains a reference that lets the proxy access the real subject. Proxy may refer to a Subject if the RealSubject and Subject interfaces are the same.
    provides an interface identical to Subject's so that a proxy can be substituted for for the real subject.
    controls access to the real subject and may be responsible for creating and deleting it.
    other responsibilites depend on the kind of proxy:
    remote proxies are responsible for encoding a request and its arguments and for sending the encoded request to the real subject in a different address space.
    virtual proxies may cache additional information about the real subject so that they can postpone accessing it. For example, the ImageProxy from the Motivation caches the real images's extent.
    protection proxies check that the caller has the access permissions required to perform a request.
2. Subject   (IMath)
    defines the common interface for RealSubject and Proxy so that a Proxy can be used anywhere a RealSubject is expected.
3. RealSubject   (Math)
    defines the real object that the proxy represents.

Structural code in C#

This structural code demonstrates the Proxy pattern which provides a representative object (proxy) that controls access to another similar object.

*/


namespace Structural.Proxy 
{
    public static void Main()
    {
        Proxy proxy = new Proxy();
        
        proxy.Request();
    }
    
    abstract class Subject
    {
        public abstract void Request();
    }
    
    class RealSubject : Subject
    {
        public override void Request()
        {
            Console.WriteLine("This is a request from: " + this.GetType().Name);
        }
    }
    
    class Proxy : Subject
    {
        private RealSubject _realSubject;
        
        public override void Request()
        {
            // Using "lazy initialization
            if(_realSubject is null)
            {
                _realSubject = new RealSubject();
            }
            _realSubject.Request();
        }
    }
}


/*
Real-world code in C#

This real-world code demonstrates the Proxy pattern for a Math object represented by a MathProxy object.
*/

namespace Structural.Proxy.RealLife
{
    class MainApp
    {
        static void Main()
        {
          // Create math proxy
          MathProxy proxy = new MathProxy();

          // Do the math
          Console.WriteLine("4 + 2 = " + proxy.Add(4, 2));
          Console.WriteLine("4 - 2 = " + proxy.Sub(4, 2));
          Console.WriteLine("4 * 2 = " + proxy.Mul(4, 2));
          Console.WriteLine("4 / 2 = " + proxy.Div(4, 2));
        }
    }
    
    interface IMath
    {
        double Add(double a, double b);
        double Sub(double a, double b);
        double Mul(double a, double b);
        double Div(double a, double b);
    }
    
    class Math : IMath
    {
        public double Add(double a, double b) { return a + b; }
        public double Sub(double a, double b) { return a - b; }
        public double Mul(double a, double b) { return a * b; }
        public double Div(double a, double b) { return a / b; }
    }
    
    class MathProxy : Math
    {
        private Math _math = new Math();
        
        public double Add(double a, double b) { _math.Add(a, b); }
        public double Sub(double a, double b) { _math.Sub(a, b); }
        public double Mul(double a, double b) { _math.Mul(a, b); }
        public double Div(double a, double b) { _math.Div(a, b); }
    }
}




