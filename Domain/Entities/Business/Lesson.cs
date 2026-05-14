using Domain.Entities.BaseEntities;

namespace Domain.Entities.Business
{
    public class Lesson : BaseEntity<int>
    {
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public int Duration { get; set; }

        public int OrderNumber { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;
    }
}