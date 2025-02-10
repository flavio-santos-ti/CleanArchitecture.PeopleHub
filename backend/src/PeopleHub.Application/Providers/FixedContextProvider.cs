using PeopleHub.Application.Interfaces.Common;

namespace PeopleHub.Application.Providers
{
    public class FixedContextProvider : IContextProvider
    {
        public string ContextName { get; }

        public FixedContextProvider(string contextName)
        {
            ContextName = contextName;
        }
    }
}
