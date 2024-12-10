using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace imtahan.BLL.DomainModel.Entities
{
    public class Class : EntityBase
    {
        [Key]
        [Column(TypeName = "char(3)")]
        [Required]
        [StringLength(3)]
        public string Code { get; set; }

        [Column(TypeName = "varchar(30)")]
        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "numeric(2,0)")]
        public int No { get; set; }

        [MaxLength(20), MinLength(1)]
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string InstructorName { get; set; }

        [MaxLength(20), MinLength(1)]
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string InstructorSurname { get; set; }
    }
}