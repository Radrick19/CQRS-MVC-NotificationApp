using FastSchedule.Application.Dto;
using FastSchedule.Application.Infrastructure.Enums;

namespace FastSchedule.MVC.ViewModels
{
    public class TasksListViewModel
    {
        public TasksListViewModel(List<ScheduleTaskDto> tasks, TasksCondition status)
        {
            Tasks = tasks;
            Status = status;
        }

        public List<ScheduleTaskDto> Tasks { get; set; }
        public TasksCondition Status { get; set; }
    }
}
