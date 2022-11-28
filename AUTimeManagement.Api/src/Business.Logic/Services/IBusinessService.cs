using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.Business.Logic.Services
{
    public interface IBusinessService
    {
        IProjectService Projects { get; }
    }
}
