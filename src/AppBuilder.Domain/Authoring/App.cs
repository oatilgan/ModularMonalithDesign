using AppBuilder.SharedKernel.Domain;

namespace AppBuilder.Domain.Authoring;

public class App : AggregateRoot
{
    private readonly List<Page> _pages = new();
    private readonly List<AppLanguage> _languages = new();

    // EF Core için parameterless ctor
    private App() { }

    public string Name { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string DefaultLanguageCode { get; private set; } = "en";

    public IReadOnlyCollection<Page> Pages => _pages.AsReadOnly();
    public IReadOnlyCollection<AppLanguage> Languages => _languages.AsReadOnly();

    public App(string name, string slug, string defaultLanguageCode)
    {
        Id = Guid.NewGuid();

        Rename(name);
        SetSlug(slug);
        SetDefaultLanguage(defaultLanguageCode);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("App name cannot be empty.", nameof(name));

        Name = name.Trim();
    }

    public void SetSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be empty.", nameof(slug));

        Slug = slug.Trim().ToLowerInvariant();
    }

    public void SetDefaultLanguage(string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode))
            throw new ArgumentException("Language code cannot be empty.", nameof(languageCode));

        DefaultLanguageCode = languageCode.ToLowerInvariant();

        // Eğer dil listesinde yoksa ekle
        if (_languages.All(l => l.LanguageCode != DefaultLanguageCode))
        {
            _languages.Add(new AppLanguage(Id, DefaultLanguageCode, isDefault: true));
        }
        else
        {
            foreach (var lang in _languages)
            {
                lang.SetIsDefault(lang.LanguageCode == DefaultLanguageCode);
            }
        }
    }

    public Page AddPage(string name, string path, int order = 0)
    {
        var page = new Page(Id, name, path, order);
        _pages.Add(page);
        return page;
    }
}
