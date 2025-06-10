using lab2.Utils;
using Microsoft.AspNetCore.Http;

namespace lab2.Models
{
    public class UserContext
    {
        private readonly IHttpContextAccessor _accessor;

        public UserContext(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void SetUser(User user)
        {
            _accessor.HttpContext?.Session.SetInt32("UserId", (int)user.Id);
        }

        public User? GetUser(MyDbContext context)
        {
            var id = _accessor.HttpContext?.Session.GetInt32("UserId");
            return id.HasValue ? context.Users.Find((long)id.Value) : null;
        }

        public void Clear()
        {
            _accessor.HttpContext?.Session.Remove("UserId");
        }
    }

}
