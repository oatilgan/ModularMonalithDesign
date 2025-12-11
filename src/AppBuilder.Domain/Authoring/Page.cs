using AppBuilder.SharedKernel.Domain;

namespace AppBuilder.Domain.Authoring;

public class Page : Entity
{
    private readonly List<Component> _components = new();

    private Page() { }

    public Guid AppId { get; private set; }
    public string Name { get; private set; } = default!;
    public string Path { get; private set; } = default!;
    public int Order { get; private set; }

    public IReadOnlyCollection<Component> Components => _components.AsReadOnly();

    public Page(Guid appId, string name, string path, int order = 0)
    {
        Id = Guid.NewGuid();

        AppId = appId;
        Rename(name);
        SetPath(path);
        Order = order;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Page name cannot be empty.", nameof(name));

        Name = name.Trim();
    }

    public void SetPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty.", nameof(path));

        Path = path.StartsWith("/") ? path : "/" + path;
    }

    public Component AddComponent(string type, string jsonConfig)
    {
        var component = new Component(Id, type, jsonConfig);
        _components.Add(component);
        return component;
    }
}
