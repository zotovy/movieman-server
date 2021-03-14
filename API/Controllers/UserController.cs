using System.Collections.Generic;
using System.Linq;
using API.DTO;
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
        
        public IEnumerable<UserDTO> Index() {
            return _service.GetUsers().Select(UserDTO.FromDomain);
        }
    }
}