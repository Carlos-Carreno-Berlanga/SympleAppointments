// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using McMaster.AspNetCore.LetsEncrypt.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace McMaster.AspNetCore.LetsEncrypt
{
    /// <summary>
    /// Extensions for configuring certificate persistence
    /// </summary>
    public static class PersistenceExtensions
    {
        /// <summary>
        /// Save generated certificates to
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="directory"></param>
        /// <param name="pfxPassword"></param>
        /// <returns></returns>
        public static ILetsEncryptServiceBuilder PersistCertificatesToDirectory(this ILetsEncryptServiceBuilder builder, DirectoryInfo directory, string pfxPassword)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (string.IsNullOrEmpty(pfxPassword))
            {
                throw new ArgumentException("Certificate password should be non-empty.", nameof(pfxPassword));
            }

            builder.Services.AddSingleton<ICertificateRepository>(new FileSystemCertificateRepository(directory, pfxPassword));
            return builder;
        }

        /// <summary>
        /// Save certificates to the current user's certificate storage mechanism.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILetsEncryptServiceBuilder PersistCertificatesToLocalX509Store(this ILetsEncryptServiceBuilder builder)
            => builder.PersistCertificatesToLocalX509Store(StoreName.My, StoreLocation.CurrentUser);

        /// <summary>
        /// Save certificates to the specified certificate storage mechanism.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="storeName">
        /// The name of the store.
        /// By convention, HTTPS certs should be in <see cref="StoreName.My"/>
        /// </param>
        /// <param name="storeLocation">
        /// The location of the store. Normally you should use <see cref="StoreLocation.CurrentUser"/>
        /// because <see cref="StoreLocation.LocalMachine"/> typically requires admin access.
        /// </param>
        /// <returns></returns>
        public static ILetsEncryptServiceBuilder PersistCertificatesToLocalX509Store(this ILetsEncryptServiceBuilder builder,
            StoreName storeName,
            StoreLocation storeLocation)
        {
            builder.Services.AddSingleton<ICertificateRepository>(new X509StoreRepository(storeName, storeLocation));
            return builder;
        }
    }
}
