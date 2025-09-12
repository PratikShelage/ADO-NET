namespace AngularAuthApi.Models.Dto
{
    public class EmailDTO
    {
        public string to { get; set; }
        public string subject { get; set; }

        public string contact { get; set; }

        public EmailDTO(string To,string Subject,string Contact)
        {           
            this.to = To;
            this.subject = Subject;
            this.contact = Contact;
        }
    }
}
