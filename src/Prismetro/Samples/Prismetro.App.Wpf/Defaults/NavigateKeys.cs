using System.Diagnostics.CodeAnalysis;

namespace Prismetro.App.Wpf.Defaults;

public static class NavigateKeys
{
    public static class Greeting
    {
        // MARK: атрибутами можно помечать, является ли аргумент с данным именем обязательным или нет (еще можно указывать тип)
        [NotNull]
        public const string Name = "Name";
        [AllowNull]
        public const string TextValidation = "TextValidation";
    }
}