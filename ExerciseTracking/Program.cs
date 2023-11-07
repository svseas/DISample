using ExerciseTracker.DataAccess.DatabaseContext;
using ExerciseTracker.DataAccess.Repositories;
using ExerciseTracker.Domain.Entities;
using ExerciseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExerciseTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=exercise.db"))
                .AddScoped<IExerciseRepository, ExerciseRepositoryEF>()
                .AddScoped<App>()
                .BuildServiceProvider();

            // Run the app
            var app = serviceProvider.GetService<App>();
            app.Run();
        }

    }

    public class App
    {
        private readonly IExerciseRepository _repository;

        public App(IExerciseRepository repository)
        {
            _repository = repository;
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("Welcome to Exercise Tracker!");
                Console.WriteLine("1. Add Exercise");
                Console.WriteLine("2. View Exercises");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddExercise();
                        break;
                    case "2":
                        ViewExercises();
                        break;
                    case "3":
                        keepGoing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        private void AddExercise()
        {
            Console.Write("Enter start date and time (MM/dd/yyyy hh:mm:ss): ");
            var start = DateTime.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Console.Write("Enter end date and time (MM/dd/yyyy hh:mm:ss): ");
            var end = DateTime.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Console.Write("Enter comments: ");
            var comments = Console.ReadLine();
            var exercise = new Exercise
            {
                DateStart = start,
                DateEnd = end,
                Comments = comments
            };
            _repository.Add(exercise);
        }

        private void ViewExercises()
        {
            var exercises = _repository.GetAll();
            foreach (var exercise in exercises)
            {
                Console.WriteLine($"Exercise {exercise.Id}:");
                Console.WriteLine($"Start: {exercise.DateStart}");
                Console.WriteLine($"End: {exercise.DateEnd}");
                Console.WriteLine($"Duration: {exercise.Duration}");
                Console.WriteLine($"Comments: {exercise.Comments}");
                Console.WriteLine();
            }
        }
    }

}

