using QLNH_Client.Models;

namespace QLNH_Client.Services
{
    public interface ITokenSerivce
    {
        string CreateToken(User user);
    }
}
