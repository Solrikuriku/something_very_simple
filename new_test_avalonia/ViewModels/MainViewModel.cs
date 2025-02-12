﻿using ReactiveUI;
using System.ComponentModel;
using System;
using System.Reactive;
using System.Windows.Input;
using System.Xml.Linq;
using new_test_avalonia.Models;
using System.Text;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace new_test_avalonia.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //да, я пишу комментарии на родном языке и че вы мне сделаете
        
        private string _displayText = "0";
        private Status _status = Status.Empty;
        public ReactiveCommand<char, Unit> AddNewSymbol { get; }
        public ReactiveCommand<Unit, Unit> ClearAll { get; }
        public ReactiveCommand<Unit, Unit> BackSpace { get; }
        public ReactiveCommand<Unit, Unit> Result { get; }

        public MainViewModel()
        {
            AddNewSymbol = ReactiveCommand.Create<char>(AddSymbol);
            ClearAll = ReactiveCommand.Create(Clearing);
            BackSpace = ReactiveCommand.Create(DeletePrevious);
            Result = ReactiveCommand.Create(CalculateAll);
        }

        //отображение текста
        public string DisplayText
        {
            get => _displayText;
            set => this.RaiseAndSetIfChanged(ref _displayText, value);  
        }

        //добавление символа для отображения
        private void AddSymbol(char symbol)
        {
            var digit = string.Empty;

            if (IsCombinationAllowed(symbol)) 
            { 
                if (_status == Status.Empty) DisplayText = String.Empty;
                ChangeStatus(symbol);
                DisplayText += symbol;
            }
        }

        private void CalculateAll()
        {
            if (_status != Status.Operator && _status != Status.Empty)
            {
                var _postfixexpr = new PostfixExpr();
                var _listpostfix = _postfixexpr.GetPostfixExpr(DisplayText);

                var calc = new Calculator();
                DisplayText = calc.Calculate(_listpostfix);
            }
        }

        private void Clearing()
        {
            DisplayText = "0";
            _status = Status.Empty;
        }

        //не оч хорошо
        //создать изменяемые лист(стринг) для манипуляции с дисплейтекстом?
        //да, лучше поменять
        private void DeletePrevious()
        {
            if (DisplayText.Length > 1)
            {
                DisplayText = DisplayText.Remove(DisplayText.Length - 1);
                ChangeStatus(DisplayText[DisplayText.Length - 1]);
            }
            else if (DisplayText.Length == 1)
            {
                DisplayText = "0";
                _status = Status.Empty;
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
        private bool IsCombinationAllowed(char s) => (_status == Status.Number)
                                                        || (_status == Status.Operator && (IsZero(s) || Char.IsDigit(s)))
                                                        || (_status == Status.Empty && !IsZero(s) && Char.IsDigit(s))
                                                        || (_status == Status.Zero && !IsZero(s) && !Char.IsDigit(s));

        //является ли числом
        //private bool IsDigit(char s) => int.TryParse(s, out int n);

        //является ли нулем
        private bool IsZero(char s) => s == '0';
        private void ChangeStatus(char s) 
        {
            if (IsZero(s) && _status != Status.Number) _status = Status.Zero;
            else if (Char.IsDigit(s)) _status = Status.Number;
            else _status = Status.Operator;
        }
    }
}
