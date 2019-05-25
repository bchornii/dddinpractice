using DddInPractice.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DddInPractice.Domain.SeedObjects;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate;

namespace DddInPractice.Api.Infrastructure.SeedData
{
    public class ContextSeed
    {
        public async Task SeedAsync(DefaultDbContext context, ILogger<ContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(DefaultDbContext));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    context.Database.Migrate();

                    if (!context.SnakTypes.Any())
                    {
                        context.SnakTypes.AddRange(Enumeration.GetAll<SnakType>());

                        await context.SaveChangesAsync();
                    }
                }
            });
        }

        private Policy CreatePolicy(ILogger<ContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} " +
                            "detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
