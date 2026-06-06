using Microsoft.AspNetCore.Components.Web;

namespace CardExplorer.Client.Core;

public static class UiHelper
{
    public static Func<KeyboardEventArgs, Task> OnEnterPressedAsync(Func<Task> action)
    {
        return e =>
        {
            if (e.Key == "Enter")
            {
                return action();
            }

            return Task.CompletedTask;
        };
    }
}