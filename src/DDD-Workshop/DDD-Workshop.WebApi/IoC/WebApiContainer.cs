using StructureMap;

namespace DDD_Workshop.WebApi.IoC
{
    public class WebApiContainer: Container
    {
        public WebApiContainer()
            : base(new WebApiRegistry())
        {
            
        }
    }
}