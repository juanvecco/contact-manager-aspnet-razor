# Gerenciador de contatos

Aplicação web em ASP.NET Core Razor Pages, .NET 6, Entity Framework Core e MariaDB 10.6.

Ambiente publicado para avaliação: <https://ramonjuan-dot.recruitment.alfasoft.pt>

## Funcionalidades

- Listar contatos ativos.
- Cadastrar contato com nome, e-mail e número de contato.
- Consultar os detalhes de um contato.
- Editar um contato.
- Excluir logicamente um contato. O registro permanece no banco com `IsDeleted = true` e deixa de aparecer nas consultas normais.
- Validar campos obrigatórios, formato de e-mail, número com exatamente nove dígitos e unicidade de e-mail e número.

## Requisitos

- .NET 6 SDK.
- MariaDB 10.6 acessível pela aplicação.

## Configurar a conexão com o banco

A aplicação espera uma connection string chamada `MariaDB`. Configure-a fora do controle de versão, por exemplo, com User Secrets no ambiente local:

```powershell
dotnet user-secrets set "ConnectionStrings:MariaDB" "Server=localhost;Port=3306;Database=ContactManager;Uid=SEU_USUARIO;Pwd=SUA_SENHA;"
```

Substitua `SEU_USUARIO` e `SUA_SENHA` pelos dados do seu banco. No servidor, a configuração pode ser fornecida pela variável de ambiente `ConnectionStrings__MariaDB` ou por um arquivo de configuração de produção que não seja enviado ao Git.

Na inicialização, a aplicação executa as migrações do Entity Framework Core para criar ou atualizar a estrutura do banco. A conexão precisa estar disponível e o usuário do banco precisa ter as permissões necessárias.

## Executar localmente

Na pasta do projeto:

```powershell
dotnet restore
dotnet run
```

Abra o endereço local informado pelo comando e acesse a opção **Contatos**.

## Publicar

Para gerar os arquivos de publicação:

```powershell
dotnet publish ContactManager.csproj --configuration Release --output ./publish
```

Configure a connection string de produção no ambiente de hospedagem antes de iniciar a aplicação. Após publicar uma versão no ambiente de avaliação, solicite o reinício do serviço conforme as instruções fornecidas pela hospedagem.
