using System.Reflection;
using System.IO;
using Gamu.Classmaters.Query.Handlers;
using LightInject;
using MediatR;
using MediatR.Pipeline;
using Gamu.Classmaters.Data.Interfaces;
using Gamu.Classmaters.Data.Models;
using System.Collections.Generic;
using Gamu.Classmaters.Data.Implementation.Mappers;
using Gamu.Classmaters.Data.Implementation;

namespace Gamu.Classmaters.Test.Cqrs
{
    public class BaseTestClass
    {
        protected IMediator BuildMediator()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IMediator, Mediator>();
            serviceContainer.Register<IMapper<IList<object>, Person>, PersonMapper>();
            serviceContainer.Register<IRepository<Person>, PersonRepository>();

            serviceContainer.RegisterAssembly(typeof(GetAllPersonsQueryHandler).GetTypeInfo().Assembly, (serviceType, implementingType) =>
                serviceType.IsConstructedGenericType &&
                (
                    serviceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                    serviceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                ));

            serviceContainer.RegisterOrdered(typeof(IPipelineBehavior<,>),
                new[]
                {
                    typeof(RequestPreProcessorBehavior<,>),
                    typeof(RequestPostProcessorBehavior<,>),
                }, type => new PerContainerLifetime());


            serviceContainer.Register<ServiceFactory>(fac => fac.GetInstance);
            return serviceContainer.GetInstance<IMediator>();
        }
    }
}
