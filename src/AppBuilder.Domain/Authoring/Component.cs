using AppBuilder.SharedKernel.Domain;

namespace AppBuilder.Domain.Authoring;

public class Component : Entity
{
    private Component() { }

    public Guid PageId { get; private set; }
    public string Type { get; private set; } = default!;       // "text", "button", "image"...
    public string JsonConfig { get; private set; } = "{}";     // props'lar JSON olarak

    public Component(Guid pageId, string type, string jsonConfig)
    {
        Id = Guid.NewGuid();

        PageId = pageId;
        SetType(type);
        UpdateConfig(jsonConfig);
    }

    public void SetType(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Component type cannot be empty.", nameof(type));

        Type = type.Trim().ToLowerInvariant();
    }

    public void UpdateConfig(string jsonConfig)
    {
        JsonConfig = string.IsNullOrWhiteSpace(jsonConfig) ? "{}" : jsonConfig;
    }
}
