using System;

namespace BasicInfrastructureAuthentication
{
    public interface IAuthToken
    {
        Guid Token { get; set; }
        Guid UserId { get; set; }
        DateTime CreateTime { get; set; }
        DateTime LastTouch { get; set; }
        long RenewInterval { get; set; }
        bool IsValid(DateTime? when = null);
    }
}