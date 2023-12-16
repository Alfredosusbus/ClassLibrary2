using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{

    public class Context
    {
        public readonly System.Collections.Generic.Dictionary<string, bool> variables = new System.Collections.Generic.Dictionary<string, bool>();

        public bool Lookup(string variableName)
        {
            if (variables.TryGetValue(variableName, out var value))
                return value;

            return false; // Default value for undefined variables
        }

        public void Assign(string variableName, bool value)
        {
            variables[variableName] = value;
        }
    }

    public class TerminalExpression : IExpression
    {
        public readonly string variableName;

        public TerminalExpression(string variableName)
        {
            this.variableName = variableName;
        }

        public bool Interpret(Context context)
        {
            return context.Lookup(variableName);
        }
    }

    public class AndExpression : IExpression
    {
        public readonly IExpression expression1;
        public readonly IExpression expression2;

        public AndExpression(IExpression expression1, IExpression expression2)
        {
            this.expression1 = expression1;
            this.expression2 = expression2;
        }

        public bool Interpret(Context context)
        {
            return expression1.Interpret(context) && expression2.Interpret(context);
        }
    }

    public class OrExpression : IExpression
    {
        public readonly IExpression expression1;
        public readonly IExpression expression2;

        public OrExpression(IExpression expression1, IExpression expression2)
        {
            this.expression1 = expression1;
            this.expression2 = expression2;
        }

        public bool Interpret(Context context)
        {
            return expression1.Interpret(context) || expression2.Interpret(context);
        }
    }

    public class NotExpression : IExpression
    {
        public readonly IExpression expression;

        public NotExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public bool Interpret(Context context)
        {
            return !expression.Interpret(context);
        }
    }
}