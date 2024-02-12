using System.Windows.Media;
using Prismetro.Core.Contracts;
using Prismetro.Core.Models.Scope;

namespace Prismetro.Core.Models.Navigation;

/// <summary>
/// Настройки отображения диалогового окна
/// </summary>
/// <typeparam name="TContainer">Тип используемого контейнера</typeparam>
public record DialogView<TContainer> // TODO: Dispose!!!
    where TContainer : IDialogContainerCoreSupport
{
    public Action<TContainer, DialogScope>? OnShow { get; protected init; }
    public Brush? WindowDarkModeOverlayBrush { get; } = new SolidColorBrush(Colors.Black);
}