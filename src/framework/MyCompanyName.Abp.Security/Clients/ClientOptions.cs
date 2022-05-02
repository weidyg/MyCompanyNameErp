using System.Collections.Generic;

namespace MyCompanyName.Abp.Security
{
    public class ClientOptions
    {
        public ClientOptions()
        {
            ClientTypeMap = new Dictionary<string, ClientType>();
        }

        public Dictionary<string, ClientType> ClientTypeMap { get; }

        public ClientType? GetClientTypeOrNull(string clientId)
        {
            var resulr = ClientTypeMap.TryGetValue(clientId, out ClientType clientType);
            if (resulr) { return clientType; }
            return null;
        }
        public string GetClientTypeString(string clientId)
        {
            var resulr = ClientTypeMap.TryGetValue(clientId, out ClientType clientType);
            if (resulr) { return clientType.ToString(); }
            return null;
        }
    }

    public enum ClientType
    {
        EFX = 100,
        ERP = 1000,
    }
}
