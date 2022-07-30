using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_Resume_Maker
{
    public class Resume_Data
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Profile1 { get; set; }
        public string Profile2 { get; set; }
        public string CareerObj { get; set; }

        public List<Experience> Experiences { get; set; }
        public List<Education> Education { get; set; }
        public List<string> Skills { get; set; }
        public string Activities { get; set; }
    }
    public class Experience
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }

    }
    public class Education
    {
        public string MonthYear { get; set; }
        public string DegreeTitle { get; set; }
        public string School { get; set; }
        public string Description { get; set; }
    }

}