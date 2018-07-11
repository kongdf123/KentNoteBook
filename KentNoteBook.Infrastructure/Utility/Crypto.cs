using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace KentNoteBook.Infrastructure.Utility
{
	public class Crypto
	{
		public static string GeneratePasswordSalt() {
			using ( var rng = RandomNumberGenerator.Create() ) {
				var saltArray = new byte[128 / 8];

				rng.GetBytes(saltArray);

				return Convert.ToBase64String(saltArray);
			}
		}

		public static string HashPassword(string saltString, string password) {
			var hashed = KeyDerivation.Pbkdf2(
				password: password,
				salt: Convert.FromBase64String(saltString),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 10000,
				numBytesRequested: 256 / 8);

			return Convert.ToBase64String(hashed);
		}

		static byte[] key = new byte[] { 49, 67, 210, 225, 9, 84, 122, 1, 71, 243, 117, 78, 122, 165, 56, 41, 134, 218, 253, 93, 99, 21, 91, 71, 189, 161, 120, 137, 39, 101, 254, 102 };
		static byte[] iV = new byte[] { 39, 162, 226, 236, 38, 132, 217, 231, 164, 6, 253, 180, 56, 68, 243, 163 };

		public static string Encrypt(string plainText) {
			using ( var rijAlg = Rijndael.Create() ) {

				rijAlg.Key = key;
				rijAlg.IV = iV;

				// Create an encryptor to perform the stream transform.
				var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for encryption.
				using ( var msEncrypt = new MemoryStream() )
				using ( CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write) )
				using ( StreamWriter swEncrypt = new StreamWriter(csEncrypt) ) {

					//Write all data to the stream.
					swEncrypt.Write(plainText);

					return Convert.ToBase64String(msEncrypt.ToArray());
				}
			}
		}

		public static string Decrypt(string cipherText) {

			// Create an Rijndael object
			// with the specified key and IV.
			using ( Rijndael rijAlg = Rijndael.Create() ) {

				rijAlg.Key = key;
				rijAlg.IV = iV;

				var cipherTextBytes = Convert.FromBase64String(cipherText);

				// Create a decryptor to perform the stream transform.
				var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
				
				// Create the streams used for decryption.
				using ( MemoryStream msDecrypt = new MemoryStream(cipherTextBytes) )
				using ( CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read) )
				using ( StreamReader srDecrypt = new StreamReader(csDecrypt) ) {

					// Read the decrypted bytes from the decrypting stream
					// and place them in a string.
					return srDecrypt.ReadToEnd();
				}
			}
		}
	}
}
