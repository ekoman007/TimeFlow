 
using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Payment : AggregateRoot<int>
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? TransactionReference { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Completed;

        public int? ProcessedByUserId { get; set; } // Nëse e regjistron dikush manualisht

        public static Payment Create(
            int appointmentId,
            decimal amount,
            PaymentMethod method,
            DateTime paymentDate,
            string? transactionReference = null,
            int? processedByUserId = null)
        {
            return new Payment
            {
                AppointmentId = appointmentId,
                Amount = amount,
                Method = method,
                PaymentDate = paymentDate,
                TransactionReference = transactionReference,
                Status = PaymentStatus.Completed,
                ProcessedByUserId = processedByUserId
            };
        }
    }
}