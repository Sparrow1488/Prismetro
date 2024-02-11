using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using Prism.Mvvm;

namespace Prismetro.App.Wpf.ViewModels;

public abstract class ValidationViewModel : BindableBase, IDataErrorInfo
{
    private Dictionary<string, (Func<object?> ValueSelector, ValidationRule Rule)>? _validators;
    private Dictionary<string, (Func<object?> ValueSelector, ValidationRule Rule)> Validators => _validators ??= new();

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            if (!Validators.TryGetValue(columnName, out var tuple)) return string.Empty;
            
            var result = tuple.Rule.Validate(tuple.ValueSelector.Invoke(), CultureInfo.CurrentCulture);
            return result.ErrorContent?.ToString() ?? string.Empty;
        }
    }

    protected void AddValidator(string property, Func<object?> valueSelector, ValidationRule rule)
    {
        Validators.Add(property, (valueSelector, rule));
    }
}