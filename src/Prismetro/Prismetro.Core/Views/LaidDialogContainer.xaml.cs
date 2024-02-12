using System.Windows;
using Prism.Regions;
using Prismetro.Core.Contracts;

namespace Prismetro.Core.Views;

public partial class LaidDialogContainer : IDisposable
{
    #region Dependency Properties

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(LaidDialogContainer),
        new PropertyMetadata(OnDependencyPropertyChanged)
    );

    public static readonly DependencyProperty BottomProperty = DependencyProperty.Register(
        nameof(Bottom),
        typeof(object),
        typeof(LaidDialogContainer),
        new PropertyMetadata(OnDependencyPropertyChanged)
    );

    private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((LaidDialogContainer)d)._propertyChanged?.Invoke(e.Property.Name);

    #endregion

    private Action<string>? _propertyChanged;
    
    public LaidDialogContainer(IRegionManager regionManager)
    {
        InitializeComponent();

        BodyPresenter.Content = Core = new DialogContainerCore(regionManager);
        _propertyChanged += OnThisPropertyChanged;
    }

    public override IDialogContainerCore Core { get; }
    
    #region Properties

    public object? Header
    {
        get => (object?) GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    
    public object? Bottom
    {
        get => (object?) GetValue(BottomProperty);
        set => SetValue(BottomProperty, value);
    }

    #endregion

    private void OnThisPropertyChanged(string name)
    {
        _ = name switch
        {
            nameof(Header) => HeaderPresenter.Content = Header,
            nameof(Bottom) => BottomPresenter.Content = Bottom,
            _ => string.Empty // Nothing
        };
    }

    public virtual void Dispose()
    {
        _propertyChanged -= OnThisPropertyChanged;
    }
}