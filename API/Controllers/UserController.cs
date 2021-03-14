using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.User;

namespace API.Controllers {
    [ApiController]
    [Route("/users")]
    public class UserController : Controller {
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public UserController(IUserService service, ILogger<UserController> logger) {
            _service = service;
            _logger = logger;
        }
        
        public IEnumerable<User> Index() {
            return _service.GetUsers();
        }
    }
}