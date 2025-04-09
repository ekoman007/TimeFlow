﻿ 

namespace TimeFlow.Domain.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string storedPasswordHash);
    }
}
