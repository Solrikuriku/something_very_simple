using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_test_avalonia.Models
{
    //возможно, удалю
    
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
