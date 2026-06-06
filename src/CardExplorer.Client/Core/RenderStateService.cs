namespace CardExplorer.Client.Core;

public class RenderStateService
{
    public bool IsInteractive { get; private set; }
    public event Action? OnInteractive;

    public void SetInteractive()
    {
        if (IsInteractive) return;
        IsInteractive = true;
        OnInteractive?.Invoke();
    }
}
