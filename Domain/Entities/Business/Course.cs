using Domain.Entities.BaseEntities;
using Domain.Entities.System;

namespace Domain.Entities.Business
{
    public class Course : BaseEntity<int>
    {

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string InstructorId { get; set; } = null!;
        public ApplicationUser Instructor { get; set; } = null!;

        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Section> Sections { get; set; } = new List<Section>();

    }
}
