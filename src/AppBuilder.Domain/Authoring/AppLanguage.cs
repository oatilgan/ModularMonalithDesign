using AppBuilder.SharedKernel.Domain;

namespace AppBuilder.Domain.Authoring;

public class AppLanguage : Entity
{
    private AppLanguage() { }

    public Guid AppId { get; private set; }
    public string LanguageCode { get; private set; } = default!;  // "tr", "en", "de"
    public bool IsDefault { get; private set; }

    public AppLanguage(Guid appId, string languageCode, bool isDefault = false)
    {
        Id = Guid.NewGuid();

        AppId = appId;
        LanguageCode = languageCode.ToLowerInvariant();
        IsDefault = isDefault;
    }

    public void SetIsDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }
}
