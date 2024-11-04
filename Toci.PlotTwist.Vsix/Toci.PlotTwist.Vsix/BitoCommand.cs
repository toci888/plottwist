using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Toci.PlotTwist.Wpf;

namespace Toci.PlotTwist.Vsix
{
    internal sealed class BitoCommand
    {
        public const int CommandId = 0x0100; // Command ID
        public static readonly Guid CommandSet = new Guid("your-command-set-guid"); // Replace with your GUID

        private readonly AsyncPackage package;

        private BitoCommand(AsyncPackage package)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));

            // Register the command
            var commandService = this.package.GetService<OleMenuCommandService, IMenuCommandService>(true) as OleMenuCommandService;
            var menuCommandID = new System.ComponentModel.Design.CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            new BitoCommand(package);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ShowChatGptWindow();
        }

        private void ShowChatGptWindow()
        {
            // Show the ChatGPT window
            var chatGptWindow = new ChatGptWindow();
            chatGptWindow.Show();
        }
    }
}