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
            var submitResponse = _applicationService.Handle(command);
            var evaluateReponse =
                _applicationService.Handle(new EvaluateApplicationCommand(submitResponse.ApplicationId));


            if (evaluateReponse != null)
            {
                return Ok(evaluateReponse);
            }

            return NotFound();
        }
    }
}
