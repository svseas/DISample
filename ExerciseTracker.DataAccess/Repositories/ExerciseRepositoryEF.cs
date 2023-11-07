using ExerciseTracker.DataAccess.DatabaseContext;
using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;

namespace ExerciseTracker.DataAccess.Repositories;

public class ExerciseRepositoryEF : IExerciseRepository
{
    private readonly AppDbContext _context;
    
    public ExerciseRepositoryEF(AppDbContext context)
    {
        _context = context;
    }
    
    public void Add(Exercise exercise)
    {
        _context.Exercises.Add(exercise);
        _context.SaveChanges();
    }
    
    public IEnumerable<Exercise> GetAll()
    {
        return _context.Exercises;
    }
    
    public Exercise GetById(int id)
    {
        return _context.Exercises.FirstOrDefault(e => e.Id == id) ?? throw new InvalidOperationException();
    }
    
    public void Update(Exercise exercise)
    {
        _context.Exercises.Update(exercise);
        _context.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var exercise = _context.Exercises.FirstOrDefault(e => e.Id == id);
        if (exercise != null)
        {
            _context.Exercises.Remove(exercise);
            _context.SaveChanges();
        }
    }
}