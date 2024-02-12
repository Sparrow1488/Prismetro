using MahApps.Metro.Controls.Dialogs;
using Prismetro.Core.Contracts;

namespace Prismetro.Core.Views;

public abstract class DialogContainerBase : BaseMetroDialog, IDialogContainerCoreSupport
{
    public abstract IDialogContainerCore Core { get; }
}