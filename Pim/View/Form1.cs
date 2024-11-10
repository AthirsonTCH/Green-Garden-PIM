using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            MudarTela(new MenuPreLogin());
        }
        public void MudarTela(UserControl newTel)
        {
            this.Controls.Clear();
            newTel.Dock = DockStyle.Fill;
            this.Controls.Add(newTel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // form1 load
        }
    }
}
