using BuildCustomPolicy.LocalAccounts;
using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace BuildCustomPolicy
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<PolicyOptions>(args)
                .WithParsedAsync(async o =>
                {
                    await ProcessPolicyFilesAsync(o);
                }).Wait();
        }


        private static async Task ProcessPolicyFilesAsync(PolicyOptions options)
        {
            var path = await InitOutputDirAsync(options);
            await ProcessPolicyFileAsync(PolicyResources.TrustFrameworkBase, options, path, "01-TrustFrameworkBase.xml");
            await ProcessPolicyFileAsync(PolicyResources.TrustFrameworkExtensions_Common, options, path, "02-TrustFrameworkExtensions.xml");
            await ProcessPolicyFileAsync(PolicyResources.SignUpOrSignin, options, path, "03-SignUpOrSignin.xml");
            await ProcessPolicyFileAsync(PolicyResources.PasswordReset, options, path, "04-PasswordReset.xml");
            await ProcessPolicyFileAsync(PolicyResources.ProfileEdit, options, path, "05-ProfileEdit.xml");
        }

        private static async Task<string> InitOutputDirAsync(PolicyOptions options)
        {
            var output = Path.Combine(Directory.GetCurrentDirectory(), options.Environment ?? "");
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }

            var list = new List<string>();
            foreach(var file in Directory.GetFiles(output))
            {
                list.Add(file);
            }

            foreach(var file in list)
            {
                File.Delete(file);
            }

            return output;
        }

        private static async Task ProcessPolicyFileAsync(string content, PolicyOptions options, string outputPath, string fileName)
        {
            content = content
                .Replace("{{yourtenant}}", options.Tenant)
                .Replace("{{IdentityExperienceFrameworkAppId}}", options.IdentityExperienceFrameworkAppId)
                .Replace("{{ProxyIdentityExperienceFrameworkAppId}}", options.ProxyIdentityExperienceFrameworkAppId)
                .Replace("{{TokenSigningKeyContainerName}}", options.TokenSigningKeyContainer)
                .Replace("{{TokenEncryptionKeyContainerName}}", options.TokenEncryptionKeyContainer)
                ;

            await File.WriteAllTextAsync(Path.Combine(outputPath, fileName), content);
        }
    }



    public class PolicyOptions
    {

        [Option('e', "environment", Default = "Development", Required = false, HelpText = "The name of your environment.")]
        public string Environment { get; set; }

        [Option('t', "tenant", Required = true, HelpText = "The name of your tenant.")]
        public string Tenant { get; set; }

        [Option('a', "appid", Required = true, HelpText = "The application ID of the Identity Experience Framework Application.")]
        public string IdentityExperienceFrameworkAppId { get; set; }

        [Option('p', "proxyappid", Required = true, HelpText = "The application ID of the Proxy Identity Experience Framework Application.")]
        public string ProxyIdentityExperienceFrameworkAppId { get; set; }

        [Option('s', "signingkeycontainer", Default = "B2C_1A_TokenSigningKeyContainer", Required = false, HelpText = "The name of the signing key container to use with your policy.")]
        public string TokenSigningKeyContainer { get; set; }

        [Option('c', "encryptionkeycontainer", Default = "B2C_1A_TokenEncryptionKeyContainer", Required = false, HelpText = "The name of the encryption key container to use with your policy.")]
        public string TokenEncryptionKeyContainer { get; set; }

    }
}
