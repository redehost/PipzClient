using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pipz.Models;
using System.Collections.Generic;

namespace Pipz.Tests
{
    [TestClass()]
    public class PipzClientTests
    {
        PipzClient _Pipz = new PipzClient();

        [TestMethod()]
        public void Sould_Identify_Without_All_Fields()
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
        public void IdentifyTest()
        {
            var user = new User
            {
                Name = "Fabio Junior",
                Email = "fabio.junior@redehost.com.br",
                JobTitle = "Developer",
                Phone = 5551982599714,
                Company = new Company { Name = "RedeHost", WebSite = "redehost.com.br", RemoteId = "RedeHost" },
                UserId = "fabio.junior@redehost.com.br"
            };

            _Pipz.Identify(user);
        }

        [TestMethod()]
        public void Track()
        {
            var user = new User
            {
                Name = "Pablo",
                Email = "pablo.feijo@redehost.com.br",
                JobTitle = "Developer",
                Phone = 5551982599714,
                Company = new Company { Name = "RedeHost", WebSite = "redehost.com.br", RemoteId = "RedeHost" },
                UserId = "pablo.feijo@redehost.com.br"
            };

            var propeties = new Dictionary<string, object>();
            propeties.Add("proprety1", "Test");
            propeties.Add("proprety2", "123");

            _Pipz.Track("homologacao test", propeties, user);
        }
    }
}
