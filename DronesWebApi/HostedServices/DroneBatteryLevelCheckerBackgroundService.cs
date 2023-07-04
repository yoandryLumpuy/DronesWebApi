using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Threading;
using System;
using DronesWebApi.Commons.Configuration;
using DronesWebApi.Commons.Constants;
using DronesWebApi.Core;
using Microsoft.Extensions.DependencyInjection;
using DronesWebApi.Persistence;

namespace DronesWebApi.HostedServices
{
    public class DroneBatteryLevelCheckerBackgroundService: BackgroundService
    {
        private readonly IServiceProvider _servicesProvider;
        private readonly ILogger<DroneBatteryLevelCheckerBackgroundService> _logger;
        private readonly BackgroundServicesOptions _options;

        public DroneBatteryLevelCheckerBackgroundService(
            IServiceProvider servicesProvider, IOptions<BackgroundServicesOptions> options, 
            ILogger<DroneBatteryLevelCheckerBackgroundService> logger)
        {

            _options = options?.Value ?? new BackgroundServicesOptions();
            _servicesProvider = servicesProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking of drones' battery level started.");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _servicesProvider.CreateScope();

                    var provider = scope.ServiceProvider;

                    var unitOfWork = provider.GetRequiredService<IUnitOfWork>();

                    foreach (var drone in unitOfWork.Drones.GetAll())
                        if (drone.BatteryCapacityInPercentage < Constants.MinPercentageOfBatteryLevelRequired)
                            _logger.LogInformation($"Battery level = '{drone.BatteryCapacityInPercentage}' in Drone with id '{drone.Id}' " +
                                                   $"is below required value of '{Constants.MinPercentageOfBatteryLevelRequired}'. ");
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"An Error occurred while trying to check drones' battery level");
                }

                await Task.Delay(TimeSpan.FromSeconds(_options.IntervalInSeconds), cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
