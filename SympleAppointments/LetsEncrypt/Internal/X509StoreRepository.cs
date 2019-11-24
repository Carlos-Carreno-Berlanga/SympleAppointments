// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace McMaster.AspNetCore.LetsEncrypt.Internal
{
    internal class X509StoreRepository : IFallbackCertificateRepository, ICertificateRepository, IDisposable
    {
        private readonly X509Store _x509Store;

        public X509StoreRepository(StoreName storeName, StoreLocation storeLocation)
        {
            _x509Store = new X509Store(storeName, storeLocation);
            _x509Store.Open(OpenFlags.ReadWrite);
        }

        public Task SaveAsync(X509Certificate2 certificate, CancellationToken cancellationToken)
        {
            _x509Store.Add(certificate);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _x509Store.Dispose();
        }
    }
}
