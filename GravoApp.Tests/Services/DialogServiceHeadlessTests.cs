using Avalonia.Controls;
using Avalonia.Headless.NUnit;
using Avalonia.Interactivity;
using Avalonia.Threading;
using FluentAssertions;
using GravoApp.Services;
using GravoApp.Tests.Support;
using GravoApp.ViewModels;
using GravoApp.Views;

namespace GravoApp.Tests.Services;

public class DialogServiceHeadlessTests
{
    private static Window Owner()
    {
        var owner = new Window();
        owner.Show();
        return owner;
    }

    private static T OpenedDialog<T>(Window owner) where T : Window
    {
        Dispatcher.UIThread.RunJobs();
        return owner.OwnedWindows.OfType<T>().Single();
    }

    private static void Click(Button button) => button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

    [AvaloniaTest]
    public void ShowMessage_ShowsTextAndOnlyOk()
    {
        var owner = Owner();
        var task = new DialogService(() => owner, Fakes.Texts()).ShowMessageAsync("Titel", "Hallo");
        var dialog = OpenedDialog<MessageWindow>(owner);
        dialog.Title.Should().Be("Titel");
        dialog.MessageText.Text.Should().Be("Hallo");
        dialog.OkButton.IsVisible.Should().BeTrue();
        dialog.OkButton.Content.Should().Be("T85");
        dialog.YesButton.IsVisible.Should().BeFalse();
        Click(dialog.OkButton);
        Dispatcher.UIThread.RunJobs();
        task.IsCompletedSuccessfully.Should().BeTrue();
    }

    [AvaloniaTest]
    public void Confirm_Yes_ReturnsTrue_No_ReturnsFalse()
    {
        var owner = Owner();
        var service = new DialogService(() => owner, Fakes.Texts());
        var yes = service.ConfirmAsync("t", "m");
        var confirm = OpenedDialog<MessageWindow>(owner);
        confirm.YesButton.Content.Should().Be("T13");
        confirm.NoButton.Content.Should().Be("T14");
        Click(confirm.YesButton);
        Dispatcher.UIThread.RunJobs();
        yes.Result.Should().BeTrue();

        var no = service.ConfirmAsync("t", "m");
        Click(OpenedDialog<MessageWindow>(owner).NoButton);
        Dispatcher.UIThread.RunJobs();
        no.Result.Should().BeFalse();
    }

    [AvaloniaTest]
    public void Prompt_Ok_ReturnsText_Cancel_ReturnsNull()
    {
        var owner = Owner();
        var service = new DialogService(() => owner, Fakes.Texts());
        var ok = service.PromptAsync("t", "Name", "alt");
        var prompt = OpenedDialog<PromptWindow>(owner);
        prompt.LabelText.Text.Should().Be("Name");
        prompt.InputBox.Text.Should().Be("alt");
        prompt.OkButton.Content.Should().Be("T85");
        prompt.CancelButton.Content.Should().Be("T94");
        prompt.InputBox.Text = "neu";
        Click(prompt.OkButton);
        Dispatcher.UIThread.RunJobs();
        ok.Result.Should().Be("neu");

        var cancelled = service.PromptAsync("t", "Name", "alt");
        Click(OpenedDialog<PromptWindow>(owner).CancelButton);
        Dispatcher.UIThread.RunJobs();
        cancelled.Result.Should().BeNull();
    }

    [AvaloniaTest]
    public void ShowDialog_ClosesWithRequestCloseResult()
    {
        var owner = Owner();
        var vm = new ProbeViewModel();
        var task = new DialogService(() => owner, Fakes.Texts()).ShowDialogAsync(vm);
        OpenedDialog<ProbeWindow>(owner).Should().NotBeNull();
        vm.Close(true);
        Dispatcher.UIThread.RunJobs();
        task.Result.Should().BeTrue();
    }
}
