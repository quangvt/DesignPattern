/*
Definition
Given a language, define a representation for its grammar along with an interpreter that uses the representation to interpret sentences in the language.

Frequency of use:Low

Participants

    The classes and objects participating in this pattern are:

1. AbstractExpression  (Expression)
    declares an interface for executing an operation
2. TerminalExpression  ( ThousandExpression, HundredExpression, TenExpression, OneExpression )
    implements an Interpret operation associated with terminal symbols in the grammar.
    an instance is required for every terminal symbol in the sentence.
3. NonterminalExpression  ( not used )
    one such class is required for every rule R ::= R1R2...Rn in the grammar
    maintains instance variables of type AbstractExpression for each of the symbols R1 through Rn.
    implements an Interpret operation for nonterminal symbols in the grammar. Interpret typically calls itself recursively on the variables representing R1 through Rn.
4. Context  (Context)
    contains information that is global to the interpreter
5. Client  (InterpreterApp)
    builds (or is given) an abstract syntax tree representing a particular sentence in the language that the grammar defines. The abstract syntax tree is assembled from instances of the NonterminalExpression and TerminalExpression classes
    invokes the Interpret operation

Structural code in C#

This structural code demonstrates the Interpreter patterns, which using a defined grammer, provides the interpreter that processes parsed statements.


*/

namespace Behavioral.Interpreter
{
    class MainApp
    {
        static void Main()
        {
            Context context = new Context();
            
            ArrayList list = new ArrayList();
            
            list.Add(new TerminalExpression());
            list.Add(new NonterminalExpression());
            list.Add(new TerminalExpression());
            list.Add(new TerminalExpression());
            
            foreach (AbstractExpression exp in list)
            {
                exp.Interpret(context);
            }
        }
    }
    
    class Context
    {
        
    }
    
    abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }
    
    class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("Called Terminal.Interpret()");
        }
    }
    
    class NonterminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("Called Nonterminal.Interpret()");
        }
    }
}

/*
Real-world code in C#

This real-world code demonstrates the Interpreter pattern which is used to convert a Roman numeral to a decimal.

*/

namespace Behavioral.Interpreter
{
    class MainApp
    {
        string roman = "MCMXXVIII";
        Context context = new Context(roman);
        
        // Build the 'parse tree'
        List<Expression> tree = new List<Expression>();
        tree.Add(new ThousandExpression());
        tree.Add(new HundredExpression());
        tree.Add(new TenExpression());
        tree.Add(new OneExpression());
        
        // Interpret
        foreach (Expression exp in tree)
        {
            exp.Interpret(context);
        }
        
        Console.WriteLine("{0} = {1}", roman, context.Output);
    }
    
    class Context
    {
        private string _input;
        private int _output;
        
        public Context(string input)
        {
            this._input = input;
        }
        
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }
        
        public int Output
        {
            get { return _output; }
            set { _output = value; }
        }
    }
    
    abstract class Expression
    {
        public void Interpret(Context context)
        {
            if(context.Input.Length == 0)
                return;
            
            if(context.Input.StartsWith(Nine()))
            {
                context.Output += (9 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if(context.Input.StartsWith(Four()))
            {
                context.Output += (4 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if(context.Input.StartsWith(Five()))
            {
                context.Ouptput += (5 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
            
            while(context.Input.StartsWith(One()))
            {
                context.Output += (1 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
        }
        
        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier(;)
    }
    
    // Thousand checks for the Roman Numeral M 
    class ThousandExpression : Expression
    {
        public override string One() { return "M"; }
        public override string Four() { return " "; }
        public override string Five() { return " "; }
        public override string Nine() { return " "; }
        public override string Multiplier() { return 1000; }
    }
    
    // Hundred checks C, CD, D or CM
    class HundredExpression : Expression
    {
        public override string One() { return "C"; }
        public override string Four() { return "CD"; }
        public override string Five() { return "D"; }
        public override string Nine() { return "CM"; }
        public override string Multiplier() { return 100; }
    }
    
    
    // Ten check for X, XL, L and XC 
    class TenExpression : Expression
    {
        public override string One() { return "X"; }
        public override string Four() { return "XL"; }
        public override string Five() { return "L"; }
        public override string Nine() { return "XC"; }
        public override string Multiplier() { return 10; }
    }
    
    // One check for I, II, III, IV, V, VI, VII, VIII, IX
    class TenExpression : Expression
    {
        public override string One() { return "I"; }
        public override string Four() { return "IV"; }
        public override string Five() { return "V"; }
        public override string Nine() { return "IX"; }
        public override string Multiplier() { return 1; }
    }
}