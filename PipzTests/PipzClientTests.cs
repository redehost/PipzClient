using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pipz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pipz.Tests
{
    [TestClass()]
    public class PipzClientTests
    {
        PipzClient _Pipz = new PipzClient();

        [TestMethod()]
        public void Should_Identify_Without_All_Fields()
        {
            var user = new User
            {
                Name = "Teste",
                Email = "teste@teste.com",
                UserId = "teste@teste.com",
                Company = new Company { Name = "RedeHost", RemoteId = "RedeHost" },
            };

            _Pipz.Identify(user);
        }

        [TestMethod()]
        public void Should_Identify_With_all_Fields()
        {
            var user = new User
            {
                Name = "Teste",
                Email = "teste@redehost.com.br",
                JobTitle = "Developer",
                Phone = "5551980000000",
                Company = new Company { Name = "RedeHost", WebSite = "redehost.com.br", RemoteId = "RedeHost" },
                UserId = "teste@redehost.com.br"
            };

            _Pipz.Identify(user);
        }

        [TestMethod()]
        public async Task Should_Track_An_Action()
        {
            var user = new User
            {
                Name = "Teste",
                Email = "teste@redehost.com.br",
                JobTitle = "Developer",
                Phone = "5551980000000",
                Company = new Company { Name = "RedeHost", WebSite = "redehost.com.br", RemoteId = "RedeHost" },
                UserId = "teste@redehost.com.br"
            };

            var propeties = new Dictionary<string, object>
            {
                { "proprety1", "Test" },
                { "proprety2", "123" }
            };

            await _Pipz
                .Identify(user)
                .Track("homologacao test", propeties);
        }
    }
}
