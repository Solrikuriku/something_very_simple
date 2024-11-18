using ReactiveUI;
using System.ComponentModel;
using System;
using System.Reactive;
using System.Windows.Input;
using System.Xml.Linq;
using new_test_avalonia.Models;
using System.Text;
using System.Linq.Expressions;

namespace new_test_avalonia.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //да, я пишу комментарии на родном языке и че вы мне сделаете
        
        //private Calculator _calculator = new Calculator();
        private string _displayText = "0";
        private Operation _operation = Operation.Empty;
        private Status _status = Status.Empty;
        public ReactiveCommand<string, Unit> AddNewSymbol { get; }

        public MainViewModel()
        {
            AddNewSymbol = ReactiveCommand.Create<string>(AddSymbol);
        }

        //отображение текста
        public string DisplayText
        {
            get => _displayText;
            set => this.RaiseAndSetIfChanged(ref _displayText, value);  
        }

        //добавление символа для отображения
        private void AddSymbol(string symbol)
        {
            if (IsCombinationAllowed(symbol)) 
            { 
                if (_status == Status.Empty) DisplayText = String.Empty;
                ChangeStatus(symbol);
                DisplayText += symbol;
            }
        }

        /*
         * разрешенные комбинации: 
         * 1. стартовый статус + число + не нуль
         * 2. любой символ при статусе цифры
         * 3. статус оператора + число + не нуль
         * 
         * остальные комбинации запрещены, так как могут привести 
         * к некорректному отображению
         * например, нуль после оператора в начале числа, двойные-тройные операторы и т.д.
         */
        private bool IsCombinationAllowed(string s) => (_status == Status.Empty && IsDigit(s) && !IsZero(s)) || (_status == Status.Number)
                                                        || (_status == Status.Operator && IsDigit(s) && !IsZero(s));

        //является ли числом
        private bool IsDigit(string s) => int.TryParse(s, out int n);
        //является ли нулем
        private bool IsZero(string s) => s == "0";
        private void ChangeStatus(string s) 
        {
            if (IsDigit(s)) _status = Status.Number;
            else _status = Status.Operator;
        }
    }
}
