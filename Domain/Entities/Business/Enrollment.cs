using Domain.Entities.BaseEntities;
using Domain.Entities.System;

namespace Domain.Entities.Business
{
    public class Enrollment : BaseEntity<int>
    {
        public string UserId { get; set; } = null!;   // لازم string عشان ApplicationUser.Id
        public ApplicationUser User { get; set; } = null!;

        public int CourseId { get; set; }             // Course.Id هو int
        public Course Course { get; set; } = null!;
    }
}
