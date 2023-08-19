namespace ISP.Application.Requests.Identity.User;

public sealed class DeleteUserRequest
{
    public string Id { get; set; }
    public string PhoneNumber { get; set; }
}