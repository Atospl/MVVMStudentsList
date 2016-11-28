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

        [MaxLength(16)]
        public string Name { get; set; }
        [Timestamp]
        public byte[] Stamp { get; set; }
        public virtual List<Student> Students { get; set; } 

        public Group(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Group))
                return false;


            return ((Group)obj).IDGroup == this.IDGroup;
        }

        private Group()
        {

        }
    }
}
