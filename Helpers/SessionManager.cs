using EcommerceBackend.Data;
using EcommerceBackend.Models;

namespace EcommerceBackend.Helpers
{
    public class SessionManager
    {
        public void LoginUser(int userId, EcommerceBackendContext context)
        {
            User user = context.User.Where(user => user.Id == userId).FirstOrDefault();

            var session = new Session()
            {
                userId = userId,
                logonTime = DateTime.Now,
                lastInteraction = DateTime.Now,
            };

            context.Session.Add(session);
        }

        public void UserInteraction(int userId, EcommerceBackendContext context)
        {
            User user = context.User.Where(user => user.Id == userId).FirstOrDefault();

            var session = new Session()
            {
                userId = userId,
                lastInteraction = DateTime.Now,
            };

            context.Session.Add(session);
        }
    }
}
