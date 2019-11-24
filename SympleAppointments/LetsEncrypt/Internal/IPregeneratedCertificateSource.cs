// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography.X509Certificates;

namespace McMaster.AspNetCore.LetsEncrypt.Internal
{
    internal interface IPregeneratedCertificateSource
    {
        X509Certificate2? GetCertificate(string domainName);
    }
}
