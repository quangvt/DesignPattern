/*
Definition
Encapsulate a request as an object, thereby letting you parameterize clients with different requests, queue or log requests, and support undoable operations

Frequency of use: Medium high

UML class diagram

Participants

    The classes and objects participating in this pattern are:

1. Command  (Command)
    declares an interface for executing an operation
2. ConcreteCommand  (CalculatorCommand)
    defines a binding between a Receiver object and an action
    implements Execute by invoking the corresponding operation(s) on Receiver
3. Client  (CommandApp)
    creates a ConcreteCommand object and sets its receiver
4. Invoker  (User)
    asks the command to carry out the request
5. Receiver  (Calculator)
    knows how to perform the operations associated with carrying out the request.

Structural code in C#

This structural code demonstrates the Command pattern which stores requests as objects allowing clients to execute or playback the requests.
*/

namespace Behavioral.Command
{

    static void Main()
    {
        Receiver receiver = new Receiver();
        Command command = new ConcreteCommand(receiver);
        Invoker invoker = new Invoker();
        
        invoker.SetCommand(command);
        invoker.ExecuteCommand();
    }
    abstract class Command
    {
        protected Receiver receiver;
        public Command(Receiver receiver)
        {
            this.receiver = receiver;
        }
        
        public abstract void Execute();
    }
    
    class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver receiver) : 
            base(receiver)
        {
        }
        
        public override void Execute()
        {
            reveiver.Action()    
        }
    }
    
    class Receiver
    {
        public void Action()
        {
            Console.WriteLine("Called Receiver.Action()");
        }
    }
    
    class Invoker
    {
        private Command _command;
        
        public void SetCommand(Command command)
        {
            this._command = command;
        }
        
        public void ExecuteCommand()
        {
            _command.Execute();
        }
    }
}

/*
Real-worl code in C#

This real-world code demonstrates the Command pattern used in a simple calculator with unlimited number of undo's and redo's. Note that in C# the word 'operator' is a keyword. Prefixing it with '@' allows using it as an identifier

*/

namespace Behavioral.Command.RealWorld
{
    abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }

    class CalculatorCommand : Command
    {
        private char _operator;
        private int _operand;
        private Calculator _calculator;

        public CalculatorCommand(Calculator calculator, 
            char @operator, int operand)
        {
            this._calculator = calculator;
            this._operand = operand;
            this._operator = @operator;
        }

        // Gets operator
        public char Operator
        {
            set { _operator = value; }
        }

        // Get operand
        public int Operand
        {
            set { _operand = value; }
        }

        // Execute new command
        public override void Execute()
        {
            _calculator.Operation(_operator, _operand);
        }

        // Unexecute last command
        public override void UnExecute()
        {
            _calculator.Operation(Undo(_operator), _operand);
        }

        // Return opposite operator for given operator
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default: throw new
                    ArgumentException("@operator");
            }    
        }
    }

    class Calculator
    {
        private int _curr = 0;

        public void Operation(char @operation, int operand)
        {
            switch (@operator)
            {
                case '+': _curr += operand; break;
                case '-': _curr += operand; break;
                case '*': _curr *= operand; break;
                case '/': _curr /= operand; break;
            }
            Console.WriteLine("Current value = {0,3} (following {1} {2})", _curr, @operator, operand);
        }
    }
    
    class User
    {
        // Initializers
        private Calculator _calculator = new Calculator();
        private List<Command> _commands = new List<Command>();
        private int _current = 0;
        
        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            Command command = new CalculatorCommand(
                _calculator, @operator, operand);
            command.Execute();
            
            // Add command to undo list
            _command.Add(command);
            _current++;
        }
        
        public void Undo(int levels)
        {
            Console.WriteLine("\n--- Undo {0} levels ", levels);
            // Perform undo operations
            for(int i = 0; i < levels; i++)
            {
                if (_current > 0)
                {
                    Command command = _command[--_current] as Command;
                    command.UnExecute();
                }
            }
        }
        
        public void Redo(int levels)
        {
            Console.WriteLine("\n--- Redo {0} levels ", levels);
            // Perform redo operations
            for(int i = 0; i < levels; i++)
            {
                if(_current < _command.Count - 1)
                {
                    Command command = _command[_current++];
                    command.Execute();
                }
            }
        }
        
    }
}


