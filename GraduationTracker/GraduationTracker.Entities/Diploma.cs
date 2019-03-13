using GraduationTracker.Interfaces;

namespace GraduationTracker.Entities
{
    public class Diploma : IDiploma
    {
        public int Id { get; set; }
        public int Credits { get; set; }
        public int[] Requirements { get; set; }
    }
}
