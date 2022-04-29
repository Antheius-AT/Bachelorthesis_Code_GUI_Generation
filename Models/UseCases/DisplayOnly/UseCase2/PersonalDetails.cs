using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UseCases.DisplayOnly.UseCase2
{
    public class PersonalDetails
    {
        public PersonalDetails()
        {
            this.Gender = "Undefined";
            this.FirstName = "Spaghetti";
            this.LastName = "Monster";
            this.Age = 96;
            this.Address = new Address();
            this.Educations = new List<JobEducation>()
            {
                new JobEducation(),
                new JobEducation(),
                new JobEducation(),
                new JobEducation(),
                new JobEducation(),
                new JobEducation(),
                new JobEducation(),
                new JobEducation(),
            };
        }

        public string Gender { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }

        public List<JobEducation> Educations { get; set; }
    }
}
