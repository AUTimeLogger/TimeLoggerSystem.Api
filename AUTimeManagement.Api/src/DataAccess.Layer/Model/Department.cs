using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.DataAccess.Layer.Model;

public sealed class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid Director { get; set; }
}
