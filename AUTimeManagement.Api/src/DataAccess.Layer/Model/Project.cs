using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.DataAccess.Layer.Model
{
    public class Project
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; } = "";
    }
}
