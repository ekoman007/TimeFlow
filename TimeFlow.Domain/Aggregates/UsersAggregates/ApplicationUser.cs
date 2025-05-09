﻿using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class ApplicationUser : AggregateRoot<int>
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; } 
        public DateTime? LastLogin { get; private set; }
        public int RoleId { get; private set; }
        public virtual Role Role { get; private set; }
        public bool IsBussines { get; private set; }
            
        // Static factory method
        public static ApplicationUser Create(string username, string email, string passwordHash, bool isBussines)
        {
            var user = new ApplicationUser
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash,
                IsBussines = isBussines,
                RoleId = isBussines ? (int)RolesStatus.BussinesAdmin : (int)RolesStatus.User
            };

            user.ValidateUser();

            return user;
        }

        public void ChangeUserName(string username)
        {
            Username = username;
            ValidateUser();
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            ValidateUser();
        }

        public void ChangeRoleId(int roleId)
        {
            RoleId = roleId;
            ValidateUser();
        }

        public void SetLastLogin(DateTime time)
        {
            LastLogin = time;
        }

        public void Activate()
        {
            IsActive = true;
        }
        public void DeActivate()
        {
            IsActive = false;
        }

        private void ValidateUser()
        {
            if (string.IsNullOrWhiteSpace(Username))
                ThrowDomainException("Username is required.");

            if (string.IsNullOrWhiteSpace(Email))
                ThrowDomainException("Email is required.");

            if (string.IsNullOrWhiteSpace(PasswordHash))
                ThrowDomainException("Password is required.");

            if (RoleId <= 0)
                ThrowDomainException("Role is required.");
        }
    }
}
