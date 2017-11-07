using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructureAuthentication
{
    public class LoginCredentials : IAuthable
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class ChangePasswordViewModel : IAuthable
    {
        public Guid Token { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }

    public class ChangePasswordToken : Entity
    {
        public Guid Token { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual User User { get; set; }

        public bool IsValid() => CreationTime.AddDays(1d).CompareTo(DateTime.Now) > 0;
    }

    public class User : Entity
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public virtual Password Password { get; set; }
    }

    public class Password : Entity
    {
        public Password()
            => CreationDate = DateTime.Now;

        public void SetValue(string value)
        {
            Value = PasswordCrypt.CreateHash(value);
        }

        public bool Matches(string password)
        {
            if (PasswordCrypt.ValidatePassword(password, Value))
                return true;
            return false;
        }

        public bool Matches(string password, bool alreadyEncryptedPassword)
        {
            if (!alreadyEncryptedPassword)
                return Matches(password);
            if (PasswordCrypt.ValidatePassword(password, Value))
                return true;
            return false;
        }

        public string Value { get; internal set; }
        public DateTime CreationDate { get; set; }
    }

    public class PasswordCrypt
    {
        // The following constants may be changed without breaking existing hashes.
        public const int SALT_BYTE_SIZE = 256;
        public const int HASH_BYTE_SIZE = 512;
        public const int PBKDF2_ITERATIONS = 1433;

        public const int ITERATION_INDEX = -1;
        public const int SALT_INDEX = 0;
        public const int PBKDF2_INDEX = 1;

        public static string CreateHash(string password)
        {
            // Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            var hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }


        public static bool ValidatePassword(string password, string correctHash)
        {
            // Extract the parameters from the hash
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = PBKDF2_ITERATIONS; //Int32.Parse(split[ITERATION_INDEX]);
            var salt = Convert.FromBase64String(split[SALT_INDEX]);
            var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            var testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
