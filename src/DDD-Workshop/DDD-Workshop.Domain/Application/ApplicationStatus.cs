namespace DDD_Workshop.Domain.Application
{
    public class ApplicationStatus
    {
        public string Status { get; private set; }

        public ApplicationStatus Accepted()
        {
            Status = "Accepted";
            return this;
        }

        public ApplicationStatus Offered()
        {
            if (Status.Equals("Accepted"))
            {
                Status = "Offered";
            }

            return this;
        }

        public ApplicationStatus ManualEvaluation()
        {
            if (Status.Equals("Accepted"))
            {
                Status = "Manual Evaluation";
            }

            return this;
        }
    }
}