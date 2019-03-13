namespace GraduationTracker.Interfaces
{
    public interface ICourse : IEntity
    {
        int Credits { get; set; }
        int Mark { get; set; }
        string Name { get; set; }
    }
}