namespace Creational.FactoryMethod
{
    public static void Main()
    {
        var factories = new Factory[] {
            new ToyotaFactory(),
            new HondaFactory()
        };
        
        foreach (var factory in factories) 
        {
            Car _car = factory.ProduceCar();
            Console.WriteLine("This is " + _car.GetType().Name);
        }
    }
    
    abstract class Car
    {
        
    }
    
    class ToyotaCar : Car
    {
    
    }
    
    class HondaCar : Car
    {
    
    }
    
    abstract class Factory
    {
        public abstract Car ProduceCar();
    }
    
    class ToyotaFactory : Factory
    {
        public override Car ProduceCar()
        {
            return new ToyotaCar();
        }
    }
    
    class HondaFactory : Factory
    {
        public override Car ProduceCar()
        {
            return new HondaCar();
        }
    }
}