using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pkbackend.DTOs;
using pkbackend.interfaces;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace pkbackend.services
{
    public class MailJetService : IMailService
    {
        private readonly MailjetClient _mailjetclient;
        public MailJetService(MailjetClient mailjetClient)
        {
            _mailjetclient = mailjetClient;
        }

        
        public async Task<MailResponseDTO> SendMailAsync(MailDTO MailPayload)
        {
            MailjetRequest Request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }.Property(Send.Messages, new JArray {
                new JObject {
                    { "From", new JObject {
                        { "Email", "ediks05@gmail.com" },
                        { "Name", $"Website Form" }
                    }},
                    { "To", new JArray {
                        new JObject {
                            { "Email", "pithonkids@gmail.com" },
                            { "Name", "PithonKids" }
                        }
                    }},
                    { "ReplyTo", new JObject {
                        { "Email", $"{MailPayload.Email}" },
                        { "Name", $"{MailPayload.FirstName} {MailPayload.LastName}" }
                    }},
                    { "Subject", $"Service registration request from {MailPayload.FirstName} {MailPayload.LastName}" },
                    { "TextPart", $"Message: {MailPayload.Message}\n\nFrom: {MailPayload.FirstName} {MailPayload.LastName} <{MailPayload.Email}>" },
                    { "HTMLPart", $"<p>{MailPayload.Message}</p><p>From: <strong>{MailPayload.FirstName} {MailPayload.LastName}</strong> &lt;{MailPayload.Email}&gt;</p>" }
                }
            });

            MailjetResponse Response = await _mailjetclient.PostAsync(Request);
            if (Response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", Response.GetTotal(), Response.GetCount()));
                return new MailResponseDTO
                {
                    Success = true,
                    StatusCode = Response.StatusCode
                };
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", Response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", Response.GetErrorInfo()));
                Console.WriteLine(Response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", Response.GetErrorMessage()));
                return new MailResponseDTO
                {
                    Success = false,
                    StatusCode = Response.StatusCode,
                    ErrorInfo = Response.GetErrorInfo(),
                    ErrorMessage = Response.GetErrorMessage()
                };
            }
        }
    }
}