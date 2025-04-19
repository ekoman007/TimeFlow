using System;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class RefreshToken : AggregateRoot<int>
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } // Linku në përdoruesin aktual
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }

        // Static method to create RefreshToken
        public static RefreshToken Create(string token, DateTime expires, int userId)
        {
            if (expires <= DateTime.UtcNow)
            {
                throw new ArgumentException("Expiration date must be in the future.");
            }

            return new RefreshToken
            {
                Token = token,
                Expires = expires,
                UserId = userId,
                IsUsed = false,
                IsRevoked = false
            };
        }


        public void MarkAsUsed()
        {
            IsUsed = true;
        }

        // opcionalisht mund të kesh edhe një metodë për revokim
        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
