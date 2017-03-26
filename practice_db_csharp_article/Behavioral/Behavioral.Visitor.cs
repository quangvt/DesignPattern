/*
Definition
Represent an operation to be performed on the elements of an object structure. Visitor lets you define a new operation without changing the classes of the elements on which it operates

Frequency of use: Low

Participants

The classes and objects participating in this pattern are:
1. Visitor (Visitor)
- declares a Visit operation for each class of ConcreteElement in the object structure. The operation's name and signature identifies the class that sends the Visit request to the visitor. That lets the visitor determine the concrete class of the element being visited. Then the visitor can access the elements directly through its particular interface.

2. ConcreteVisitor (IncomeVisitor, VacationVisitor)
- Implements each operation declared by Visitor. Each operation implements a fragment of the algorithm defined for the corresponding class or object in structure. ConcreteVisitor provides the context for the algorithm and stores its local state. This state often accumulates results during the traversal of the structure.

3. Element (Element)
- defines an Accept operation that takes a visitor as an argument.

4. ConcreteElement (Employee)
- Implements an Accept operation that akes a vistor as an argument.

5. ObjectStructure (Employees)
- can enumerate its elements
- may provide a high-level interface to allow the visitor to visit its elements.
- may either be a Composite (pattern) or a collection such as a list or a set.

Structural code in C#
This structural code demonstrates the Visitor pattern in which an object traverses an object structure and performs the same operation on each node in this structure. Differenct visitor objects define different operations

*/
using System;
using System.Collections.Generic;

namespace Behavioral.Visitor
{
    class MainApp
    {
        static void Main()
        {
            ObjectStructure o = new ObjectStructure();
            o.Attach(new ConcreteElementA());
            o.Attach(new ConcreteElementB());
            
            ConcreteVisitor1 v1 = new ConcreteVisitor1();
            ConcreteVisitor2 v2 = new ConcreteVisitor2();
            
            o.Accept(v1);
            o.Accept(v2);
        }
    }
    abstract class Visitor
    {
        public abstract void VisitorConcreteElementA(ConcreteElementA concreteElementA);
        public abstract void VisitorConcreteElementB(ConcreteElementB concreteElementB);
    }
    
    class ConcreteVisitor1 : Visitor
    {
        public override void VisitorConcreteElementA(ConcreteElementA concreteElementA)
        {
            Console.WriteLine("{0} visited by {1}", concreteElementA.GetType().Name, this.GetType().Name);
        }
        
        public override void VisitorConcreteElementA(ConcreteElementB concreteElementB)
        {
            Console.WriteLine("{0} visited by {1}}", concreteElementB.GetType().Name, this.GetType().Name);
        }
        
        abstract class Element
        {
            public abstract void Accept(Visitor visitor);
        }
        
        class ConcreteElementA : Element
        {
            public override void Accept(Visitor visitor)
            {
                visitor.VisitConcreteElementA(this);
            }
            
            public void OperationA()
            {
            
            }
        }
        
        class ConcreteElementB : Element
        {
            public override void Accept(Visitor visitor)
            {
                visitor.VisitConcreteElementB(this);
            }
            
            public void OperationB()
            {
            
            }
        }
        
        class ObjectStructure
        {
            private List<Element> _elements = new List<Element>();
            
            public void Attach(Element element)
            {
                _elements.Add(element);
            }
            
            public void Detach(Element element)
            {
                _elements.Remove(element);
            }
            
            public void Accept(Visitor visitor)
            {
                foreach (Element element in _elements)
                {
                    element.Accept(visitor);
                }
            }
        }
    }
}

/*
Real-world Code in C#

This real-world code demonstrates the Visitor pattern in which two objects traverse a list of Employees and performs the same operation on each Employee. The two visitor objects define different operations -- one adjusts vacation days and the other income
*/

using System;
using System.Collections.Generic;

namespace Behavioral.Visitor.RealWorld
{
    class MainApp
    {
        static void Main()
        {
            Employees e = new Employees();
            e.Attach(new Clerk());
            e.Attach(new Director());
            e.Attach(new President());
            
            e.Accept(new IncomeVisitor());
            e.Accept(new VacationVisitor());
        }
    }

    interface IVisitor
    {
        void Visit(Element element);
    }
    
    class IncomeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;
            
            // Provide 10% pay raise
            employee.Income *= 1.10;
            Console.WriteLine("{0} {1}'s new income: {2:C}", employee.GetType().Name, employee.Name, employee.Income);
        }
    }
    
    class VacationVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;
            
            // Provide 3 extra vacation days
            employee.VacationDay += 3;
            Console.WriteLine("{0} {1}'s new vacation days: {2}", employee.GetType().Name, employee.Name, employee.VacationDays);
        }
    }
    
    abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }
    
    class Employee : Element
    {
        private string _name;
        private double _income;
        private int _vacationDays;
        
        public Employee(string name, double income, int vacationDays)
        {
            this._name = name;
            this._income = income;
            this._vacationDays = vacationDays;
        }
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public double Income
        {
            get { return _income; }
            set { _income = value; }
        }
        
        public in VacationDays
        {
            get { return _vacationDays; }
            set { _vacationDays = value; }
        }
        
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    
    class Employees
    {
        private List<Employee> _employees = new List<Employee>();
        
        public void Attach(Employee employee)
        {
            _employees.Add(employee);
        }
        
        public void Detach(Employee employee)
        {
            _employee.Remove(employee);
        }
        
        public void Accept(IVisitor visitor)
        {
            foreach (Employee e in _employees)
            {
                e.Accept(visitor);
            }
            Console.WriteLine();
        }
    }
    
    class Clerk : Employee
    {
        public Clerk()
            : base("Hank", 25000.0, 14)
            {
                
            }
    }
    
    class Director : Employee
    {
        public Director()
            : base("Elly", 35000.0, 16)
            {
            
            }
    }
    
    class President : Employee
    {
        public President()
            : base("Dick", 45000.0, 21)
        {}
    }
}