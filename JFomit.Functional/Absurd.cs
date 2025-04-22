namespace JFomit.Functional;

/// <summary>
/// Provides a type that is not populated.
/// No instance of <see cref="Absurd"/> type can be constructed via normal means.
/// <br/> This class cannot be inherited.
/// </summary>
public sealed record Absurd
{
    private Absurd() { }
}