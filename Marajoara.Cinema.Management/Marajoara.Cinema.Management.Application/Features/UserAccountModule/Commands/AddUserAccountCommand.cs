namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public abstract class AddUserAccountCommand
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        //public string Password { get; set; }
    }
}
