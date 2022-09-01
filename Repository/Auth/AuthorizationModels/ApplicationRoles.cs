using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Repository.Auth.AuthorizationModels
{
    [CollectionName("Roles")]
    public class ApplicationRoles : MongoIdentityRole<Guid>
    {
        public ApplicationRoles() : base() { }
        public ApplicationRoles(string roleName) : base(roleName)
        {

        }
    }
}
