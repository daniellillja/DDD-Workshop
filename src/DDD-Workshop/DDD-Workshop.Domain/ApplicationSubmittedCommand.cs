namespace DDD_Workshop.Domain
{
    public class ApplicationSubmittedCommand
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
