﻿using Academy.Lib.Context;
using Academy.Lib.Infrastructure;
using System;

namespace Academy.Lib.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; }

        public override Action<Entity> RepositoryAddAction => (ent) => { DbContext.AddSubject(ent as Subject); };
        public override Action<Entity> RepositoryUpdateAction => (ent) => { DbContext.UpdateSubject(ent as Subject); };

        public void ValidateName(ValidationResult<Entity> validationResult)
        {
            validationResult.IsSuccess = true;

            if (!string.IsNullOrEmpty(this.Name))
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add("el nombre de la asignatura no puede estar vacío");
            }

            if (DbContext.SubjectsByName.ContainsKey(this.Name))
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add($"Ya existe una asignatura que se llama {this.Name}");
            }           
        }

        public override ValidationResult<Entity> Validate()
        {
            var validationResult = new ValidationResult<Entity>
            {
                IsSuccess = true
            };

            ValidateName(validationResult);

            return validationResult;
        }

        public SaveResult<Subject> SaveStudent()
        {
            var saveResult = base.Save();
            return saveResult.Cast<Subject>();
        }
    }
}