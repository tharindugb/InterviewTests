namespace GraduationTracker.Interfaces
{
    public interface IRequirement :IEntity
    {
        int[] Courses { get; set; }
        int Credits { get; set; }
        int MinimumMark { get; set; }
        string Name { get; set; }
    }
}