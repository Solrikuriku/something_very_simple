using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace new_test_avalonia.Models
{
    //возможно, удалю
    
    public class PostfixExpr
    {
        private Dictionary<char, int> _priority = new()
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2}
        };

        public List<string> GetPostfixExpr(string exp)
        {
            var _operandStack = new Stack<string>();
            var currentPriority = -1;
            var postfix = new List<string>();
            var nums = new List<int>();
            var num = new StringBuilder();

            //нет смысла делать ловушки для дурака, так как текстбокс не пропустит ошибочные символы

            /*
             * Если символ последний, то он добавляется, а затем все оставшееся из стека.
             * Если число, то просто добавляется.
             * Если операнд, то определяется его приоритет.
             */
            for (int i = 0; i < exp.Length - 1; i++)
            {                
                if (Char.IsDigit(exp[i]))
                { 
                    num.Append(exp[i]);
                }
                else
                {
                    //var symbol = Convert.ToChar(exp[i]);

                    postfix.Add(num.ToString());
                    num.Clear();

                    if (currentPriority != -1 && _priority[exp[i]] <= currentPriority)
                        CleanStack(ref _operandStack, ref postfix);

                    currentPriority = _priority[exp[i]];
                    _operandStack.Push(exp[i].ToString());
                }
            }

            num.Append(exp[exp.Length - 1]);
            postfix.Add(num.ToString());
            CleanStack(ref _operandStack, ref postfix);

            //???
            return postfix;
        }

        private void CleanStack(ref Stack<string> _opStack, ref List<string> pstfx)
        {
            while (_opStack.Count > 0)
            {
                pstfx.Add(_opStack.Pop());
            }
        }
    }

    public class Calculator
    {
        public string Calculate(List<string> exp)
        {
            var _stack = new Stack<double>();
            double res = 0;

            foreach (var s in exp)
            {
                if (double.TryParse(s, out var number))
                    _stack.Push(Convert.ToDouble(s.ToString()));
                else
                {
                    var a = _stack.Pop();
                    var b = _stack.Pop();

                    switch (s)
                    {
                        case "+":
                            res = b + a;
                            break;
                        case "-":
                            res = b - a;
                            break;
                        case "*":
                            res = b * a;
                            break;
                        case "/":
                            if (a == 0) return "null";
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

    public enum Status
    {
        Empty,
        Zero,
        Operator,
        Number
    }
}
