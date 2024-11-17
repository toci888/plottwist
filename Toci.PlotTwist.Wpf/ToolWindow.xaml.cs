using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Toci.PlotTwist.Wpf
{
    /// <summary>
    /// Interaction logic for ToolWindow.xaml
    /// </summary>
    public partial class ToolWindow : UserControl
    {
        public ToolWindow()
        {
            InitializeComponent();
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(OutputBox.Text);
        }

        private async void Execute(object sender, EventArgs e)
        {
          //await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var client = new ChatGptApiClient("YOUR_API_KEY");
            string code = await client.GenerateCodeAsync("Generate a function that calculates Fibonacci numbers in C#.");

            // Display code in the ToolWindow
            //var window = await package.FindToolWindowAsync(typeof(ToolWindow), 0, true, package.DisposalToken);
            //(window.Content as ToolWindowControl).OutputBox.Text = code;
        }
    }
}
