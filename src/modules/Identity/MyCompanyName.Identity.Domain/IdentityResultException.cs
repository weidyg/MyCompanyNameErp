using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using MyCompanyName.Identity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Localization;

namespace MyCompanyName.Identity
{
    [Serializable]
    public class IdentityResultException : BusinessException, ILocalizeErrorMessage
    {
        public IdentityResult IdentityResult { get; }

        public IdentityResultException([NotNull] IdentityResult identityResult)
            : base(
                code: $"Identity:{identityResult.Errors.First().Code}",
                message: identityResult.Errors.Select(err => err.Description).JoinAsString(", "))
        {
            IdentityResult = Check.NotNull(identityResult, nameof(identityResult));
        }

        public IdentityResultException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public virtual string LocalizeMessage(LocalizationContext context)
        {
            var localizer = context.LocalizerFactory.Create<IdentityResource>();
            SetData(localizer);
            return IdentityResult.LocalizeErrors(localizer);
        }

        protected virtual void SetData(IStringLocalizer localizer)
        {
            var values = IdentityResult.GetValuesFromErrorMessage(localizer);
            for (var index = 0; index < values.Length; index++)
            {
                Data[index.ToString()] = values[index];
            }
        }
    }
}
