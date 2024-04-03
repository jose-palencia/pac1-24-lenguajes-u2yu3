using System.ComponentModel.DataAnnotations;

namespace todo_list_backend.Dtos.Tasks
{
    public class TaskCreateDto
    {
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        [StringLength(maximumLength: 250, 
            MinimumLength = 10, 
            ErrorMessage = "La {0} debe tener entre {2} y {1} letras" )]
        public string Description { get; set; }

        public bool Done { get; set; }
    }
}
