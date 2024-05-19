using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Contracts.Interfaces
{
    public interface IAES256
    {
        string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv);
        byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv);
        string Decrypt(string ciphertext);
        string Encrypt(string paintext);
        string Base64Encode(string plainText);
        string Base64Decode(string base64EncodedData);
    }
}
