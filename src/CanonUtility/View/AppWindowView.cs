namespace CanonUtility.View;

public class AppWindowView : Window
{
    public AppWindowView()
    {
        ResizeMode = ResizeMode.CanResizeWithGrip;
        SetBinding(TitleProperty, new Binding("Title"));

        TaskbarItemInfo taskbarItemInfo = new TaskbarItemInfo();
        BindingOperations.SetBinding(taskbarItemInfo, TaskbarItemInfo.DescriptionProperty, new Binding("Title"));
        BindingOperations.SetBinding(taskbarItemInfo, TaskbarItemInfo.ProgressStateProperty, new Binding("ProgressState"));
        BindingOperations.SetBinding(taskbarItemInfo, TaskbarItemInfo.ProgressValueProperty, new Binding("ProgressValue"));
        TaskbarItemInfo = taskbarItemInfo;

        SetKeyBinding(Key.F5, "RefreshCommand");
        SetEventBinding("Loaded", "StartupCommand");
        SetEventBinding("Closing", "ClosingCommand", true);

        AllowDrop = true;
        SetEventBinding("PreviewDragEnter", "DragCommand");
        SetEventBinding("PreviewDragOver", "DragCommand");
        SetEventBinding("PreviewDrop", "DropCommand");
    }

    protected void SetKeyBinding(Key key, string commandName)
    {
        KeyBinding keyBinding = new() { Key = key };
        BindingOperations.SetBinding(keyBinding, KeyBinding.CommandProperty, new Binding(commandName));
        InputBindings.Add(keyBinding);
    }

    protected void SetEventBinding(string eventName, string commandName, bool passEventArgsToCommand = false)
    {
        Microsoft.Xaml.Behaviors.EventTrigger trigger = new(eventName);
        Microsoft.Xaml.Behaviors.InvokeCommandAction action = new() { PassEventArgsToCommand = passEventArgsToCommand };
        BindingOperations.SetBinding(action, Microsoft.Xaml.Behaviors.InvokeCommandAction.CommandProperty, new Binding(commandName));
        trigger.Actions.Add(action);
        Microsoft.Xaml.Behaviors.Interaction.GetTriggers(this).Add(trigger);
    }
}
