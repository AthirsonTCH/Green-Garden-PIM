using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography;

namespace Pim
{
    internal class Produto
    {
        string strcon = @"Data Source=ATHIRSON-GAMER;" +
                       "Initial Catalog=GreenGardenDB;Integrated Security=True";


        string addpro = "INSERT INTO [dbo].[Produtos] (nome_produto, preco, imgPrd, estoque) " +
            "VALUES (@nome_produto , @preco, @imgPrd, @estoque)";
        string verpro = "SELECT COUNT(*) FROM [dbo].[Produtos] WHERE nome_produto = @nome_produto";

        string crrgPrd = "SELECT nome_produto, preco, imgPrd, estoque FROM Produtos";

        public int pk = -1;
        public string nomeProduto;
        public string nomeProduto2;
        public float preco;
        public byte[] imgPrd;
        public int estoque;

        public bool AddPro()//adiciona produto
        {
            if (string.IsNullOrWhiteSpace(nomeProduto))
            {
                MessageBox.Show("Nome do produto esta vazio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    using (SqlCommand cmdVer = new SqlCommand(verpro, con))
                    {
                        cmdVer.Parameters.AddWithValue("@nome_produto", nomeProduto);


                        int cont = (int)cmdVer.ExecuteScalar();
                        if (cont > 0)
                        {
                            MessageBox.Show("Já existe um produto com esse nome", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    using (SqlCommand cmdCadPro = new SqlCommand(addpro, con))
                    {
                        cmdCadPro.Parameters.AddWithValue("@nome_produto", nomeProduto);
                        cmdCadPro.Parameters.AddWithValue("@preco", preco);
                        cmdCadPro.Parameters.AddWithValue("@imgPrd", imgPrd);
                        cmdCadPro.Parameters.AddWithValue("@estoque", estoque);

                        int resultado = cmdCadPro.ExecuteNonQuery();
                        if (resultado == 1)
                            MessageBox.Show("Cadastro bem sucedido", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void CrrgPrd(MenuPosLogin menuPosLogin)//metodo usado para ler produtos do DB e adicionalos a um liestview
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    using (SqlCommand cmdCrgPrd = new SqlCommand(crrgPrd, con))
                    {
                        using (SqlDataReader reader = cmdCrgPrd.ExecuteReader())
                        {
                            int imageIdex = 0;

                            while (reader.Read())
                            {
                                //ler o campo nome_produto no banco de dados na tabela produtos
                                nomeProduto = reader["nome_produto"].ToString();

                                //ler o campo preço no banco de dados na tabela produtos
                                if (float.TryParse(reader["preco"].ToString(), out float val))
                                {
                                    preco = val;
                                    
                                }
                                string precoString = val.ToString("C");

                                //ler o campo de estoque
                                string estoqueStr = reader["estoque"].ToString();

                                //ler o codigo da imagem no banco de dados
                                byte[] imageByte = (byte[])reader["imgPrd"];
                                // Converter bytes da imagem para objeto Image
                                Image img;
                                using (MemoryStream ms = new MemoryStream(imageByte))
                                {
                                    img = Image.FromStream(ms);
                                }
                                //Adiciona a imagem ao ImageList
                                menuPosLogin.imageList1.Images.Add(img);

                                // Adiciona o item ao ListView com o nome, o preço e a imagem correspondente
                                ListViewItem item = new ListViewItem();
                                item.SubItems.Add(nomeProduto);
                                item.SubItems.Add(precoString);
                                item.SubItems.Add(estoqueStr);
                                item.ImageIndex = imageIdex;

                                menuPosLogin.listView1.Items.Add(item);
                                //adiciona o codigo binario na imagem a tag do item para que seja possivel acessar a imagem e mostrala em uma picturebox
                                item.Tag = imageByte;

                                // Incrementa o índice da imagem
                                imageIdex++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public int RecuperarID(string nomeProdutoProcurado)//recupera id do produto
        {
            try
            {
                string recId = "SELECT produto_id FROM [dbo].[Produtos] WHERE nome_produto = @nome_produto";
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    SqlCommand cmdRecId = new SqlCommand(recId, con);
                    cmdRecId.Parameters.AddWithValue("@nome_produto", nomeProdutoProcurado);
                    object resultt = cmdRecId.ExecuteScalar();

                    if (resultt == null)
                    {
                        MessageBox.Show("Produto não foi encontrado", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                    pk = Convert.ToInt32(resultt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RECUPERAR ID:{ex.Message}");
            }
            return pk;
        }

        public void AlterarPrd(int pk, string nomeProdutoAlterado, float precoAlterado, int estoqueAlterado)//altera produto
        {
            string AltPrd = "UPDATE [dbo].[Produtos] " +
                "SET nome_produto = @nome_produto, preco = @preco, estoque = @estoque " +
                "WHERE produto_id = @produto_id";

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(AltPrd, con))
                    {
                        cmd.Parameters.AddWithValue("@produto_id", pk);
                        cmd.Parameters.AddWithValue("@nome_produto", nomeProdutoAlterado);
                        cmd.Parameters.AddWithValue("@preco", precoAlterado);
                        cmd.Parameters.AddWithValue("@estoque", estoqueAlterado);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Produto alterado com sucesso");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ALTERAR PRODUTO:{ex.Message}");
            }
        }
        public void ExcPrd(int id)//exclui produto
        {
            string querryExcPrd = "DELETE FROM [dbo].[Produtos] WHERE produto_id = @id";

            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    using (SqlCommand cmdExcPrd = new SqlCommand(querryExcPrd, con))
                    {
                        cmdExcPrd.Parameters.AddWithValue("@id", id);
                        int r = cmdExcPrd.ExecuteNonQuery ();
                        if (r > 0)
                        {
                            MessageBox.Show("Produto excluido com sucesso", "Produto excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Falha ao excluir", "Falha ao excluir produto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
