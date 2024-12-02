﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_test_avalonia.Models
{
    //возможно, удалю
    
    public class Expr
    {
        private string? _infixExpr;
        private StringBuilder _postfixExpr = new();
        private Dictionary<char, int> _priority = new()
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2}
        };

        public Expr(string expression)
        {
            _infixExpr = expression;
            GetPostfixExpr(expression, ref _postfixExpr);
        }
        private StringBuilder GetPostfixExpr(string exp, ref StringBuilder postfix)
        {
            var _operandStack = new Stack<Char>();
            var currentPriority = -1;
            
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
                    if (currentPriority == -1 || _priority[exp[i]] > currentPriority)
                    {
                        currentPriority = _priority[exp[i]];
                    }
                    else
                    {
                        CleanStack(ref _operandStack, ref postfix);
                    }

                    _operandStack.Push(exp[i]);
                }
            }
            
            //???
           return postfix;
        }

        private void CleanStack(ref Stack<char> _opStack, ref StringBuilder pstfx)
        {
            while (_opStack.Count > 0)
            {
                pstfx.Append(_opStack.Last());
                _opStack.Pop();
            }
        }
    }

    public class Calculator
    {
        private string? _expr;
        public double _result = 0;

        public Calculator(string expr)
        {
            _expr = expr;
            Calculate(_expr, ref _result);
        }
        public double Calculate(string exp, ref double res)
        {
            var _stack = new Stack<double>();

            foreach (var s in exp)
            {
                if (Char.IsDigit(s))
                    _stack.Push(Convert.ToDouble(s));
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
                }
            }

            return res;
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
