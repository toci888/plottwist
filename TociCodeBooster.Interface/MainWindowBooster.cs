using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TociCodeBooster.Interface
{
    public partial class MainWindowBooster : Form
    {
        public MainWindowBooster()
        {
            InitializeComponent();

            Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogicBaseGenerator logicBaseGenerator = new LogicBaseGenerator();

            logicBaseGenerator.Show();
        }
    }
}
