using Domain.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models;
using Persistence.Models.Enums;
using RestApi.Controllers.v1.InquiryController.Requests;
using RestApi.Controllers.v1.InquiryController.Response;

namespace RestApi.Controllers.v1.InquiryController;

[ApiController]
public class InquiryController : v1_BaseController
{
    IInquiryService _inquiryService;
    public InquiryController(IInquiryService inquiryService)
    {
        _inquiryService = inquiryService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IList<Inquiry>>> GetInquiries()
    {
        var claims = User.Claims;
		var userIdString = claims.FirstOrDefault(x => x.Type == "userId")!.Value;

		var isUserIdValid = Guid.TryParse(userIdString, out var userId);

        var inquiries = await _inquiryService.GetAsync(userId);
        var result = inquiries.Select(x => new InquiryResponse
        {
            Id = x.Id,
            Date = x.DateSubmitted,
            Type = x.Type,
            Status = x.Status
        }
        );

        return new OkObjectResult(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<ActionResult<IList<InquiryResponse>>> PostInquiry([FromBody] CreateInquiryRequest request)
    {
        var claims = User.Claims;
		var userIdString = claims.FirstOrDefault(x => x.Type == "userId")!.Value;

		var isUserIdValid = Guid.TryParse(userIdString, out var userId);

		var result = await _inquiryService.PostAsync(userId, request.Body!, request.Type!.Value);

        if (result is null)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        //Simulating job completion
        BackgroundJob.Schedule<IInquiryService>(x => x.UpdateAsync(result.Id, InquiryStatus.Completed), TimeSpan.FromMinutes(1));

		return new OkObjectResult(result);
	}
}
