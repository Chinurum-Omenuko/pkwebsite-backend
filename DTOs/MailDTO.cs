namespace pkbackend.DTOs
{
    public class MailDTO
    {
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public required String Email { get; set; }
        public required String Phone { get; set; }
        public required String Message { get; set; }
    }
}