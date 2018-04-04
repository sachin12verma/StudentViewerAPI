using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting;
using AttributeRouting.Web.Http;
using BusinessEntities;
using BusinessServices;
using WebApi.ActionFilters;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
  [AuthorizationRequired]
  [RoutePrefix("v1/Students/Student")]
  public class StudentController : ApiController
  {
    //
    // GET: /Student/
    #region Private variable.

    private readonly IStudentService _studentServices;

    #endregion

    #region Public Constructor

    /// <summary>
    /// Public constructor to initialize student service instance
    /// </summary>
    public StudentController(IStudentService studentServices)
    {
      _studentServices = studentServices;
    }

    #endregion

    // GET api/student
    [GET("allstudents")]
    [GET("all")]
    public HttpResponseMessage Get()
    {
      var students = _studentServices.GetStudents(); 
      var studentEntities = students as List<StudentEntity> ?? students.ToList();
      if (studentEntities.Any())
        return Request.CreateResponse(HttpStatusCode.OK, studentEntities);
      throw new ApiDataException(1000, "Students not found", HttpStatusCode.NotFound);
    }

    // GET api/student/5
    [GET("student/{id?}")]
    [GET("particularstudent/{id?}")]
    [GET("mystudent/{id:range(1, 3)}")]
    public HttpResponseMessage Get(int id)
    {
      if (id > 0)
      {
        var student = _studentServices.GetStudentById(id);
        if (student != null)
          return Request.CreateResponse(HttpStatusCode.OK, student);

        throw new ApiDataException(1001, "No student found for this id.", HttpStatusCode.NotFound);
      }
      throw new ApiException() { ErrorCode = (int)HttpStatusCode.BadRequest, ErrorDescription = "Bad Request..." };
    }
  }
}
