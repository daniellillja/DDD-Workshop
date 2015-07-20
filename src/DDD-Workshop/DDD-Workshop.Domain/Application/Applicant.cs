using System;

namespace DDD_Workshop.Domain.Application
{
    public class Applicant : IEquatable<Applicant>
    {
        public Applicant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Equals(Applicant other)
        {
            var firstNameSame = FirstName.Equals(other.FirstName);
            var lastNameSame = LastName.Equals(other.LastName);
            return firstNameSame && lastNameSame;
        }
    }
}