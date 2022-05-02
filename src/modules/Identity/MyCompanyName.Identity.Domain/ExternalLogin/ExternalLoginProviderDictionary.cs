using JetBrains.Annotations;
using System.Collections.Generic;

namespace MyCompanyName.Identity
{
    public class ExternalLoginProviderDictionary : Dictionary<string, ExternalLoginProviderInfo>
    {
        /// <summary>
        /// Adds or replaces a provider.
        /// </summary>
        public void Add<TProvider>([NotNull] string name)
            where TProvider : IExternalLoginProvider
        {
            this[name] = new ExternalLoginProviderInfo(name, typeof(TProvider));
        }
    }
}