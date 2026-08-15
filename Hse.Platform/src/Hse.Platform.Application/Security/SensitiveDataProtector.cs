using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Encryption;

namespace Hse.Platform.Security;

public class SensitiveDataProtector : ITransientDependency
{
    private readonly IStringEncryptionService _encryptionService;
    private readonly AbpStringEncryptionOptions _options;

    public SensitiveDataProtector(
        IStringEncryptionService encryptionService,
        IOptions<AbpStringEncryptionOptions> options)
    {
        _encryptionService = encryptionService;
        _options = options.Value;
    }

    public string? Encrypt(string? plaintext)
    {
        if (string.IsNullOrWhiteSpace(plaintext))
        {
            return null;
        }

        return "v1:" + _encryptionService.Encrypt(plaintext);
    }

    public string? Decrypt(string? ciphertext)
    {
        if (string.IsNullOrWhiteSpace(ciphertext))
        {
            return null;
        }

        var value = ciphertext.StartsWith("v1:", StringComparison.Ordinal)
            ? ciphertext[3..]
            : ciphertext;

        return _encryptionService.Decrypt(value);
    }

    public string? CreateBlindIndex(string? plaintext)
    {
        if (string.IsNullOrWhiteSpace(plaintext))
        {
            return null;
        }

        var key = Encoding.UTF8.GetBytes(_options.DefaultPassPhrase + ":index");
        var data = Encoding.UTF8.GetBytes(plaintext.Trim());
        var hash = HMACSHA256.HashData(key, data);
        return Convert.ToHexString(hash);
    }
}
