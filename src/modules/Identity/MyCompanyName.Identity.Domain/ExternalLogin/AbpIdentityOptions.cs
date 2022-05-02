namespace MyCompanyName.Identity
{
    public class IdentityExternalOptions
    {
        public ExternalLoginProviderDictionary ExternalLoginProviders { get; }

        public IdentityExternalOptions()
        {
            ExternalLoginProviders = new ExternalLoginProviderDictionary();
        }
    }
}
