using MahApps.Metro.Controls.Dialogs;
using Prismetro.Core.Contracts;

namespace Prismetro.Core.Views;

public abstract class DialogContainer : BaseMetroDialog, IDialogContainerCoreSupport
{
    public abstract IDialogContainerCore Core { get; }
}