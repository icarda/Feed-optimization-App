namespace FeedOptimizationApp.Localization;

public class TranslateExtension : IMarkupExtension<BindingBase>
{
    public string Key { get; set; }

    public BindingBase ProvideValue(IServiceProvider serviceProvider)
    {
        Console.WriteLine($"[TranslateExtension] Providing value for key: {Key}");
        var translationProvider = App.ServiceProvider.GetService<TranslationProvider>();

        if (translationProvider == null)
        {
            Console.WriteLine("[TranslateExtension] TranslationProvider is null. Ensure it is registered in the service container.");
        }

        return new Binding
        {
            Mode = BindingMode.OneWay,
            Path = $"[{Key}]",
            Source = translationProvider
        };
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        => ProvideValue(serviceProvider);
}