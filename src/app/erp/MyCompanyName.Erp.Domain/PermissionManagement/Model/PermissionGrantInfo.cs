using JetBrains.Annotations;

namespace MyCompanyName.Erp.Permissions
{
    public class PermissionGrantInfo 
    {
        public static PermissionGrantInfo NonGranted { get; } = new PermissionGrantInfo(false);

        public virtual bool IsGranted { get; }

        public virtual string ProviderKey { get; }

        public PermissionGrantInfo(bool isGranted, [CanBeNull] string providerKey = null)
        {
            IsGranted = isGranted;
            ProviderKey = providerKey;
        }
    }
}