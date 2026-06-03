using Microsoft.AspNetCore.Components.Web;

namespace SetExplorer.Client.Core;

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