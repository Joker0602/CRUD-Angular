using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data.Repository.StudentRepository;
using StudentAPI.Dto;
using StudentAPI.Model;

namespace StudentAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studnetRepo;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepo studnetRepo, IMapper mapper)
        {
            _studnetRepo = studnetRepo;
            _mapper = mapper;
        }
        [HttpGet("Student")]
        public async Task<ActionResult<IEnumerable<StudentMaster>>> GetStudentAll()
        {
            var student = await _studnetRepo.GetStudentAll();
            return Ok(_mapper.Map<IEnumerable<StudentReadDto>>(student));
        }
        [HttpGet("Student/{id}")]
        public async Task<ActionResult<StudentMaster>>GetById(int id)
        {
            var student = await _studnetRepo.GetStudentById(id);
            if(student==null)
            {
                return NotFound("Student Not found");
            }
            return Ok(_mapper.Map<StudentReadDto>(student));
        }
        [HttpPost("Student/Add")]
        public async Task<ActionResult> AddStudent(StudentCreateDto student){
            var studentModel = _mapper.Map<StudentMaster>(student);
            _studnetRepo.AddStudent(studentModel);
            await _studnetRepo.SaveChanges();

            return Ok(_mapper.Map<StudentReadDto>(studentModel));  
        }
        [HttpPut("Student/Update/{id}")]
        public async Task<ActionResult>UpdateStudent(int id, StudentCreateDto student)
        {
            var studentModel = await _studnetRepo.GetStudentById(id);
            if (studentModel == null)
            {
                return NotFound("Not Found");
            }
            _mapper.Map(student, studentModel);
            await _studnetRepo.SaveChanges();

            return Ok(_mapper.Map<StudentReadDto>(studentModel));
        }
        [HttpDelete("Student/Delete/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _studnetRepo.GetStudentById(id);
            if(student == null)
            {
                return NotFound("Not Found");
            }
            _studnetRepo.DeleteStudent(student);
            await _studnetRepo.SaveChanges();

            return Ok(new {message="success"});
        }
        [HttpGet("Student/MaxId")]
        public async Task<ActionResult> MaxId()
        {
            
            var  result = await _studnetRepo.MaxId();
            return Ok(result);
            
        }
    }
}
