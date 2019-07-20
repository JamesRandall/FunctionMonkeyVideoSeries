using System;
using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace ServerlessBlog.CrossCuttingConcerns
{
    internal class CustomDispatcher : ICommandDispatcher
    {
        private readonly ILogger _logger;
        private readonly IFrameworkCommandDispatcher _underlyingDispatcher;
        private readonly CloudBlobContainer _blobContainer;

        public CustomDispatcher(ILogger logger, IFrameworkCommandDispatcher underlyingDispatcher)
        {
            _logger = logger;
            _underlyingDispatcher = underlyingDispatcher;

            _blobContainer = CloudStorageAccount
                .DevelopmentStorageAccount
                .CreateCloudBlobClient()
                .GetContainerReference("commandhistory");
        }
        
        public async Task<CommandResult<TResult>> DispatchAsync<TResult>(
            ICommand<TResult> command,
            CancellationToken cancellationToken = new CancellationToken())
        {
            Guid commandId = await PersistCommand(command);
            try
            {
                var result = await _underlyingDispatcher.DispatchAsync(command, cancellationToken);
                _logger.LogInformation("Executed command {commandType} with ID {commandId}",
                    command.GetType().Name,
                    commandId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error executing command {commandType} with ID {commandId}",
                    command.GetType().Name,
                    commandId);
                throw;
            }
        }

        public async Task<CommandResult> DispatchAsync(
            ICommand command,
            CancellationToken cancellationToken = new CancellationToken())
        {
            Guid commandId = await PersistCommand(command);
            try
            {
                var result = await _underlyingDispatcher.DispatchAsync(command, cancellationToken);
                _logger.LogInformation("Executed command {commandType} with ID {commandId}",
                    command.GetType().Name,
                    commandId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error executing command {commandType} with ID {commandId}",
                    command.GetType().Name,
                    commandId);
                throw;
            }
        }

        public ICommandExecuter AssociatedExecuter => null;

        private async Task<Guid> PersistCommand(ICommand command)
        {
            string json = JsonConvert.SerializeObject(command);
            Guid commandId = Guid.NewGuid();
            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference($"{commandId}.json");
            await blob.UploadTextAsync(json);
            
            _logger.LogInformation("Executing command {commandType} with ID {commandId}",
                command.GetType().Name,
                commandId);

            return commandId;
        }
    }
}