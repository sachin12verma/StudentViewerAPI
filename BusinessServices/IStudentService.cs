using BusinessEntities;
using System.Collections.Generic;

namespace BusinessServices
{
  public interface IStudentService
  {
    StudentEntity GetStudentById(int studentId);
    IEnumerable<StudentEntity> GetStudents();
  }
}
