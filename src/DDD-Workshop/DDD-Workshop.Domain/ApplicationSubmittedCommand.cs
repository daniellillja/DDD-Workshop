using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public class ApplicationSubmittedCommand : DomainCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ApplicationSubmittedCommand(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
