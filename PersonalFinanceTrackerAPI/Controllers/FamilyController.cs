using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerAPI.DataTransferObjects.Requests;
using PersonalFinanceTrackerAPI.Mapping.Requests;
using PersonalFinanceTrackerDataAccess.Entities;
using PersonalFinanceTrackerDataAccess.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PersonalFinanceTrackerAPI.Authentication;
using System.Net.WebSockets;

namespace PersonalFinanceTrackerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly FamilyService _familyService;
        private readonly UserHandler _userHandler;
        private readonly JwtProvider _jwtProvider;

        public FamilyController(FamilyService familyService, UserHandler userHandler, JwtProvider jwtProvider)
        {
            _familyService = familyService;
            _userHandler = userHandler;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFamily(CreateFamilyRequestDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Family inputFamily = input.ToFamily();
                int familyId = await _familyService.CreateFamilyAsync(inputFamily);
                return Ok($"FamilyId = {familyId}");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Unexpected Server Error.");
            }
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteUserToFamily(InviteUserToFamilyRequestDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var senderId = _userHandler.GetUserId();
            if (senderId == null)
            {
                return Unauthorized("User not logged in.");
            }

            try
            {
                FamilyInvitation inputFamilyInvite = input.ToFamilyInvitation();
                inputFamilyInvite.SenderId = senderId;

                string token = _jwtProvider.GenerateInvitationToken(inputFamilyInvite);

                await _familyService.InviteUserToFamilyAsync(inputFamilyInvite, token);
                return Ok("User invited to family.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Unexpected Server Error.");
            }
        }
    }
}
