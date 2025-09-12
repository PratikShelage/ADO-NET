namespace AngularAuthApi.Models.Dto
{
    public record ResetPasswordDTO
    {
        public string email { get; set; }
        public string emailToken { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }
}
