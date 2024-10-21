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
    public partial class MenuPreLogin : UserControl
    {
        public MenuPreLogin()
        {
            InitializeComponent();
        }

        private void MenuPreLogin_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(
                (this.ClientSize.Width - panel1.Width) / 2, // Calcula a posição X central
                (this.ClientSize.Height - panel1.Height)/ 2 // Calcula a posição Y central
            );
            panel1.Anchor = AnchorStyles.None;
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            //btnlogar
            var parentform = this.ParentForm as Form1;
            if (parentform != null)
            {
                parentform.MudarTela(new TelaLogin());
            }
        }

        private void btnCad_Click(object sender, EventArgs e)
        {
            //btn cad
            var parentform = this.ParentForm as Form1;
            if (parentform != null)
            {
                parentform.MudarTela(new TelaCad());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btn sair
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
