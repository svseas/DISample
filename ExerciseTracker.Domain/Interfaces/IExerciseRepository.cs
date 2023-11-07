using ExerciseTracker.Domain.Entities;

namespace ExerciseTracker.Domain.Interfaces;

public interface IExerciseRepository
{
    IEnumerable<Exercise> GetAll();
    Exercise GetById(int id);
    void Add(Exercise exercise);
    void Update(Exercise exercise);
    void Delete(int id);
}