using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace imtahan.BLL.DomainModel.Entities
{
    public class Student : EntityBase
    {
        [Key]
        [Column(TypeName = "numeric(5,0)")]
        [Required]
        public int Number { get; set; }

        [Required]
        [MaxLength(30), MinLength(1)]
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(30), MinLength(1)]
        [Column(TypeName = "varchar(30)")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "numeric(2,0)")]
        public int ClassNo { get; set; }
    }
}