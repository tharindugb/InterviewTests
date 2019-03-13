namespace GraduationTracker.Interfaces
{
    public interface IDiplomaService
    {
        void AddDiploma(IDiploma diploma);
        IDiploma GetDiploma(int id);
        void AddRequirement(IRequirement requirement);
        IRequirement GetRequirement(int id);
    }
}