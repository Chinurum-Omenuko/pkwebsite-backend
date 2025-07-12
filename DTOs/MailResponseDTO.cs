using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pkbackend.DTOs
{
    public class MailResponseDTO
    {
        public int StatusCode { get; set; }
        public string? ErrorInfo { get; set; }
        public string? ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}