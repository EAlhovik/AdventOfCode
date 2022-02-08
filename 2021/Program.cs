using System;
using System.Linq;

namespace _2021
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var tasks = typeof(Program).Assembly.GetTypes()
                .Where(p => p.Name == nameof(Day01.Task))
                .Where(p => !p.IsAbstract);
            if(Environment.GetEnvironmentVariable("DEVELOPMENT") == "true")
            {
                tasks = tasks.Take(1).OrderByDescending(p => p.Namespace);
            }
            else
            {
                tasks = tasks.OrderBy(p => p.Namespace);
            }

            foreach (Type taskType in tasks)
            {
                var day = taskType.Namespace.Split('.')[1];
                var instance = Activator.CreateInstance(taskType);
                instance.GetType().GetMethod("Part1").Invoke(instance, new[] { day });
                instance.GetType().GetMethod("Part2").Invoke(instance, new[] { day });

                Console.WriteLine(string.Empty);
            }
        }
    }
}
