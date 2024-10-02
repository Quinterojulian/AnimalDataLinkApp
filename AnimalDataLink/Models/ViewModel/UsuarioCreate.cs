using System.ComponentModel.DataAnnotations;

namespace AnimalDataLinkApp.Models.ViewModel
{
    public class UsuarioCreate
    {
        [Required(ErrorMessage ="El usuario no es correcto")]
        public string usuario { get; set; }
        [PasswordValidation]
        public string pass { get; set; }
    }

    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("La contraseña no puede estar vacía.");
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsDigit))
            {
                return new ValidationResult("La contraseña debe contener al menos una letra mayúscula y un número.");
            }

            return ValidationResult.Success;
        }
    }
}
