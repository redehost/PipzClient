# PipzClient

## Configura��o

~~~~
<add key="pipz:default-address" value="https://{{Seu-Dominio-no-Pipz}}.events.pipz.io/v1/event/"/>
<add key="pipz:tracker-api-key" value="{{Sua-Api-Key}}" />
~~~~


## Utiliza��o

````
 public async Task AcessouMinhaPagina()
{
	//Primeiro identifique o usu�rio que executar� a tarefa
    var user = new User
    {
        Name = "NomeDoUsuario",
        Email = foo@bar.com.br",
        JobTitle = "Developer",
        Phone = 551112345678,
        Company = new Company { Name = "RedeHost", WebSite = "redehost.com.br", RemoteId = "RedeHost" },
        UserId = "foo@bar.com.br"
    };

	//Dicion�rio de dados que fazem sentido para voc�
    var propriedades = new Dictionary<string, object>
    {
        { "propriedade1", "Test" },
        { "propriedade2", "123" }
    };

	//Chame a a��o
    await _Pipz
        .Identify(user)
        .Track("acessou-minha-pagina", propriedades);
}

````