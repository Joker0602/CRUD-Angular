using StudentAPI.Model;

namespace StudentAPI.Data.Repository.StudentRepository
{
    public interface IStudentRepo
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<StudentMaster>> GetStudentAll();
        Task<StudentMaster> GetStudentById(int id);
        void AddStudent(StudentMaster student);
        void DeleteStudent(StudentMaster student);
        Task<int> MaxId();
    }
}
