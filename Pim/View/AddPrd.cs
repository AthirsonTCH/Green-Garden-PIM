using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pim
{
    public partial class AddPrd : Form
    {
        public AddPrd()
        {
            InitializeComponent();
        }

        Produto produto = new Produto();

        private void pictureBox1_Click(object sender, EventArgs e)//metodo acionado quando se clica na picturebox
        {
            //click picture box
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Title = "Selecione a Imagem do produto";

            // Verificar se o usuário selecionou o arquivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName; // Caminho do arquivo de imagem

                // Carregar a imagem
                Image selectImage = Image.FromFile(imagePath);

                //Carrega img na picture box para visualização
                pictureBox1.Image = selectImage;

                // Converter a imagem em array de bytes
                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    selectImage.Save(ms, selectImage.RawFormat);
                    imageBytes = ms.ToArray();
                }
                produto.imgPrd = imageBytes;
            }
            else
            {
                MessageBox.Show("Imagem não foi Definida", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textbox nome
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //textbox preço 
        }

        private void btnCadPrd_Click(object sender, EventArgs e)
        {
            //btn cadastrar
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("O produto está sem o nome", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            produto.nomeProduto = textBox1.Text;

            //Verifica se o preço é valido
            if (float.TryParse(textBox2.Text, out float val))
            {
                produto.preco = val;
            }
            else
            {
                MessageBox.Show("O Preço digitado é invalido", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //verifica se a imagem foi selecionada
            if (produto.imgPrd == null) {
                MessageBox.Show("Imagem não foi Definida", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //verifica se o estoque é valido
            if(int.TryParse(textBox3.Text, out int valint))
            {
                produto.estoque = valint;
            }
            else
            {
                MessageBox.Show("O valor no campo estoque é inválido", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //adicionar produto
            if (!produto.AddPro())
            {
                return;
            }
            this.Close();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //texbox estoque
        }

        

        private void AddPrd_Load(object sender, EventArgs e)
        {

        }
    }
}
