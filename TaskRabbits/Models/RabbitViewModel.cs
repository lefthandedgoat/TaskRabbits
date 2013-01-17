using System.Web.Mvc;

namespace TaskRabbits.Models
{
    public class RabbitViewModel
    {
        public RabbitViewModel(Rabbit rabbit, UrlHelper url )
        {
            Id = rabbit.Id;
            Name = rabbit.Name;
            TaskUrl = url.RouteUrl(new {controller = "Tasks", action = "Index", rabbitId = rabbit.Id});
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaskUrl { get; set; }
    }
}