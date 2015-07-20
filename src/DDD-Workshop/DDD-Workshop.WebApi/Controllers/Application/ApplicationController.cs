using System;
using System.Collections.Generic;
using System.Web.Http;
using DDD_Workshop.Domain;
using DDD_Workshop.Domain.Application;

namespace DDD_Workshop.WebApi.Controllers.Application
{
    public class ApplicationController : ApiController
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public IHttpActionResult PostAcceptOfferRequest(AcceptOfferRequest request)
        {
            var command = request.ToCommand();
            var acceptOfferResponse = _applicationService.Handle(command);
            if (acceptOfferResponse != null)
            {
                return Ok(acceptOfferResponse);
            }

            return NotFound();
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

    

    public class AcceptOfferRequest
    {
        public Guid ApplicationId { get; set; }

        public AcceptOfferCommand ToCommand()
        {
            return new AcceptOfferCommand(ApplicationId);
        }
    }
}
