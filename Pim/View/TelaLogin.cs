using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pim
{
    public partial class TelaLogin : UserControl
    {
        public TelaLogin()
        {
            InitializeComponent();
        }

        private void textUsernameLog_TextChanged(object sender, EventArgs e)
        {
            //username
        }

        private void textSenhaLog_TextChanged(object sender, EventArgs e)
        {
            //senha
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            //btn logar
            Autenticar autenticador = new Autenticar();

            autenticador.Name = textUsernameLog.Text;
            autenticador.Senha = textSenhaLog.Text;
            autenticador.Login();

            if (autenticador.EstLgd)
            {
                MenuPosLogin menuPosLogin = new MenuPosLogin();
                menuPosLogin.label3.Text = autenticador.Name;

                var parentform = this.ParentForm as Form1;
                if (parentform != null)// && autenticador.EstLgd == true)
                {
                    parentform.MudarTela(menuPosLogin);

                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            //btn voltar
            var parentform = this.ParentForm as Form1;
            if (parentform != null)
            {
                parentform.MudarTela(new MenuPreLogin());
            }
        }

        private void TelaLogin_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point(
                (this.ClientSize.Width - panel1.Width)/2,
                (this.ClientSize.Height - panel1.Height)/2
            );
            panel1.Anchor = AnchorStyles.None;

            this.Paint += new PaintEventHandler(TelaLogin_Paint);
            this.Resize += new EventHandler(TelaLogin_Resize);

        }

        private void panel1_Load(object sender, EventArgs e)
        {
            //panel load
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //*panel1
        }
        private void TelaLogin_Paint(object sender, PaintEventArgs e)
        {
            // Carrega a imagem (certifique-se de ajustar o caminho correto
            string imgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Content\IMG\Green Garden background.jpg");
            if (!System.IO.File.Exists(imgPath))
            {
                MessageBox.Show("Imagem não encontrada no caminho especificado.");
                return;
            }
            // Obtém o tamanho do formulário ou controle
            using (Image imgOriginal = Image.FromFile(imgPath))
            {
                int altura = this.ClientSize.Height;
                int comprimento = this.ClientSize.Width;

                // Desenha a imagem ajustada ao tamanho do formulário
                e.Graphics.DrawImage(imgOriginal, 0, 0, comprimento, altura);
            }

            // Desenha o painel simulando transparência
           // DrawPanelSimulatedTransparency(e.Graphics, panel1);
        }


        private void TelaLogin_Resize(object sender, EventArgs e)
        {
            // Força a repintura do formulário ao redimensionar
            this.Invalidate();
        }

        private void DrawPanelSimulatedTransparency(Graphics g, Panel panel)// Desenha o painel simulando transparência

        {
            /* Salva o estado original para restaurar depois
            var state = g.Save();

            // Define a região para desenhar dentro do painel
            g.SetClip(panel.Bounds);

            // Desenha o painel com uma leve cor opaca
            g.FillRectangle(new SolidBrush(Color.FromArgb(150, panel.BackColor)), panel.Bounds);

            // Restaura o estado original
            g.Restore(state);*/
        }

    }
}
