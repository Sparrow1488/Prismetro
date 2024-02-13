using Prism.Commands;
using Prism.Regions;
using Prismetro.Core.Defaults;
using Prismetro.Core.Extensions;
using Prismetro.Core.Models.Scope;
using Prismetro.Core.Views;
using Prismetro.Core.Views.Components;

namespace Prismetro.Core.Models.Navigation;

public record MessageDialogNavigate(string Text) : Navigate<ButtonResult>(
    PrismetroRegions.MessageDialogRegion, 
    new NavigationParameters().AddPair(NavigateKeys.Message.Text, Text)
);

public record MessageDialogView : DialogView<LaidDialogContainer, ButtonResult>, IDisposable
{
    private IDisposable? _agreementSub;
    private Agreement? _agreement;

    public MessageDialogView(string title)
    {
        OnShow = (container, scope) =>
        {
            if (scope is not DialogScope<ButtonResult> resultScope) throw new Exception();
            
            _agreement = new Agreement();
            _agreementSub = _agreement.Submit.Subscribe(resultScope.PushAndCloseResult);

            container.Bottom = _agreement;
            container.Header = new DefaultHeader(title)
            {
                CloseCommand = new DelegateCommand(scope.RequestClose)
            };
        };
    }

    public void Dispose()
    {
        _agreement?.Dispose();
        _agreementSub?.Dispose();
    }
}