﻿using Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UseCases.IncludingUserInteraction.UseCase2
{
    public class JobEducation
    {
        /// <summary>
        /// Initializes an instance of this class with default values for testing.
        /// In reality these members would be mapped from a database or otherwise retrieved.
        /// </summary>
        public JobEducation()
        {
            this.InstitutionName = "Demo Institution areal 5.6 XVC B";
            this.Duration = 6;
        }

        [Editable]
        [StringLength(25)]
        public string InstitutionName { get; set; }

        [Editable]
        public int Duration { get; set; }
    }
}
