﻿

using Models.Metadata;

namespace Models.UseCases.DisplayOnly.UseCase2
{
    [Editable]
    public class PersonalDetails
    {
        public PersonalDetails()
        {
            this.Gender = "Spaghetti Monster does not have a defined gender";
            this.FirstName = "Spaghetti";
            this.LastName = "Monster";
            this.Age = 96291;
            this.Address = new Address();
            this.Educations = new List<JobEducation>()
            {
                new JobEducation() {Duration = 2, InstitutionName = "MonsterAG"},
                new JobEducation() {Duration = 27, InstitutionName = "Spaghetti Insitution"},
                new JobEducation() {Duration = 1, InstitutionName = "Terror institution"},
                new JobEducation() {Duration = 2, InstitutionName = "Terror AG"},
                new JobEducation() {Duration = 2, InstitutionName = "Dark cave company"},
                new JobEducation() {Duration = 1, InstitutionName = "Night company"},
                new JobEducation() {Duration = 2, InstitutionName = "Company of the moon"},
                new JobEducation() {Duration = 3, InstitutionName = "Moonfall solutions"},
                new JobEducation(),
            };
        }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }

        public List<JobEducation> Educations { get; set; }
    }
}
