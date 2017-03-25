/*
Template Method

Definition

Define the skeleton of an algorithm in an operation, deferring some steps to subclasses. Template Method lets subclasses redefine certain steps of an algorithm without changing the algorithm's structure.

Frequency of use: Medium

Participants

The classes and objects participating in this pattern are:
1. AbstractClass (DataObject)
- Define abstract primitive operations that concrete subclasses define to implement step of an algorithm
- Implements a template method defining the skeleton of an algorithm. The template method calls primitive operations as well as operations defined in AbstractClass or those of other objects.

2. ConcreteClass (CustomerDataObject)
- Implements the primitive operations ot carry out subclass-specific steps of the algorithm.

Structure Code in C#

This structural code demonstrates the Template method which provides a skeleton calling sequence of methods. One or more steps can be deferred to subclasses which implement these steps without changing overall calling sequence.
*/

using System

namespace Behavioral.Template
{
    class MainApp
    {
        static void Main()
        {
            AbstratClass aA = new ConcreteClassA();
            aA.TemplateMethod();
            
            AbstractClass aB = new ConcreteClassB();
            aB.TemplateMethod();
        }
    }
    
    abstract class AbstractClass
    {
        public abstract void PrimitiveOperation1();
        public abstract void PrimitiveOperation2();
        
        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
            Console.WriteLine("");
        }
    }
    
    class ConcreteClassA : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("ConcreteClassA.PrimitiveOperation1()");
        }
        
        public override void PrimitiveOperation2()
        {
            Console.WriteLine("ConcreteClassA.PrimitiveOperation2()");
        }
    }
    
    class ConcreteClassB : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("ConcreteClassB.PrimitiveOperation1()");
        }
        public override void PrimitiveOperation2()
        {
            Console.WriteLine("ConcreteClassB.PrimitiveOperation2()");
        }
    }
}

/*
Real-world code in C#
This real-world code demonstrates a Template method named Run() which provides a skeleton calling sequence of methods. Implementation of these steps are deferred to the CustomerDataObject subclass which implements the Connect, Select, Process, and Disconnect methods.

*/

using System;
using System.Data;
using System.Data.OleDb;

namespace Behavioral.Template.RealWorld
{
    class MainApp
    {
        DataAccessObject daoCategories = new Categories();
        daoCategories.Run();
        
        DataAccessObject daoProducts = new Products();
        daoProducts.Run();
    }
    abstract class DataAccessObject
    {
        protected string connectionString;
        protected DataSet dataSet;
        
        public virtual void Connect()
        {
            connectString = 
                "provider=Microsoft.JET.OLEDB.4.0; " +
                "data source=..\\..\\..\\db1.mdb";
        }
        
        public abstract void Select();
        public abstract void Process();
        
        public virtual void Disconnect()
        {
            connectionString = "";
        }
        
        public void Run()
        {
            Connect();
            Select();
            Process();
            Disconnect();
        }
    }
    
    class Categories : DataAccessObject
    {
        public override void Select()
        {
            string sql = "Select CategoryName From Categories";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(
            sql, connectionString);
            
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Categories");
        }
        
        public override void Process()
        {
            Console.WriteLine("Categories ----- ");
            
            DataTable dataTable = dataSet.Tables["Categories"];
            foreach(DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["CategoryName"]);
            }
            Console.WriteLine();
        }    
    }
    
    class Products : DataAccessObject
    {
        public override void Select()
        {
            string sql = "SELECT ProductName FROM Products";
            OleDbDataAdapter.Fill(dataSet, "Products");
        }
        
        public override void Process()
        {
            Console.WriteLine("Products -----");
            DataTable dataTable = dataSet.Tables["Products"];
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["ProductName"]);
            }
            Console.WriteLine();
        }
    }
}