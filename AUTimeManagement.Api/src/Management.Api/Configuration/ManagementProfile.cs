using AUTimeManagement.Api.Business.Logic.Models;
using AUTimeManagement.Api.DataAccess.Layer.Model;
using AUTimeManagement.Api.Management.Api.Models;
using AutoMapper;

namespace AUTimeManagement.Api.Management.Api.Configuration
{
    public class ManagementProfile : Profile
    {
        public ManagementProfile()
        {
            CreateMap<WorkUnitViewModel, AUTimeManagement.Api.Business.Logic.Models.CreateWorkModel>().ReverseMap();
            CreateMap<UpdateProjectViewModel, UpdateProjectModel>().ReverseMap();
            CreateMap<ProjectViewModel, ProjectModel>().ReverseMap();
            CreateMap<AUTimeManagement.Api.Business.Logic.Models.CreateWorkModel, AUTimeManagement.Api.DataAccess.Layer.Model.CreateWorkModel>().ReverseMap();
            CreateMap<Project, ProjectModel>().ReverseMap();
        }
    }
}
