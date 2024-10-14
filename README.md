# Meu Projeto Blog MBA

Meu Projeto Blog MBA - Aplicação de Blog Simples com MVC e API RESTful


**1. Apresentação**

Bem-vindo ao repositório do projeto Meu Projeto Blog MBA. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo Introdução ao Desenvolvimento ASP.NET Core. O objetivo principal desenvolver uma aplicação de blog que permite aos usuários criar, editar, visualizar e excluir posts e comentários, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful. Descreva livremente mais detalhes do seu projeto aqui.


**Autor:** Andréia Barbosa


**2. Proposta do Projeto**

Aplicação MVC: Interface web para interação com o blog.

API RESTful: Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
Autenticação e Autorização: Implementação de controle de acesso, diferenciando administradores e usuários comuns.
Acesso a Dados: Implementação de acesso ao banco de dados através de ORM.


**3. Tecnologias Utilizadas**

Linguagem de Programação: 
C#

Frameworks:
ASP.NET Core MVC
ASP.NET Core Web API
Entity Framework Core

Banco de Dados: 
SQL Server

Autenticação e Autorização: 
ASP.NET Core Identity - Sera implementado
JWT (JSON Web Token) para autenticação na API - Sera implementado

Front-end:
Razor Pages/Views
HTML/CSS para estilização básica - Sera implementado
Documentação da API: Swagger - Sera implementado


**4. Estrutura do Projeto**
A estrutura do projeto é organizada da seguinte forma:

src/
BlogApp/ - Projeto MVC
BlogApp.Data/ - Modelos de Dados e Configuração do EF Core

README.md - Arquivo de Documentação do Projeto
FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
.gitignore - Arquivo de Ignoração do Git

**5. Funcionalidades Implementadas**
CRUD para Posts e Comentários: Permite criar, editar, visualizar e excluir posts e comentários.


**6. Como Executar o Projeto**
   
Pré-requisitos
.NET SDK 8.0 ou superior
SQL Server
Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
Git


Passos para Execução

Clone o Repositório:
git clone https://github.com/andreialuiza/meu-projeto-mba-csharp.git
cd meu-projeto-mba-csharp


Configuração do Banco de Dados:
No arquivo appsettings.json, configure a string de conexão do SQL Server.
Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

Executar a Aplicação MVC:

cd src/Blog.Mvc/
dotnet run

Acesse a aplicação em: https://localhost:7037/


**9. Avaliação**

Este projeto é parte de um curso acadêmico e não aceita contribuições externas.
O projeto ainda não está completo portanto haverá commits até a data final de entrega 04/11/2024.
Para feedbacks ou dúvidas utilize o recurso de Issues
O arquivo FEEDBACK.md é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
