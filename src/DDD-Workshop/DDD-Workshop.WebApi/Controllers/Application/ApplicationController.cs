using System.Collections.Generic;
using System.Web.Http;
using DDD_Workshop.Domain;

namespace DDD_Workshop.WebApi.Controllers.Application
{
    public class ApplicationController : ApiController
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IHttpActionResult PostSubmitApplicationRequest(SubmitApplicationRequest request)
        {
            var command = new ApplicationSubmittedCommand(request.FirstName, request.LastName);
            var response = _applicationService.Handle(command);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound();
        }
    }
}
