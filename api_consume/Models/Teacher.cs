using System.ComponentModel.DataAnnotations;

namespace api_consume.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public int salary { get; set; }
        public string address { get; set; }
    }
}
