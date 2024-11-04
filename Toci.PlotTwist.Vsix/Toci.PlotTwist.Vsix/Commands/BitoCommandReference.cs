using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using Toci.PlotTwist.Vsix;
using Toci.PlotTwist.Wpf;
using Microsoft.VisualStudio.Shell;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using System.ComponentModel.Design;
using System.IO;


namespace Toci.PlotTwist.Vsix
{

    public class BitoCommandReference
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 256;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("22d887a7-d5c7-43e7-87dd-6c906639eb80");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommonsReference"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private BitoCommandReference(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static PlotTwist.Vsix.BitoCommandReference Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in AddCommonsReference's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new PlotTwist.Vsix.BitoCommandReference(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            Instance.Execute(sender, e);

        }

        //[Command(PackageIds.BitoCommand)]
        //public sealed class BitoCommandReference : BaseCommand<BitoCommandReference>
        //{
        //    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        //    {
        //        //await VS.MessageBox.ShowWarningAsync("TociCodeBooster", "Button clicked");

        //        //GhostRiderGenEx
        //        ChatGptWindow windowBooster = new ChatGptWindow();
        //        windowBooster.Show();

        //    }


        //}

    }
}