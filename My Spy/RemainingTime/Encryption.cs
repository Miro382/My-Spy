using System.Security.Cryptography;
using System.IO;
using System;
using System.Text;
using System.Diagnostics;

namespace EncryptionLibrary
{
    class Encryption256
    {
        private string Salt = "ECZh5rKADVbyM07FCemAYxSLpPzEDE";
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(512);

        public void SetSalt(String salt)
        {
            Salt = salt;
        }

        public Encryption256()
        {

        }

        public Encryption256(String salt)
        {
            Salt = salt;
        }

        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }




        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }


        //RSA Encryption
        private byte[] EncryptionRSA(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
               // byte[] Data = Encoding.UTF8.GetBytes(Text);
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey); encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        private byte[] DecryptionRSA(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        public string EncryptText(string input, string password)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Hash the password with SHA256
                passwordBytes =SHA256.Create().ComputeHash(passwordBytes);
                byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
                string result = Convert.ToBase64String(bytesEncrypted);

                return result;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }
        }

        public string DecryptText(string input, string password)
        {
            try
            {
                // Get the bytes of the string
                byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

                string result = Encoding.UTF8.GetString(bytesDecrypted);

                return result;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }
        }


    }


    
    
    public class Encryption
    {
        private byte[] _salt = Encoding.UTF8.GetBytes("ECZh5rKADVbyM07FCemAYxSLpPzEDE");


        public void SetSalt(String Salt)
        {
            _salt = Encoding.UTF8.GetBytes(Salt);
        }

        public Encryption()
        {

        }

        public Encryption(String Salt)
        {
            _salt = Encoding.UTF8.GetBytes(Salt);
        }


        public string EncryptString(string Text, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Text))
                    throw new ArgumentNullException("Text");
                if (string.IsNullOrEmpty(Password))
                    throw new ArgumentNullException("Password");

                string outStr = null;
                AesManaged aesAlg = null;

                try
                {
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Password, _salt);

                    aesAlg = new AesManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(Text);
                            }
                        }
                        outStr = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
                finally
                {
                    if (aesAlg != null)
                        aesAlg.Clear();
                }

                return outStr;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }
        }


        public string DecryptString(string Text, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Text))
                    throw new ArgumentNullException("cipherText");
                if (string.IsNullOrEmpty(Password))
                    throw new ArgumentNullException("sharedSecret");

                AesManaged aesAlg = null;

                string plaintext = null;

                try
                {
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Password, _salt);

                    byte[] bytes = Convert.FromBase64String(Text);
                    using (MemoryStream msDecrypt = new MemoryStream(bytes))
                    {
                        aesAlg = new AesManaged();
                        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                        aesAlg.IV = ReadByteArray(msDecrypt);
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
                finally
                {
                    if (aesAlg != null)
                        aesAlg.Clear();
                }

                return plaintext;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return "";
            }
        }

        private byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }

}
