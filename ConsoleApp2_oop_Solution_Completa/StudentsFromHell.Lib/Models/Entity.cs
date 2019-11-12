using Academy.Lib.Infrastructure;
using System;

namespace Academy.Lib.Models
{
    public abstract class  Entity
    {
        public Guid Id { get; set; }

        public abstract Action<Entity> RepositoryAddAction { get; }
        public abstract Action<Entity> RepositoryUpdateAction { get; }

        public ValidationResult CurrentValidation { get; private set; }

        public virtual SaveResult<Entity> Save()
        {
            var output = new SaveResult<Entity>();

            CurrentValidation = Validate();

            if (CurrentValidation.IsSuccess)
            {
                if (this.Id == Guid.Empty)
                {
                    this.Id = Guid.NewGuid();
                    RepositoryAddAction(this);
                }
                else
                {
                    RepositoryUpdateAction(this);
                }
            }

            output.Validation = CurrentValidation;

            return output;
        }

        public abstract ValidationResult Validate();
    }
}
