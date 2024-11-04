using Toci.PlotTwist.Wpf;
using TociCodeBooster.Interface;

namespace TociCodeBooster
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            //await VS.MessageBox.ShowWarningAsync("TociCodeBooster", "Button clicked");

            //GhostRiderGenEx 

            ChatGptWindow cgptWindow = new ChatGptWindow();

            //cgptWindow.Show();

        }
    }
}
