namespace DDD_Workshop.Domain
{
    public class Applicant
    {
        public Applicant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}