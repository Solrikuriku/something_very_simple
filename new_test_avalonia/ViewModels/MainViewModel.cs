using ReactiveUI;
using System.ComponentModel;
using System;
using System.Reactive;
using System.Windows.Input;
using System.Xml.Linq;

namespace new_test_avalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ICommand WriteNum { get; set; }
   
    public string Message
    {
        get => "1";
    }
}
