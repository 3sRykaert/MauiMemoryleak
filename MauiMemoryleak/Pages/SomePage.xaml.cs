using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMemoryleak.Pages;

public partial class SomePage : ContentPage
{
    public SomePage(SomePageViewmodel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }
}