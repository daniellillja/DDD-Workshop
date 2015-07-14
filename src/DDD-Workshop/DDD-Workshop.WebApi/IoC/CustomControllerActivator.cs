using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using StructureMap;

namespace DDD_Workshop.WebApi
{
    public class CustomControllerActivator : IHttpControllerActivator
    {
        private readonly IContainer _container;

        public CustomControllerActivator(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Creates a controller based on the incoming request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        public IHttpController Create(HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            // TODO: implement lifecycle scoping?
            return _container.GetInstance(controllerType) as IHttpController;
        }
    }
}