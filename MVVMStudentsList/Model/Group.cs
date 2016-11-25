using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMStudentsList.Model
{
    public class Group
    {
        [Key]
        public int IDGroup { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public byte[] Stamp { get; set; }
        public virtual List<Student> Students { get; set; } 
    }
}
