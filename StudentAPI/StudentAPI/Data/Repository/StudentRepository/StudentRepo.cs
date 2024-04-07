using Microsoft.EntityFrameworkCore;
using StudentAPI.Model;
using System.Runtime.CompilerServices;

namespace StudentAPI.Data.Repository.StudentRepository
{
    public class StudentRepo:IStudentRepo
    {
        private readonly StudentDbContext _context;
        public StudentRepo(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChanges()
        {
           var result = await _context.SaveChangesAsync();
            return (result>0);
        }

        public async Task<IEnumerable<StudentMaster>> GetStudentAll()
        {
            return await _context.studentMasters.ToListAsync();
        }
        public async Task<StudentMaster>GetStudentById(int id)
        {
            return await _context.studentMasters.FirstOrDefaultAsync(x => x.SId == id);
        }
        public void AddStudent(StudentMaster studentMaster)
        {
            _context.studentMasters.Add(studentMaster);
        }
        public void DeleteStudent(StudentMaster student)
        {
            _context.studentMasters.Remove(student);
        }
        public async Task<int> MaxId()
        {
            return await _context.studentMasters.MaxAsync(x => x.SId);
        }
    }
}
