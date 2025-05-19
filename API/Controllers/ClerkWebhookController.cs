using System.Net;
using System.Text.Json;
using API.Dto;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Svix;

namespace API.Controllers;

[Route("api/webhooks")]
public class ClerkWebhookController : BaseApiController
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;

    public ClerkWebhookController(IConfiguration configuration, IIdentityService identityService)
    {
        _configuration = configuration;
        _identityService = identityService;
    }

    [HttpPost("clerk")]
    public async Task<IActionResult> HandleClerkWebhook()
    {
        var webhookSecret = _configuration["ClerkSettings:WebhookSecret"];
        if (string.IsNullOrEmpty(webhookSecret))
        {
            return StatusCode(500, "Webhook secret not configured.");
        }
        var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
        var headers = Request.Headers;

        var svixId = headers["svix-id"].ToString();
        var svixTimestamp = headers["svix-timestamp"].ToString();
        var svixSignature = headers["svix-signature"].ToString();

        if (
            string.IsNullOrEmpty(svixId)
            || string.IsNullOrEmpty(svixTimestamp)
            || string.IsNullOrEmpty(svixSignature)
        )
        {
            return BadRequest("Missing Svix headers.");
        }

        var svixWebhook = new Webhook(webhookSecret);

        ClerkEvent? clerkEventPayload;

        try
        {
            svixWebhook.Verify(
                requestBody,
                new WebHeaderCollection
                {
                    { "svix-id", svixId },
                    { "svix-timestamp", svixTimestamp },
                    { "svix-signature", svixSignature },
                }
            );

            clerkEventPayload = JsonSerializer.Deserialize<ClerkEvent>(requestBody);
        }
        catch
        {
            return BadRequest("Invalid signature or payload");
        }

        var data = clerkEventPayload!.Data;
        switch (clerkEventPayload.Type)
        {
            case "user.created":
                await _identityService.CreateUserAsync(
                    new CreateUserRequest
                    {
                        Id = data!.Id,
                        FirstName = data.FirstName!,
                        LastName = data.LastName!,
                        Email = data.EmailAddresses[0].EmailAddress,
                    }
                );
                break;
            case "user.updated":
                await _identityService.UpdateUserAsync(
                    new CreateUserRequest
                    {
                        Id = data!.Id,
                        FirstName = data.FirstName!,
                        LastName = data.LastName!,
                        Email = data.EmailAddresses[0].EmailAddress,
                    }
                );
                break;
            case "user.deleted":
                await _identityService.DeleteUserAsync(data!.Id);
                break;
            default:
                return BadRequest("Unhandled event type");
        }
        return Ok();
    }
}
