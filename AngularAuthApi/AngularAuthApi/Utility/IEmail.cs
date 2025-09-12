using AngularAuthApi.Models.Dto;

namespace AngularAuthApi.Utility
{
    public interface IEmail
    {
        void SendEmail(EmailDTO emailDTO);
    }
}
