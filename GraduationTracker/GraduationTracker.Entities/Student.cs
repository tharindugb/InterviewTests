using GraduationTracker.Interfaces;

namespace GraduationTracker.Entities
{
    public class Student : IStudent
    {
        public int Id { get; set; }
        public ICourse[] Courses { get; set; }
        public STANDING Standing { get; set; } = STANDING.None;        
    }
}
