using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace MyCompanyName.Identity
{
    public static class ClaimsIdentityExtensions
    {
        public static void AddClaimIfNotContains(this ClaimsIdentity identity, string type, string value)
        {
            if (!value.IsNullOrWhiteSpace()) { identity.AddIfNotContains(new Claim(type, value)); }
        }


        public static void AddClaimIfNotContains(this ClaimsIdentity identity, string type, IEnumerable<string> values)
        {
            foreach (var value in values) { identity.AddClaimIfNotContains(type, value); }
        }

        public static void AddClaimIfNotContains(this ClaimsIdentity identity, Claim claim)
        {
            if (!claim.Value.IsNullOrWhiteSpace()) { identity.AddIfNotContains(claim); }
        }


        public static void AddIfNotContains(this ICollection<Claim> source, string type, IEnumerable<string> values)
        {
            foreach (var value in values) { source.AddIfNotContains(type, value); }
        }

        public static void AddIfNotContains(this ICollection<Claim> source, string type, string value)
        {
            if (!value.IsNullOrWhiteSpace()) { source.AddIfNotContains(new Claim(type, value)); }
        }

        public static void Add(this ICollection<Claim> source, string type, IEnumerable<string> values)
        {
            foreach (var value in values) { source.Add(type, value); }
        }

        public static void Add(this ICollection<Claim> source, string type, string value)
        {
            if (!value.IsNullOrWhiteSpace()) { source.Add(new Claim(type, value)); }
        }
    }
}
