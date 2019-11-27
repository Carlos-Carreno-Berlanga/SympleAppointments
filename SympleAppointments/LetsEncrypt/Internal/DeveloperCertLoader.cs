// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#if NETSTANDARD2_0
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;
#endif

namespace McMaster.AspNetCore.LetsEncrypt.Internal
{
    /// <summary>
    /// Loads the ASP.NET Developer certificate
    /// /// </summary>
    internal class DeveloperCertLoader : IHostedService
    {
        // see https://github.com/aspnet/Common/blob/61320f4ecc1a7b60e76ca8fe05cd86c98778f92c/shared/Microsoft.AspNetCore.Certificates.Generation.Sources/CertificateManager.cs#L19-L20
        // This is the unique OID for the developer cert generated by VS and the .NET Core CLI
        private const string AspNetHttpsOid = "1.3.6.1.4.1.311.84.1.1";
        private const string AspNetHttpsOidFriendlyName = "ASP.NET Core HTTPS development certificate";
        private readonly IHostEnvironment _environment;
        private readonly CertificateSelector _certSelector;
        private readonly ILogger<DeveloperCertLoader> _logger;

        public DeveloperCertLoader(
            IHostEnvironment environment,
            CertificateSelector certSelector,
            ILogger<DeveloperCertLoader> logger)
        {
            _environment = environment;
            _certSelector = certSelector;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_environment.IsDevelopment())
            {
                return Task.CompletedTask;
            }

            FindDeveloperCert();

            return Task.CompletedTask;
        }

        private void FindDeveloperCert()
        {
            using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindByIssuerName, "Let's Encrypt", validOnly: false);
            if (certs.Count == 0)
            {
                _logger.LogDebug("Could not find the " + AspNetHttpsOidFriendlyName);
            }
            else
            {
                _logger.LogDebug("Using the " + AspNetHttpsOidFriendlyName + " for 'localhost' requests");
                _certSelector.Use("localhost", certs[0]);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}