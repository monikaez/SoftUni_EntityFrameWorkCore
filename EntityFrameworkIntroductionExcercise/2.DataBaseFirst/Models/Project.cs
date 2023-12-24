﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUni.Models
{
    public partial class Project
    {
        public Project()
        {
            EmployeesProjects = new HashSet<EmployeeProject>();
        }

        public int ProjectId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        //[ForeignKey("ProjectId")]
        //[InverseProperty(nameof(EmployeesProjects))]
        public virtual ICollection<EmployeeProject> EmployeesProjects { get; set; }
        // public virtual ICollection<EmployeeProject> EmployeesProjects { get; set; }
    }
}
