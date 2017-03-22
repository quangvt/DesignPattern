/*
Definition
Allow an object to alter its behavior when its internal state changes. The object will appear to change its class.

Frequency of use:Medium

Participants

    The classes and objects participating in this pattern are:

1. Context  (Account)
    defines the interface of interest to clients
    maintains an instance of a ConcreteState subclass that defines the current state.
2. State  (State)
    defines an interface for encapsulating the behavior associated with a particular state of the Context.
3. Concrete State  (RedState, SilverState, GoldState)
    each subclass implements a behavior associated with a state of Context

Structural code in C#

This structural code demonstrates the State pattern which allows an object to behave differently depending on its internal state. The difference in behavior is delegated to objects that represent this state.

*/

namespace Behavioral.State
{
    class MainApp
    {
        static void Main()
        {
            Context c = new Context(new ConcreteStateA());
            
            c.Request();
            c.Request();
            c.Request();
            c.Request();
        }
    }
    abstract class State
    {
        public abstract void Handle(Context context);
    }
    
    class ConcreteStateA : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateB();
        }
    }
    
    class ConcreteStateB : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateA();
        }
    }
    
    class Context
    {
        private State _state;
        
        public Context(State state)
        {
            this.State = state;
        }
        
        public State State
        {
            get { return _state; }
            set {
                _state = value;
                Console.WriteLine("State: " + 
                                 _state.GetType().Name);
            }
        }
        
        public void Request()
        {
            _state.Handle(this);
        }
    }
}

/* 
Real-world code in C#

This real-world code demonstrates the State pattern which allows an Account to behave differently depending on its balance. The difference in behavior is delegated to State objecs called RedState, SilverState and GoldState. These states represent overdrawn account, starter accounts, and accounts in good standing.
*/

namespace Behavioral.State.RealWorld
{
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Open a new account
            Account account = new Account("Jim Johnson");

            // Apply financial transactions
            account.Deposit(500.0);
            account.Deposit(300.0);
            account.Deposit(550.0);
            account.PayInterest();
            account.Withdraw(2000.00);
            account.Withdraw(1100.00);

            // Wait for user
            Console.ReadKey();
        }
    }

    abstract class State
    {
        protected Account account;
        protected double balance;
        
        protected double interest;
        protected double lowerLimit;
        protected double upperLimit;
        
        public Account Account
        {
            get { return account; }
            set { account = value; }
        }
        
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        
        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract void PayInterest();
    }
    
    class RedState : State
    {
        private double _serviceFee;
        
        public RedState(State state)
        {
            this.balance = state.Balance;
            this.account = state.Account;
            Initialize();
        }
        
        private void Initialize()
        {
            // Should come from a datasource
            interest = 0.0;
            lowerLimit = -100.0;
            upperLimit = 0.0;
            _serviceFee = 15.00;
        }
        
        public override void Deposit(double amount)
        {
            balance += amount;
            StateChangeCheck();
        }
        
        public override void Withdraw(double amount)
        {
            amount = amount - _serviceFee;
            Console.WriteLine("No funds available for withdraw!");
        }
        
        public override void PayInterest()
        {
            // No interest is paid
        }
        
        private void StateChangeCheck()
        {
            if (balance > upperLimit)
            {
                account.State = new SilverState(this);
            }
        }
    }
    
    class SilverState : State
    {
        // Overloaded constructors

        public SilverState(State state) :
          this(state.Balance, state.Account)
        {
        }

        public SilverState(double balance, Account account)
        {
          this.balance = balance;
          this.account = account;
          Initialize();
        }

        private void Initialize()
        {
          // Should come from a datasource
          interest = 0.0;
          lowerLimit = 0.0;
          upperLimit = 1000.0;
        }

        public override void Deposit(double amount)
        {
          balance += amount;
          StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
          balance -= amount;
          StateChangeCheck();
        }

        public override void PayInterest()
        {
          balance += interest * balance;
          StateChangeCheck();
        }

        private void StateChangeCheck()
        {
          if (balance < lowerLimit)
          {
            account.State = new RedState(this);
          }
          else if (balance > upperLimit)
          {
            account.State = new GoldState(this);
          }
        }
    }
    
    class GoldState : State
    {
        // Overloaded constructors
        public GoldState(State state)
          : this(state.Balance, state.Account)
        {
        }

        public GoldState(double balance, Account account)
        {
          this.balance = balance;
          this.account = account;
          Initialize();
        }

        private void Initialize()
        {
          // Should come from a database
          interest = 0.05;
          lowerLimit = 1000.0;
          upperLimit = 10000000.0;
        }

        public override void Deposit(double amount)
        {
          balance += amount;
          StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
          balance -= amount;
          StateChangeCheck();
        }

        public override void PayInterest()
        {
          balance += interest * balance;
          StateChangeCheck();
        }

        private void StateChangeCheck()
        {
          if (balance < 0.0)
          {
            account.State = new RedState(this);
          }
          else if (balance < lowerLimit)
          {
            account.State = new SilverState(this);
          }
        }
    }
    
    class Account
    {
        private State _state;
        private string _owner;
        
        public Account(string owner)
        {
            this._owner = owner;
            this._state = new SilverState(0.0, this);
        }
        
        public double Balance
        {
            get { return _state.Balance; }
        }
        
        public State State
        {
            get { return _state; }
            set { _state = value; }
        }
        
        public void Deposit(double amount)
        {
            _state.Deposit(amount);
            Console.WriteLine("Deposited {0:C} --- ", amount);
            Console.WriteLine(" Balance = {0:C}", this.Balance);
            Console.WriteLine(" Status = {0}",
                             this.State.GetType().Name);
            Console.WriteLine("");
        }
        
        public void Withdraw(double amount)
        {
            _state.Withdraw(amount);
            Console.WriteLine("Withdrew {0:C} --- ", amount);
            Console.WriteLine(" Balance = {0:C}", this.Balance);
            Console.WriteLine(" Status = {0}\n",
            this.State.GetType().Name);
        }
        
        public void PayInterest()
        {
            _state.PayInterest();
            Console.WriteLine("Interest Paid --- ");
            Console.WriteLine(" Balance = {0:C}", this.Balance);
            Console.WriteLine(" Status = {0}\n",
            this.State.GetType().Name);
        }
    }
  
    
}