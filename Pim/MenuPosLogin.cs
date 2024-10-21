using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pim
{
    public partial class MenuPosLogin : UserControl
    {
        public MenuPosLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //nome prd
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //preco
        }

        private void btnCadPrd_Click(object sender, EventArgs e)
        {
            //btn cad prd
            AddPrd addPrd = new AddPrd();
            addPrd.Show();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            //sair
            var q = MessageBox.Show("Deseja mesmo sair", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (q == DialogResult.Yes) {
                var parentform = ParentForm as Form1;
                if (parentform != null) ;
                parentform.MudarTela(new MenuPreLogin());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //nome usuario
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectitem = listView1.SelectedItems[0];
                //pega o codigo binario da imagem que foi salvo na tag do item
                byte[] imageBytes = (byte[])selectitem.Tag;

                if (imageBytes != null) { 
                    Image img;
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        img = Image.FromStream(ms);
                    }
                    //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = img;
                }
                else {
                    MessageBox.Show("img nn encontrado");
                }

                //Acessa o nome do produto
                string nome = selectitem.SubItems[1].Text;

                // Acessa o preço
                string preco = selectitem.SubItems[2].Text;

                //adiciona os valores a sua texbox correspondente para futuras alterações
                textBox3.Text = nome;
                textBox4.Text = preco;
                label5.Text = nome;//LABEL USADA PARA IDENTIFICAR ID ATRAVES DO NOME DO PRODUTO

                //messagem de teste
                //MessageBox.Show($"você clicou no produto {nome} com o preço {preco}");

            }
        }

        //metodo para atualizar listview
        public void RcrLstVw()//recagerra/atualiza listview
        {
            //MenuPosLogin menuPosLogin = new MenuPosLogin();

            listView1.Items.Clear();
            imageList1.Images.Clear();
            Produto prd = new Produto();
            prd.CrrgPrd(this);
        }

        //------------------------------------------MENUPOSLOFIN LOAD--------------------------------------------------------------------------------
        private void MenuPosLogin_Load(object sender, EventArgs e)
        {
            label5.Visible = false;//deixa, a label usada para identificar a id do produto atraves do nome, visivel ou invisivel

            Produto produto = new Produto();
            produto.CrrgPrd(this);
            
            //panel1.Dock = DockStyle.Left;
            //panel1.Dock = DockStyle.Top;
            //panel2.Dock = DockStyle.Right;
            //panel2.Dock = DockStyle.Top;
            //panel1.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            //panel2.Anchor = AnchorStyles.Right | AnchorStyles.Top;

            panel3.Location = new Point(
                (this.ClientSize.Width - panel3.Width)/2,
                (this.ClientSize.Height - panel3.Height)/2
            );
            panel3.Anchor = AnchorStyles.None;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //panel para agrupar inf centrais
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //img do produto
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textbox para  edição do nome
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //textbox para edição do preço
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //btn editar produto

            //vai ter que ter um script bem grande nesse botao!!! pq ele vai alterar informações no banco
            CultureInfo culBR = new CultureInfo("pt-BR");
            Produto prd = new Produto();
            
            if(!float.TryParse(textBox4.Text, NumberStyles.Currency, culBR, out float val))
            {
                MessageBox.Show("O valor digitado é invalido");
                return;
            }
            int id = prd.RecuperarID(label5.Text);
            prd.AlterarPrd(id, textBox3.Text, val);
            label5.Text = textBox3.Text;
            RcrLstVw();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //gambiarra sinistra logo a baixo
            //label para o metodo alterar produto(AlterarPrd) pegar o text dassa label, ao inves do text da textbox3,
            //para assim recuperar a id da tabela Produtos e poder editalo no propria textbox 3 isso garante que o id captura seja sempre o id correto
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //btn excluir item
            Produto prd = new Produto();
            int id = prd.RecuperarID(label5.Text);
            prd.ExcPrd(id);
            RcrLstVw();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //panel2
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //panel 1
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //label nome
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RcrLstVw();
        }
    }
}
