using System;
using System.Collections.Generic;
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
    /// Interaction logic for ChatGptWindow.xaml
    /// </summary>
    public partial class ChatGptWindow : Window
    {
        public ChatGptWindow()
        {
            InitializeComponent();
        }

        public async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var apiClient = new ChatGptApiClient("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"); // Replace with your API key
            var prompt = InputTextBox.Text;
            var response = await apiClient.GetResponseAsync(prompt);
            ResponseTextBox.Text = response;
        }

        public void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            // Copy the response text to the clipboard
            if (!string.IsNullOrEmpty(ResponseTextBox.Text))
            {
                Clipboard.SetText(ResponseTextBox.Text);
                MessageBox.Show("Code copied to clipboard!");
            }
            else
            {
                MessageBox.Show("No code to copy.");
            }
        }

        public void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            // Insert the response text into the input text box
            if (!string.IsNullOrEmpty(ResponseTextBox.Text))
            {
                InputTextBox.Text += ResponseTextBox.Text + Environment.NewLine; // Append the response
                MessageBox.Show("Code inserted into the prompt box.");
            }
            else
            {
                MessageBox.Show("No code to insert.");
            }
        }
    }


}
