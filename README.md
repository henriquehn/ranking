# Teste com WEBAPI: Ranking
Esse projeto foi desenvolvido como um teste utilizando WebAPI com o .Net Framework.
## Decisões de projeto
* Por questão de famiiaridade, optou-se pela utilização do Visual Basic.Net para o desenvolvimento do teste;
* O projeto foi dividido em 8 pastas principais:
** Controllers: Contém os controllers que respondem pelas requisições da API;
** Data: Contém as entidades responsáveis pela manipulação de dados (DAOs, DataSourceAdapters, etc.);
** DataModels: Contém os modelos de entidades correspondentes às tabelas do banco de dados;
** DefaultComparers: COntém as entidades responsáveis pela comparação de DataModels do mesmo tipo
** Extensions: Contém as entidades que definem métodos de extensão para facilitar tarefas comuns que se repetem com frequência
** Interfaces: COntém as interfaces que definem estruturas genéricas que deverão ser implementadas por classes especializadas
** lib: Contém as bibliotecas do DOD Framework para manipulação de fontes de dados. O DOD Framework foi criado como resultado da minha dissertação de mestrado.
** Models: cContém os modelos de entidades utilizados na interface da API com o usuário final
* Foi incluído um projeto de testes que contém uma classe para testar todas as funcionalidades da API;
* As configurações de bancos de dados foram colocadas nos arquivos Web.Config (no projeto principal) e App.Config (no projeto de testes);
## Estrutura da API
A API contém apenas um tipo de entidade e foram criados dois métodos para manipulação dos dados:
* GetValues: responde ao método GET pelo endereço "/api/ranking/" e retorna um JSON contendo todos os elementos existentes no banco de dados.
* PostValue: responde ao método POST pelo endereço "/api/ranking/" e recebe um JSON contendo um elemento para ser armazenado no banco de dados.
## Formato das requisições
Para fazer uma consulta na API, deve ser enviada uma requisição do tipo GET para o endereço [/api/ranking/](/api/ranking/). A resposta será um JSON com o seguinte formato:
```json
[
{
"posicao": 1,
"nome": "Aluno1",
"pontos": 30
},
{
"posicao": 2,
"nome": "Aluno2",
"pontos": 29
}
]
```

Para enviar um item para a API, deve ser enviada uma requisição do tipo POST para o endereço [/api/ranking/](/api/ranking/), passando como argumento um JSON com o seguinte formato:
```json
{
"nome": "Nome do Aluno",
"pontos": 40
}
```

Em ambos os casos, os dados são apenas exemplos, porém a estrutura é obrigatória.
## Configuração do ambiente de execução.
A configuração do ambiente é relativamente simples e consiste na criação do banco de dados e na configuração das credenciais de acesso.
É recomendavel que se use a autenticação integrada para evitar que credenciais sejam xplicitamente armazenadas.

### Criação do banco de dados.
Para a criação do banco de dados, foi incluído no repositório o script de configuração, conforme mostrado abaixo:

```sql
USE [master]
GO
CREATE DATABASE Ranking
GO
USE Ranking
GO
CREATE TABLE Alunos(id int IDENTITY(1,1) NOT NULL primary key, Nome varchar(50) NOT NULL, Pontos int NOT NULL)
GO
}
```

### Configuração do projeto (Web.config e App.Config)
Tanto no Web.config quanto no App.config, o primeiro elemento do bloco <configuration> deverá ser <configSections>, onde deverá ser definido o nome da seção que conterá a configuração do banco de dados.
No exemplo abaixo, o nome da seção declarada é "BancoSQLServer".

```xml
<configSections>
	<!-- Referência para uso do SQL Server -->
	<section name="BancoSQLServer" type="DataLibrary.DAO.Specialized.SQL.SqlSettings, DataLibrary"/>
</configSections>
```

Uma vez feita essa configuração, poderá ser criada em qualquer parte da raíz do bloco <configuration> (posterior ao bloco <configSections>) um bloco com o nome especificado, conforme o exemplo abaixo:

```xml
<!-- Seção para tratamento dos bancos de dados, conforme nomes definido nas referências acima -->
<BancoSQLServer integratedsecurity="true" server="(local)\SQLEXPRESS" database="ranking"/>
```

Neste exemplo, está sendo usada autenticação integrada para uma instância do SQL Server chamada "SQLEXPRESS" que está hospedada na máquina local e contém um banco de dados chamado "ranking" (criado com o script que já foi mostrado neste documento).

Para concluir a configuração é necessário editar "\Data\Adapters\DefaultSqlAdapter.vb" que contém o seguinte código:

```vb
Public Class DefaultSqlAdapter
	Inherits SqlDatabaseAdapter
	Public Sub New()
		MyBase.New("BancoSQLServer")
	End Sub
End Class
```

Conforme pode ser observado no exemplo, o nome da seção que foi declarada deve ser passado como argumento no construtor da classe base.

## Execução do projeto
Uma vez que tenham sido feitas as configurações iniciais (incluindo o banco de dados), o projeto Ranking deve ser definido como projeto inicial. Após executar o programa, o navegador será aberto com a URL raíz e uma página de erro será exibida coma mensagem "HTTP Error 403.14 - Forbidden".
Na URL que aparece, deve ser acrscentado o endereço "/api/Ranking" e um JSON já aparecerá como resposta. Caso o banco de dados esteja vazio, aparecerá apenas "[]". Este resultado já é uma resposta para o método GET.

Para testar o método POT, poderá ser feita uma requisição manual usando um aplicativo de TELNET ou ainda uma ferramenta de testes como o [Insomnia](https://insomnia.rest/download/).

## Execução dos testes
Para acompanhar a execução dos testes, a janela "Test Explorer" deve estar visível. Para isso deve ser utilizado o menu "Test\Windows\Test Explorer"
Para testar a API, com o projeto aberto no, deve-se clicar no menu "Test => Run => All Tests".

As instruções acima foram elaboradas com base no Visual Studio Community 2017. As opções de menu podem variar ligeiramente em outras versões. Estas opções não estão disponíveis no Visual Studio code.


