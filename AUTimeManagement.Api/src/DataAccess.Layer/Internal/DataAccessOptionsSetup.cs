using Microsoft.Extensions.Options;

namespace AUTimeManagement.Api.DataAccess.Layer.Internal;

internal class DataAccessOptionsSetup : IConfigureOptions<DataAccessOptions>
{
    public void Configure(DataAccessOptions options)
    {
        if (string.IsNullOrEmpty(options.ConnectionString))
        {
            throw new ArgumentNullException(nameof(options.ConnectionString));
        }
    }
}
