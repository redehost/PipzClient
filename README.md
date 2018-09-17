# PipzClient

## Configuração

~~~~
<add key="pipz:default-address" value="https://{{Seu-Dominio-no-Pipz}}.events.pipz.io/v1/event/"/>
<add key="pipz:tracker-api-key" value="{{Sua-Api-Key}}" />
~~~~


## Utilização

````
 public async Task AcessouMinhaPagina()
{
	//Primeiro identifique o usuário que executará a tarefa
    var user = new User
    {
        Name = "NomeDoUsuario",
        Email = foo@bar.com.br",
        JobTitle = "Developer",
        Phone = 551112345678,
        Company = new Company { Name = "RedeHost", WebSite = "redehost.com.br", RemoteId = "RedeHost" },
        UserId = "foo@bar.com.br"
    };

	//Dicionário de dados que fazem sentido para você
    var propriedades = new Dictionary<string, object>
    {
        { "propriedade1", "Test" },
        { "propriedade2", "123" }
    };

	//Chame a ação
    await _Pipz
        .Identify(user)
        .Track("acessou-minha-pagina", propriedades);
}

````