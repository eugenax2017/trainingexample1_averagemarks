using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Lib.Context;
using Academy.Lib.Infrastructure;

namespace Academy.Lib.Models
{
    public class Student : Entity
    {
        #region Static Validations

        public static void ValidateDni(ValidationResult validationResult, string dni)
        {
            var vr = ValidateDni(dni);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }            
        }

        public static ValidationResult ValidateDni(string dni)
        {
            var output = new ValidationResult()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("el dni delalumno no puede estar vacío");
            }

            if(DbContext.StudentsByDni.ContainsKey(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("ya existe un alumno con ese dni");
            }
            
            return output;
        }

        public static Tuple<bool, string> ValidateName(string dni)
        {
            if (string.IsNullOrEmpty(dni))
                return new Tuple<bool, string>(false, "el nombre delalumno no puede estar vacío");
            else
                return new Tuple<bool, string>(true, string.Empty);
        }

        #endregion

        public string Dni { get; set; }
        public string Name { get; set; }

        public List<Exam> Exams
        {
            get
            {
                return DbContext.Exams.Values.Where(e => e.student.Id == this.Id).ToList();
            }
        }

        public override Action<Entity> RepositoryAddAction => (ent) => 
        {
            if (CurrentValidation != null && CurrentValidation.IsSuccess)
                DbContext.AddStudent(ent as Student);
        };

        public override Action<Entity> RepositoryUpdateAction => (ent) => 
        {
            if (CurrentValidation != null && CurrentValidation.IsSuccess)
                DbContext.UpdateStudent(ent as Student); 
        };

        #region Domain Validations

        // Borrar y llevar a Static validations
        public void ValidateName(ValidationResult validationResult)
        {
            var validateNameResult = ValidateName(this.Name);
            if (!validateNameResult.Item1)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add(validateNameResult.Item2);
            }
        }

        #endregion
               
        public SaveResult<Student> SaveStudent()
        {            
            var saveResult = base.Save();
            return saveResult.Cast<Student>();
        }

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            // cambiar ValidateName para que sea igual que ValidateDni
            ValidateName(output);
            ValidateDni(output, this.Dni);

            return output;
        }
    }
}
