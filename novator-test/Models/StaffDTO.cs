using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novator_test.Models
{
    public class StaffDTO
    {
        [Key]
        public int personId { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string patronymic { get; set; }

        public StaffDTO(string name, string lastname, string patronymic) {
            this.name= name;
            this.lastname= lastname;
            this.patronymic= patronymic;
        }
    }
}
