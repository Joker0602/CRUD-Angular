using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Model
{
    public class StudentMaster
    {
        [Key]
        public int SId { get; set; }
        public string SName { get; set; }
        public string SEmail { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; } 
    }
}
