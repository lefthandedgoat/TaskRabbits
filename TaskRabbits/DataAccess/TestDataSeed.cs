using System.Data.Entity;
using Highway.Data;
using TaskRabbits.Models;

namespace TaskRabbits.DataAccess
{
    public class TestDataSeed : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.Add(new Rabbit() {Name = "Test 1"});
            context.Add(new Rabbit() {Name = "Test 2"});
            context.SaveChanges();
            base.Seed(context);
        }
    }
}