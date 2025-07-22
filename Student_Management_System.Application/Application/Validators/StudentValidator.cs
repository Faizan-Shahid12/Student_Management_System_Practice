using FluentValidation;
using Student_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Application.Application.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(4).MaximumLength(10).WithMessage("FirstName not correct");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(4).MaximumLength(10).WithMessage("LastName not correct");
            RuleFor(x => x.Semester).NotEmpty().InclusiveBetween(1, 8).WithMessage("Semester is not between 1 to 8");
            RuleFor(x => x.Phone).NotEmpty().MinimumLength(8).MaximumLength(8).WithMessage("Not correct format");
        }
    }
}
