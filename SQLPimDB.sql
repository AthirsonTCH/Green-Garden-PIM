CREATE DATABASE GreenGarden

USE GreenGarden

--DELETA TODOS OS ITENS DA TABELA
DELETE FROM Endereco;

--Resetar a contagem das PK's começar do 1 novamente:
DBCC CHECKIDENT (Produtos, RESEED, 0);

--<<exibir>>
SELECT * FROM Produtos
select * from Endereco
select * from Funcionario
--<</exibir>>

CREATE TABLE Endereco(
endereco_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
cep CHAR(9)NOT NULL,
cidade VARCHAR(35)NOT NULL,
bairro VARCHAR(35)NOT NULL,
rua VARCHAR(35)NOT NULL,
numero INT
);
--<<alteraçoes>>
ALTER TABLE Endereco
ALTER COLUMN numero VARCHAR(10);
--<</alteraçoes>>

CREATE TABLE Funcionario(
funcionario_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome_usuario VARCHAR(150),
nome_completo VARCHAR(150)NOT NULL,
email_corporativo VARCHAR(150),
tel VARCHAR(15),
cpf VARCHAR(11)NOT NULL,
endereco_id INT NOT NULL,
FOREIGN KEY (endereco_id) REFERENCES Endereco(endereco_id)
);
--<<exibir>>
SELECT * FROM Funcionario
--<</exibir>>
--<<alteraçoes
ALTER TABLE Funcionario
ADD senha VARCHAR(25) NOT NULL
--<</alteraçoes>>

CREATE TABLE CadastroCliente (
cadastro_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome_completo VARCHAR(150) NOT NULL,
nome_usuario VARCHAR(150),
email VARCHAR(150) NOT NULL,
data_nasc DATE,
tel VARCHAR(15),
cpf VARCHAR(11) NOT NULL,
endereco_id INT,
FOREIGN KEY (endereco_id) REFERENCES Endereco(endereco_id)
);
--<<alteraçoes
ALTER TABLE CadastroCliente
ADD senha VARCHAR(25) NOT NULL
--<</alteraçoes>>

CREATE TABLE Fornecedor(
fornecedor_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome VARCHAR (150)NOT NULL,
tel VARCHAR(15)NOT NULL,
email VARCHAR(150)NOT NULL,
endereco_id INT NOT NULL,
FOREIGN KEY (endereco_id) REFERENCES Endereco(endereco_id)
);

CREATE TABLE ProdutosFornecedor(
produto_fornecedor_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
produtos_fornecidos VARCHAR (60)NOT NULL,
preco DECIMAL (10,2) NOT NULL,
fornecedor_id INT NOT NULL,
FOREIGN KEY (fornecedor_id) REFERENCES Fornecedor(fornecedor_id)
);

CREATE TABLE Produtos (
produto_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome_produto VARCHAR(60),
preco DECIMAL(10,2)
);
--<<alteraçoes
ALTER TABLE Produtos
ADD imagePorduto VARBINARY(MAX)
EXEC sp_rename 'Produtos.imagePorduto' , 'imgPrd', 'COLUMN';

alter table Produtos
add estoque int
--<</alteraçoes>>

CREATE TABLE Compras(
compra_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
quantidade INT,
total DECIMAL (10,2)
);

CREATE TABLE ComprasDoFornecedor(
compras_do_fornecedor_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
data_compra DATE,
produto_fornecedor_id INT,
compra_id INT,
FOREIGN KEY (produto_fornecedor_id) REFERENCES ProdutosFornecedor (produto_fornecedor_id),
FOREIGN KEY (compra_id) REFERENCES Compras (compra_id)
);

CREATE TABLE Vendas (
venda_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
quantidade INT,
total DECIMAL(10,2),
cadastro_id INT,
FOREIGN KEY (cadastro_id) REFERENCES CadastroCliente (cadastro_id)
);

CREATE TABLE Pagamentos (
pagamento_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
data_venda DATE,
entrega_em_casa BIT NOT NULL,
venda_id INT NOT NULL,
produto_id INT NOT NULL,
FOREIGN KEY (venda_id) REFERENCES Vendas(venda_id),
FOREIGN KEY (produto_id) REFERENCES Produtos (produto_id)
);