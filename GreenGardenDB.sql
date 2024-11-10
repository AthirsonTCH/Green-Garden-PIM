CREATE DATABASE GreenGardenDB

USE GreenGardenDB

CREATE TABLE Endereco(
endereco_id INT PRIMARY KEY IDENTITY(1,1),
cep CHAR(9),
cidade VARCHAR(35),
bairro VARCHAR(35),
rua VARCHAR(35),
numero VARCHAR(10)
);

CREATE TABLE Funcionario(
funcionario_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome_usuario VARCHAR(150) NOT NULL,
nome_completo VARCHAR(150),
email_corporativo VARCHAR(150),
tel VARCHAR(15),
cpf VARCHAR(11),
endereco_id INT NOT NULL,
senha VARCHAR(25) NOT NULL 
FOREIGN KEY (endereco_id) REFERENCES Endereco(endereco_id)
);

CREATE TABLE Produtos (
produto_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome_produto VARCHAR(60),
preco DECIMAL(10,2),
imgPrd VARBINARY(MAX),
estoque int
);

--<<exibir>>
SELECT * FROM Produtos
select * from Endereco
select * from Funcionario
--<</exibir>>