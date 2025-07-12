using Microsoft.AspNetCore.Mvc;
using pkbackend.DTOs;

namespace pkbackend.interfaces
{
    public interface IMailService
    {
        Task<MailResponseDTO> SendMailAsync(MailDTO Payload);
    }
}