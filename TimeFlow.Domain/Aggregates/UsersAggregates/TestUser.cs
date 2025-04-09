using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class TestUser : AggregateRoot<int>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public static TestUser Create(string name, string email, string Phone)
        {
            TestUser usertest = new() { Name = name, Email = email, Phone = Phone };

            usertest.ValidateTestUser();

            return usertest;
        }

        public void ChangeName(string name)
        {
            Name = name;

            ValidateTestUser();
        }

        public void ChangeEmail(string email)
        {
            Email = email;

            ValidateTestUser();
        }

        private void ValidateTestUser()
        {
            if (Name == null) ThrowDomainException("Name is required.");

            if (Email == null ) ThrowDomainException("Email is required.");
            if (Phone == null ) ThrowDomainException("Phone is required.");
        }
    }
}
