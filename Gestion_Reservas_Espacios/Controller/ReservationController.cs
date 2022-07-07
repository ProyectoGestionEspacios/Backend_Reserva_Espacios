using DataAccess;
using DataAccess.Repos;
using Entities;
using Gestion_Reservas_Espacios.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Claims;

namespace Gestion_Reservas_Espacios.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly IGenericRepository<Reservation> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationRepository _reservationRepository;
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;


        public ReservationController(IGenericRepository<Reservation> genericRepository, IUnitOfWork unitOfWork, IReservationRepository reservationRepository, ISendGridClient sendGridClient,
            IConfiguration configuration)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _reservationRepository = reservationRepository;
            _sendGridClient = sendGridClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await _reservationRepository.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<Reservation> Get(int id)
        {
            return await _reservationRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateReservationRequest reservation)
        {
            try
            {
                var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;

                var createReservation = new Reservation
                {
                    Date = reservation.Date,
                    SpaceId = reservation.SpaceId,
                    Description = reservation.Description,
                    UserId = userId
                };


                var newReserved = await _reservationRepository.Create(createReservation);


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


                return CreatedAtAction(nameof(Get), new { id = newReserved.Id }, newReserved);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateReservationRequest reservation)
        //{

        //    var userId = ((ClaimsIdentity)User.Identity).FindFirst("Id").Value;

        //    var updateReservation = new Reservation
        //    {                
        //        SpaceId = reservation.SpaceId,
        //        Description = reservation.Description,
        //        UserId = userId
        //    };


        //}        

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _genericRepository.DeleteAsync(x => x.Id == id);
            if (deleted) _unitOfWork.Commit();
            return Ok(deleted);

        }
    }
}
