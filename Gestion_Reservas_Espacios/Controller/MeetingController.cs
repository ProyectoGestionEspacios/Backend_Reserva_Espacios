using DataAccess;
using DataAccess.Repos;
using Entities;
using Gestion_Reservas_Espacios.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Claims;

namespace Gestion_Reservas_Espacios.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IGenericRepository<Meeting> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMeetingRepository _meetingRepository;
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;


        public MeetingController(IGenericRepository<Meeting> genericRepository, IUnitOfWork unitOfWork, IMeetingRepository meetingRepository, ISendGridClient sendGridClient,
            IConfiguration configuration)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _meetingRepository = meetingRepository;
            _sendGridClient = sendGridClient;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<IEnumerable<Meeting>> Get()
        {
            return await _meetingRepository.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<Meeting> Get(int id)
        {
            return await _meetingRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateMeetingRequest meeting)
        {
            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;

                var createMeeting = new Meeting
                {
                    StartHour = meeting.StartHour,
                    EndHour = meeting.EndHour,
                    Description = meeting.Description,
                    UserId = userId
                };


                var newReserved = await _meetingRepository.Create(createMeeting);
                if (newReserved.User != null)
                {

                    //Envio email
                    string fromEmail = _configuration.GetSection("SendGridEmailSettings")
            .GetValue<string>("FromEmail");

                    string fromName = _configuration.GetSection("SendGridEmailSettings")
                    .GetValue<string>("FromName");

                    var msg = new SendGridMessage()
                    {
                        From = new EmailAddress(fromEmail, fromName),
                        Subject = "Reserva Bravent",
                        PlainTextContent = "Confirmamos que su reserva se ha completado correctamente."
                    };
                    msg.AddTo(newReserved.User.Email);
                    var response = await _sendGridClient.SendEmailAsync(msg);
                    string message = response.IsSuccessStatusCode ? "Email Send Successfully" :
                    "Email Sending Failed";
                }

                return CreatedAtAction(nameof(Get), new { id = newReserved.Id }, newReserved);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _genericRepository.DeleteAsync(x => x.Id == id);
            if (deleted) _unitOfWork.Commit();
            return Ok(deleted);

        }
    }
}
