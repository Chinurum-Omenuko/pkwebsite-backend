using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pkbackend.DTOs;
using pkbackend.interfaces;

namespace pkbackend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        public IMailService _mailservice;
        public MailController(IMailService mailService)
        {
            _mailservice = mailService;
        }

        [HttpPost]
        public Task<MailResponseDTO> PostMail([FromBody] MailDTO MailPayload)
        {
            return _mailservice.SendMailAsync(MailPayload);
        }
    }
}