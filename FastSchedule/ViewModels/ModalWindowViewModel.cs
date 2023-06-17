using FastSchedule.Application.Dto;

namespace FastSchedule.MVC.ViewModels
{
    public class ModalWindowViewModel
    {
        public ModalWindowViewModel(ScheduleTaskDto task, bool isAddWindow)
        {
            Task = task;
            IsAddWindow = isAddWindow;
        }

        public ScheduleTaskDto Task { get; set; }
        public bool IsAddWindow { get; set; }
    }
}
