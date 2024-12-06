using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace new_test_avalonia.Models
{
    //возможно, удалю
    
    public class Expr
    {
        //private string _infixExpr = String.Empty;
        private string _postfixExpr = String.Empty;
        private Dictionary<char, int> _priority = new()
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2}
        };

        //public string InfixExpr
        //{
        //    private get => _infixExpr;
        //    set => _infixExpr = value;
        //}

        public string PostfixExpr
        {
            get => _postfixExpr;
            set => _postfixExpr = GetPostfixExpr(value);
        }

        //public Expr(string expression)
        //{
        //    _infixExpr = expression;
        //    //GetPostfixExpr(expression, ref _postfixExpr);
        //    _postfixExpr = GetPostfixExpr(_infixExpr);
        //}
        private string GetPostfixExpr(string exp)
        {
            var _operandStack = new Stack<Char>();
            var currentPriority = -1;
            var postfix = new StringBuilder();
            
            //нет смысла делать ловушки для дурака, так как текстбокс не пропустит ошибочные символы

            /*
             * Если символ последний, то он добавляется, а затем все оставшееся из стека.
             * Если число, то просто добавляется.
             * Если операнд, то определяется его приоритет.
             */
            for(int i = 0; i < exp.Length; i++)
            {
                if (i == exp.Length - 1)
                {
                    postfix.Append(exp[i]);
                    CleanStack(ref _operandStack, ref postfix);
                }
                else if (Char.IsDigit(exp[i]))
                    postfix.Append(exp[i]);
                else
                {
                    //if (currentPriority == -1 || _priority[exp[i]] > currentPriority)
                    //{
                    //    currentPriority = _priority[exp[i]];
                    //}
                    if (currentPriority != -1 && _priority[exp[i]] <= currentPriority)
                    {
                        CleanStack(ref _operandStack, ref postfix);
                        //currentPriority = _priority[exp[i]];
                        //currentPriority = -1;
                    }
                    currentPriority = _priority[exp[i]];
                    _operandStack.Push(exp[i]);
                }
            }
            
            //???
           return postfix.ToString();
        }

        private void CleanStack(ref Stack<char> _opStack, ref StringBuilder pstfx)
        {
            while (_opStack.Count > 0)
            {
                //pstfx.Append(_opStack.Last());
                //_opStack.Pop();
                //var a = _opStack.Pop();
                pstfx.Append(_opStack.Pop());
            }
        }
    }

    public class Calculator
    {
        //private string _expr = String.Empty;
        private string _result = "0";

        //public string Expression
        //{
        //    private get => _expr;
        //    set => _expr = value;
        //}

        public string Result
        {
            get => _result;
            set => _result = Calculate(value);
        }

        public string Calculate(string exp)
        {
            var _stack = new Stack<double>();
            double res = 0;

            foreach (var s in exp)
            {
                if (Char.IsDigit(s))
                    _stack.Push(Convert.ToDouble(s.ToString()));
                else
                {
                    var a = _stack.Pop();
                    var b = _stack.Pop();

                    switch (s)
                    {
                        case '+':
                            res = b + a;
                            break;
                        case '-':
                            res = b - a;
                            break;
                        case '*':
                            res = b * a;
                            break;
                        case '/':
                            res = b / a;
                            break;
                        default:
                            res = 0;
                            break;
                    }

                    _stack.Push(res);
                }
            }

            return res.ToString();
        }
    }

    //class Calculator
    //{
    //    private double _firstValue = 0;
    //    private double _secondValue = 0;
    //    private double _result = 0;

    //    public double FirstValue
    //    {
    //        get => _firstValue;
    //        set => _firstValue = value;
    //    }

    //    public double SecondValue
    //    {
    //        get => _secondValue;
    //        set => _secondValue = value;
    //    }

    //    public double Result
    //    {
    //        get => _result;
    //        set => _result = value;
    //    }
    //}

    public enum Operation
    {
        Empty,
        Add,
        Subtract,
        Multiply,
        Divide,
        Result
    }

    public enum Status
    {
        Empty,
        Operator,
        Number
    }
}
