using GraduationTracker.Interfaces;

namespace GraduationTracker.Entities
{
    public class Course : ICourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public int Credits { get; set; }
    }
}
