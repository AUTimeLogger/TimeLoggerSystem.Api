using AUTimeManagement.Api.DataAccess.Layer;
using AUTimeManagement.Api.DataAccess.Layer.Context;
using AUTimeManagement.Api.DataAccess.Layer.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.UnitTest.Scenarios
{
    internal class InjectionScenario
    {
        private readonly ServiceProvider _provider;
        public InjectionScenario()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDataAccessLayer();
            
            _provider = services.BuildServiceProvider();

            var writer = _provider.GetRequiredService<IDataWriter>();

            writer.AddProject("test project");
            writer.AddProject("project2");
            writer.AddProject("project3");
        }

        public DataContext Context =>
            _provider.GetRequiredService<DataContext>();

        public IDataReader Reader =>
            _provider.GetRequiredService<IDataReader>();

        public IDataWriter Writer =>
            _provider.GetRequiredService<IDataWriter>();
    }
}
