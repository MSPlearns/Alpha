using Domain.Models;
using WebApp.Models;

namespace WebApp.Extensions;

public static class ViewModelMapper
{
    public static ProjectViewModel ToViewModel(this Project model)
    {

        var projectViewModel = new ProjectViewModel
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            Description = model.Description,
            Budget = model.Budget,
            EndDate = model.EndDate,
            ProjectStatus = model.ProjectStatus.ToViewModel(),
            ProjectManager = model.ProjectManager.ToViewModel(),
            ProjectClient = model.ProjectClient.ToViewModel()
        };

        return projectViewModel;
    }
    public static ClientViewModel ToViewModel(this Client model) 
    {
        var clientViewModel = new ClientViewModel
        {
            Name = model.Name,
            Location = model.Location,
        };
        return clientViewModel;
    }
    public static AppUserViewModel ToViewModel(this AppUser model) 
    {
        var appUserViewModel = new AppUserViewModel
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            ImageUrl = model.ImageUrl,
            JobTitle = model.JobTitle,
        };
        return appUserViewModel;
    }
    public static StatusViewModel ToViewModel(this Status model) 
    {
        var statusViewModel = new StatusViewModel
        {
            Name = model.Name,
            Description = model.Description,
            Colour = model.Colour,
};
        return statusViewModel;
    }
}
