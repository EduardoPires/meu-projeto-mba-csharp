# Feedback do Instrutor

#### 28/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Aguardando finalização

## Pontos Negativos:

- O projeto não está completo
- Falta uma camada core para dividir responsabilidades com a API
- O autor não está trabalhando em conjunto com o usuário do identity, ou seja, não sabemos se quem está logado representa de fato um autor.
- Não tem validação da role "Admin" apenas se o autor é dono do post
- A aplicação está muito simples, precisa evoluir e demonstrar algum domínio do ecossistema .NET

## Sugestões:

- Evoluir o projeto para as necessidades solicitadas no escopo.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
