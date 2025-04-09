
namespace TimeFlow.SharedKernel
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public void SetModifiedDate()
        {
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
