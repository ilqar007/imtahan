using System.ComponentModel.DataAnnotations;

namespace imtahan.BLL.ServiceLayer.DtoAndMessages
{
    public class ExamDto
    {
        [StringLength(3)]
        public string ClassCode { get; set; }

        [Required]
        public int StudentNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public byte Grade { get; set; }
    }
}