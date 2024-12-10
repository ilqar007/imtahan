using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace imtahan.BLL.DomainModel.Entities
{
    public class Exam : EntityBase
    {
        public virtual Class ClassCodeNavigation { get; set; }

        [ForeignKey(nameof(ClassCodeNavigation))]
        [StringLength(3)]
        public string ClassCode { get; set; }

        public virtual Student Student { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public byte Grade { get; set; }
    }
}