using Pipz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pipz
{
    public interface IPipzClient
    {
        PipzClient Identify(User user);
        Task Track(string eventName, Dictionary<string, object> properties);
        Task Track(string eventName);
    }
}
