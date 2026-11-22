using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome.")]
        [MinLength(6, ErrorMessage = "O nome deve ter pelo menos 6 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Contato")]
        [Required(ErrorMessage = "Informe o contato.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "O contato deve ser numérico e conter exatamente 9 dígitos.")]
        public string ContactNumber { get; set; } = string.Empty;

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

    }
}
