using System;
using DDD_Workshop.Infrastructure;

namespace DDD_Workshop.Domain
{
    public class EvaluateApplicationCommand : DomainCommand
    {
        public Guid Id { get; set; }


        public EvaluateApplicationCommand(Guid id)
        {
            Id = id;
        }
    }
}