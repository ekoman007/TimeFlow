using System;
using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole Role { get; private set; }
        public string? PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLogin { get; private set; }

        // Private constructor for EF Core
        private User() { }

        public User(string email, string passwordHash, string firstName, string lastName, 
            UserRole role, string? phoneNumber = null)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Role = role;
            PhoneNumber = phoneNumber;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateLoginTime()
        {
            LastLogin = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void UpdatePersonalInfo(string firstName, string lastName, string? phoneNumber)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            PhoneNumber = phoneNumber;
        }

        public void UpdateRole(UserRole role)
        {
            Role = role;
        }
    }
} 