using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pim
{
    internal class Autenticar
    {
        public string strcon = @"Data Source=NOME_SERVIDOR;" +
                        "Initial Catalog=GreenGardenDB;Integrated Security=True";

        string strcadastrar = "INSERT INTO [dbo].[Funcionario] (nome_usuario, senha, nome_completo, email_corporativo, tel, cpf, endereco_id)" +
            "VALUES (@nome, @senha, @nome_completo, @email_corporativo, @tel, @cpf, @endereco_id)";
        String strVer = " SELECT COUNT(*) FROM [dbo].[Funcionario] WHERE nome_usuario  COLLATE SQL_Latin1_General_CP1_CS_AS = @nome";
        string dfnEnd = "INSERT INTO [dbo].[Endereco] (cep,cidade,bairro,rua,numero) " +
            "VALUES (@cep ,@cidade ,@bairro ,@rua ,@numero)";

        string strLog = "SELECT * FROM [dbo].[Funcionario] WHERE nome_usuario COLLATE SQL_Latin1_General_CP1_CS_AS = @nome AND senha COLLATE SQL_Latin1_General_CP1_CS_AS = @senha";
        

        private string _name;
        private string _senha;
        private string _senhaC;

        public string _nome_completo;
        public string _email_corporativo;
        public string _tel;
        public string _cpf;

        public string _cep;
        public string _cidade;
        public string _bairro;
        public string _rua;
        public string _numero;


        private bool _estLgd = false; //LOGIN BEM SUCEDIDO OU MAL SUCEDIDO
        private bool _cadScss = false; // CAD BEM SUCEDIDO OU MAL SUCEDIDO

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Senha
        {
            get
            {
                return _senha;
            }
            set
            {
                _senha = value;
            }
        }
        public string SenhaC
        {
            get
            {
                return _senhaC;
            }
            set
            {
                _senhaC = value;
            }
        }
        public bool EstLgd
        {
            get { return _estLgd; }
            set { _estLgd = value; }
        }
        public bool CadScss
        {
            get { return _cadScss; }
            set { _cadScss = value; }
        }

        public void Cadastrar()
        {
            Form1 telaCad = new Form1();
            if (_senha != _senhaC)
            {
                MessageBox.Show("As Senhas são diferentes", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Campo username está vazio", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (string.IsNullOrWhiteSpace(Senha))
            {
                MessageBox.Show("O campo Senha está vazio", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    using (SqlCommand ver = new SqlCommand(strVer, con))
                    {
                        ver.Parameters.AddWithValue("@nome", Name);

                        int count = (int)ver.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("O Usuario ja existe, tente novamente", "Cadastro", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    int endereco_id;

                    using (SqlCommand cmdCad = new SqlCommand(dfnEnd, con))
                    {
                        cmdCad.Parameters.AddWithValue("@cep ", _cep);
                        cmdCad.Parameters.AddWithValue("@cidade ", _cidade);
                        cmdCad.Parameters.AddWithValue("@bairro ", _bairro);
                        cmdCad.Parameters.AddWithValue("@rua ", _rua);
                        cmdCad.Parameters.AddWithValue("@numero ", _numero);

                        // Executa a inserção do endereço e retorna o endereço_id gerado
                        cmdCad.CommandText += ";SELECT SCOPE_IDENTITY();";// Recupera o ID gerado
                        endereco_id = Convert.ToInt32(cmdCad.ExecuteScalar()); //Captura o endereco_id gerado


                    }

                    using (SqlCommand cmd = new SqlCommand(strcadastrar, con))
                    {

                        cmd.Parameters.AddWithValue("@nome", Name);
                        cmd.Parameters.AddWithValue("@senha", Senha);

                        cmd.Parameters.AddWithValue("@nome_completo", _nome_completo);
                        cmd.Parameters.AddWithValue("@email_corporativo", _email_corporativo);
                        cmd.Parameters.AddWithValue("@tel", _tel);
                        cmd.Parameters.AddWithValue("@cpf", _cpf);
                        cmd.Parameters.AddWithValue("@endereco_id", endereco_id);

                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado == 1)
                        {
                            MessageBox.Show("Cadastro concluido", "cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _cadScss = true;
                        }
                        else
                        {
                            MessageBox.Show("gravação mal sucedida", "cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERRO:{ex.Message}", "Erro", MessageBoxButtons.OK);
            }
        }

        public void Login()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    using (SqlCommand cmdLog = new SqlCommand(strLog, con))
                    {
                        cmdLog.Parameters.AddWithValue("@nome", Name);
                        cmdLog.Parameters.AddWithValue("@senha", Senha);

                        using (SqlDataReader reader = cmdLog.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MessageBox.Show("Usuario logado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _estLgd = true;
                            }
                            else
                            {
                                MessageBox.Show("Usuario ou senha estão incorretos", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERRO:{ex.Message}", "Erro", MessageBoxButtons.OK);
            }
        }
    }
}
