using System.Collections.Generic;

namespace GraduationTracker.Interfaces
{
    public interface IStudentRepository
    {
        void AddStudent(IStudent student);
        List<IStudent> GetStudents();
    }
}