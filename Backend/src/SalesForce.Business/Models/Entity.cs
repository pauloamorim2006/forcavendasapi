using FluentValidation.Results;
using System;

namespace ERP.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}