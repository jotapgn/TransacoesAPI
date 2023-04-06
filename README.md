# TransacoesAPI

## :pushpin:Sobre
Web API para registro de transações monetárias de entrada e saída.

## :closed_book:Tecnologias
* .NET 6
* Entity Framework
* SQLite
* Swagger
* JWT Web Token

## :pencil:Instalação
Para utilizar esta API, você precisará ter o .NET 6 ou superior instalado na sua máquina.

Clonar o repositório abaixo:
```
$ git clone git@github.com:jotapgn/TransacoesAPI.git
```
## :pencil:Como Executar
Restaure as dependências, através do comando:
```
$ dotnet restore
```
Para rodar a API, execute o comando:
```
$ dotnet run
```
## Documentação com Swagger

A documentação da API pode ser encontrada em https://localhost:7200/swagger. Através dela é possível visualizar os endpoints disponíveis e testar cada rota.

## Autenticação

Para acessar as rotas de transações da API é preciso realizar autenticação, é necessário enviar um token Bearer no header da requisição. Para gerar um token, faça uma requisição POST para o endpoint `/api/login` com as credenciais de um usuário válido.

A resposta da requisição será um token Bearer que deve ser utilizado para acessar as rotas protegidas. Adicione o token no header da requisição da seguinte forma:
Authorization: Bearer < Token >
