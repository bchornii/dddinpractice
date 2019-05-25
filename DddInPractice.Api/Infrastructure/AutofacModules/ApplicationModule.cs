using Autofac;
using DddInPractice.Data.Repositories;
using DddInPractice.Domain.Aggregates.SnakMachineAggregate;
using DddInPractice.QueryHandlers;

namespace DddInPractice.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        private readonly string _connectionString;

        public ApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SnackTypesQueries(_connectionString))
                .As<ISnackTypesQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SnackMachineRepository>()
                .As<ISnakMachineRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
