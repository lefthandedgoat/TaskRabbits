using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Highway.Data;
using TaskRabbits.Models;
using System.ComponentModel.DataAnnotations;

namespace TaskRabbits.DataAccess
{
    public class TaskRabbitMappings : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RabbitMap());
        }
    }

    public class RabbitMap : EntityTypeConfiguration<Rabbit>
    {
        public RabbitMap()
        {
            this.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(15);
        }
    }
}