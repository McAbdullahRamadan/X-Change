using Domain.Entities.BaseEntities;

namespace Domain.Entities.Business
{
    public class Section : BaseEntity<int>
    {
        public string Title { get; set; } = null!;
        public int OrderNumber { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
