using System;
using BasicInfrastructure.Persistence;

namespace BasicInfrastructureAuthentication
{
    public class AuthToken : Entity, IAuthToken
    {
        public AuthToken()
        {
            
        }
        public AuthToken(string token)
        {
            Token = new Guid(token);
        }
        public Guid Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastTouch { get; set; }
        public long RenewInterval { get; set; }

        public bool IsValid(DateTime? when = null)
        {
            return DateTime.Compare(when ?? DateTime.Now, LastTouch.AddMinutes(RenewInterval)) < 0;
        }
    }
}
