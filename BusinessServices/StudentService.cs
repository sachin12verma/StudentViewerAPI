using System;
using System.Collections.Generic;
using BusinessEntities;
using DataModel.UnitOfWork;
using System.Linq;
using DataModel;
using AutoMapper;

namespace BusinessServices
{
  public class StudentService : IStudentService
  {
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// Public constructor.
    /// </summary>
    public StudentService(UnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public StudentEntity GetStudentById(int studentId)
    {
      var student = _unitOfWork.StudentRepository.GetByID(studentId);
      if (student != null)
      {
        Mapper.CreateMap<Student, StudentEntity>();
        var studentModel = Mapper.Map<Student, StudentEntity>(student);
        return studentModel;
      }
      return null;
    }

    public IEnumerable<StudentEntity> GetStudents()
    {
      var students = _unitOfWork.StudentRepository.GetAll().ToList();
      if (students.Any())
      {
        Mapper.CreateMap<Student , StudentEntity >();
        var productsModel = Mapper.Map<List<Student>, List<StudentEntity>>(students);
        return productsModel;
      }
      return null;
    }
  }
}
