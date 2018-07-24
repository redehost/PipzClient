using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pipz.Models;
using System.Collections.Generic;

namespace Pipz.Tests
{
	[TestClass()]
	public class RestClientTests
	{
		PipzClient _Pipz = new PipzClient();

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
				Name = "Fabio Junior",
				Email = "fabio.junior@redehost.com.br",
				JobTitle = "Developer",
				Phone = 5551982599714,
				Company = new Company { Name = "RedeHost", WebSite = "redehost.com.br", RemoteId = "RedeHost" },
				UserId = "fabio.junior@redehost.com.br"
			};

			var propeties = new Dictionary<string, string>();
			propeties.Add("proprety1", "Test");
			propeties.Add("proprety2", "123");

			_Pipz.Track("homologacao" ,propeties, user);
		}
	}
}
