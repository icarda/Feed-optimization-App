namespace FeedOptimizationApp.Localization;

[ContentProperty(nameof(Key))]
public class TranslateExtension : IMarkupExtension
{
    /// <summary>
    /// The translation key to look up in TranslationProvider
    /// </summary>
    public string Key { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrWhiteSpace(Key))
            return $"[{Key}]";

        // Try to get the target object and property from XAML
        var valueTargetProvider = (IProvideValueTarget?)serviceProvider.GetService(typeof(IProvideValueTarget));
        var targetObject = valueTargetProvider?.TargetObject as BindableObject;
        var targetProperty = valueTargetProvider?.TargetProperty as BindableProperty;

        if (targetObject == null || targetProperty == null)
            return $"[{Key}]";

        // Get TranslationProvider from resources
        if (Application.Current.Resources.TryGetValue("TranslationProvider", out var providerObj)
            && providerObj is TranslationProvider translationProvider)
        {
            // Initial value
            targetObject.SetValue(targetProperty, translationProvider[Key]);

            // Subscribe to LanguageChanged to update the target automatically
            translationProvider.LanguageChanged += (s, e) =>
            {
                targetObject.SetValue(targetProperty, translationProvider[Key]);
            };
        }

        return targetObject.GetValue(targetProperty);
    }
}