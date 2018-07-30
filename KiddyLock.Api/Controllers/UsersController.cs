using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Logic;
using Core.Models;
using Swashbuckle.Swagger.Annotations;

namespace KiddyLock.Api.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public IEnumerable<User> Get()
        {
            return UserLogic.GetUsers();
        }
    }
}
