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
    public partial class TelaCad : UserControl
    {
        public TelaCad()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //username
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //nome func
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //email
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //cpf
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //tel
        }

        private void textSenha_TextChanged(object sender, EventArgs e)
        {
            //senha
        }

        private void textSenhaC_TextChanged(object sender, EventArgs e)
        {
            //senha C
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //cep
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //cidade
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //bairo
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //rua
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //numero
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //btn cad
            Autenticar autenticador = new Autenticar();
            autenticador.Name = textBox1.Text;
            autenticador.Senha = textSenha.Text;
            autenticador.SenhaC = textSenhaC.Text;

            autenticador._nome_completo = textBox2.Text;
            autenticador._email_corporativo = textBox3.Text;
            autenticador._cpf = textBox4.Text;
            autenticador._tel = textBox5.Text;

            //aut do end
            autenticador._cep = textBox6.Text;
            autenticador._cidade = textBox7.Text;
            autenticador._bairro = textBox8.Text;
            autenticador._rua = textBox9.Text;
            autenticador._numero = textBox10.Text;
            
            //f aut do end
            autenticador.Cadastrar();

            var parentform = this.ParentForm as Form1;
            if (parentform != null && autenticador.CadScss)
            {
                parentform.MudarTela(new TelaLogin());
            }
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            //btn voltar
            var parentform = this.ParentForm as Form1;
            parentform.MudarTela(new MenuPreLogin());
        }

        private void TelaCad_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(
                (this.ClientSize.Width - panel1.Width) /2,
                (this.ClientSize.Height - panel1.Height)/2
            );
            panel1.Anchor = AnchorStyles.None;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //panel1 paint tela cad
        }
    }
}
