namespace GraduationTracker.Interfaces
{
    public interface IDiploma : IEntity
    {
        int Credits { get; set; }
        int[] Requirements { get; set; }
    }
}