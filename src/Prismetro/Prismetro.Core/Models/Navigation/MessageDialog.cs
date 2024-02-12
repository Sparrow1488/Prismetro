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

public record MessageDialogView : DialogView<LaidDialogContainer>, IDisposable
{
    private IDisposable? _agreementSub;

    public MessageDialogView(string title)
    {
        OnShow += (container, scope) =>
        {
            if (scope is not DialogScope<ButtonResult> resultScope) return;
            
            var agreement = new Agreement();
            _agreementSub = agreement.Submit.Subscribe(res 
                => resultScope.PushAndCloseResult(res));

            container.Bottom = agreement;
            container.Header = new DefaultHeader(title)
            {
                CloseCommand = new DelegateCommand(scope.RequestClose)
            };
        };
    }

    public void Dispose()
    {
        _agreementSub?.Dispose();
    }
}