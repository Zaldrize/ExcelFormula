using System.Runtime.CompilerServices;

namespace ExcelFormulaWrapper
{
    public interface IFormula
    {
        public string Build();
    }

    public abstract class AbstractFormula : IFormula
    {
        public abstract string Build();
        public override string ToString()
        {
            return Build();
        }
    }

    public class Const : AbstractFormula
    {
        private readonly string _c;

        public Const(string c)
        {
            _c = c;
        }

        public Const(ValueType t)
        {
            _c = t.ToString() ?? "";
        }
        public override string Build()
        {
            return _c;
        }
    }

    public class Column : AbstractFormula
    {
        private int _index;

        public Column(int index) : base()
        {
            _index = index;
        }

        public override string Build()
        {
            return "{{" + _index + "}}";
        }
    }

    public abstract class Operator : AbstractFormula
    {
        private IFormula _left, _right;
        private string _operator;

        public Operator(IFormula left, string @operator, IFormula right)
        {
            _left = left;
            _right = right;
            _operator = @operator;
        }

        public override string Build()
        {
            return _left.Build() + _operator + _right.Build();
        }
    }

    public abstract class TwoOperands : AbstractFormula
    {
        private IFormula _left, _right;
        private string _operator;

        public TwoOperands(string @operator, IFormula left, IFormula right)
        {
            _left = left;
            _right = right;
            _operator = @operator;
        }

        public override string Build()
        {
            return _operator + "(" + _left.Build() + "," + _right.Build() + ")";
        }
    }

    public abstract class ThreeOperands : AbstractFormula
    {
        private IFormula _first, _second, _third;
        private string _operator;

        public ThreeOperands(string @operator, IFormula first, IFormula second, IFormula third)
        {
            _operator = @operator;
            _first = first;
            _second = second;
            _third = third;
        }

        public override string Build()
        {
            return _operator + "(" + _first.Build() + ", " + _second.Build() + ", " + _third.Build() + ")";
        }
    }

    public class If : ThreeOperands
    {
        public If(IFormula @if, IFormula @then, IFormula @else) : base("IF", @if,@then, @else)
        {
        }
    }

    public class Greater : Operator
    {
        public Greater(IFormula left, IFormula right) : base(left, ">", right)
        {
        }
    }

    public class Subtract : Operator
    {
        public Subtract(IFormula left, IFormula right) : base(left, "-", right)
        {
        }
    }
    public class Add : Operator
    {
        public Add(IFormula left, IFormula right) : base(left, "+", right)
        {
        }
    }

    public class Equals : Operator
    {
        public Equals(IFormula left, IFormula right) : base(left, "=", right)
        {
        }
    }

    public class Min: TwoOperands
    {
        public Min(IFormula left, IFormula right) : base("MIN", left, right)
        {
        }
    }
    public class Max : TwoOperands
    {
        public Max(IFormula left, IFormula right) : base("MAX", left, right)
        {
        }
    }
}
