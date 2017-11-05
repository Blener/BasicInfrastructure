namespace BasicInfrastructureAuthentication
{
    public interface IAuthable
    {
        string Login { get; set; }
        string Password { get; set; }
    }
}
