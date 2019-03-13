using System.Collections.Generic;

namespace GraduationTracker.Interfaces
{
    public interface IDiplomaRepository
    {
        void AddDiploma(IDiploma diploma);       
        void AddRequirement(IRequirement requirement);
        List<IDiploma> GetDiplomas();
        List<IRequirement> GetRequirements();
    }

}