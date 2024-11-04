using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Toci.PlotTwist.Vsix
{
    internal sealed class ChatGptCommand
    {
        public const int CommandId = 0x0100; // Command ID
        public static readonly Guid CommandSet = new Guid("your-command-set-guid"); // Replace with your GUID

        private readonly AsyncPackage package;

        private MyCommand(AsyncPackage package)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));

            // Register the command
            var commandService = this.package.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            new MyCommand(package);
        }

        private void ExecuteCreateClass(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // Get the DTE object
            var dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));

            // Prompt user for class name
            string className = PromptForClassName();
            if (string.IsNullOrEmpty(className)) return;

            // Create an instance of FileGenerator and create the class file
            var fileGenerator = new FileGenerator(dte);
            fileGenerator.CreateClassFile(className);
        }

        private string PromptForClassName()
        {
            // Use a simple input dialog to get the class name from the user
            string className = Microsoft.VisualBasic.Interaction.InputBox("Enter the class name:", "Class Name", "MyClass");
            return className;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ShowMyPluginWindow();
        }

        private void ShowMyPluginWindow()
        {
            // Show the plugin window
            var window = new MyPluginWindowHost();
            window.Show();
        }
    }
}