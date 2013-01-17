using Highway.Data.QueryObjects;
using TaskRabbits.Models;

namespace TaskRabbits.DataAccess
{
    public class AllRabbits : Query<Rabbit>
    {
        public AllRabbits()
        {
            ContextQuery = context => context.AsQueryable<Rabbit>();
        }
    }
}