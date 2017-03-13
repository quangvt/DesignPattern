using System;

namespace Creational.AbstractFactory
{
    public static void Main()
    {
        // Abstract Factory #1
        AbstractFactory factory1 = new ConcreteFactory1();
        Client client1 = new Client(factory1);
        client1.Run();
        
        // Abstract Factory #2
        AbstractFactory factory2 = new ConcreteFactory2();
        Client client2 = new Client(factory2);
        client2.Run();
    }
    
    public abstract class AbstractFactory 
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }

    class ConcreteFactory1 : AbstractFactory
    {
        public override AbstractProductA CreateProductA(){
            return new ProductA1();
        }

        public override AbstractProductB CreateProductB(){
            return new ProductB1();
        }
    }

    class ConcreteFactory2 : AbstractFactory
    {
        public override AbstractProductA CreateProductA(){
            return new ProductA2();
        }

        public override AbstractProductB CreateProductB(){
            return new ProductB2();
        }
    }

    public abstract class AbstractProductA
    {
    }

    public abstract class AbstractProductB
    {
        public abstract void Interact(AbstractProductA a);
    }

    class ProductA1 : AbstractProductA
    {

    }

    class ProductB1 : AbstractProductB
    {
        public override void Interact(AbstractProductA a)
        {
            Console.WriteLine(this.GetType().Name + 
                             " interacts with " + a.GetType().Name);
        }
    }

    class ProductA2 : AbstractProductA
    {

    }

    class ProductB2 : AbstractProductB
    {
        public override void Interact(AbstractProductA a)
        {
            Console.WriteLine(this.GetType().Name + 
                             " interacts with " + a.GetType().Name);
        }
    }

    public class Client
    {
        private AbstractProductA _productA;
        private AbstractProductB _productB;
        public Client(AbstractFactory pFactory)
        {
            _productA = pFactory.CreateProductA();
            _productB = pFactory.CreateProductB();
        }

        public void Run()
        {
            _productB.Interact(_productA);
        }
    }
}