using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PoS.Shared.ResponseDTOs
{
    public class CustomerResponse
    {

        public Guid? Id { get; set; }
        public Guid? BusinessId { get; set; }
        public Guid? UserId { get; set; }

        public Guid? LoyaltyId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoginName { get; set; }

        /*public string? Password {
            get => Password;
            set
            {
                Password = value;
                CreatePasswordHash();
            }
        }*/

        public string? JwtToken { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Address { get; set; }

        public double? Points { get; set; }

        /*private byte[] passwordHash;

        private byte[] passwordSalt;

        private void CreatePasswordHash()
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        public byte[] GetPasswordHash()
        {
            return passwordHash;
        }

        public byte[] GetPasswordSalt()
        {
            return passwordSalt;
        }*/

    }
}
