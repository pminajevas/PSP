namespace PoS.Application.Models.Responses
{
    public class UserResponse
    {
        public Guid? Id { get; set; }

        public string LoginName { get; set; }

        public string JwtToken { get; set; }

        public string RoleName {  get; set; }
    }
}
