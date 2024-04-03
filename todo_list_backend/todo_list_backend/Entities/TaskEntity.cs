using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo_list_backend.Entities
{
    [Table("tasks", Schema = "transactional")]
    public class TaskEntity
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("description")]
        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Column("done")]
        public bool Done { get; set; }
    }
}
