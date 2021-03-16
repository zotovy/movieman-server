using System;
using System.Collections.Generic;

namespace API.DTO.User {
    public class UserAuthTokensDTO {
        public long id { get; set; }
        public Dictionary<string, string> tokens { get; set; } 

        public UserAuthTokensDTO() { }

        public static LoginRequestNotFound NotFound => new LoginRequestNotFound();

        public UserAuthTokensDTO(long id, string access, string refresh) {
            this.id = id;
            tokens = new Dictionary<string, string> {
                { "access", access },
                { "refresh", refresh }
            };
        }
    }

    public class LoginRequestNotFound {
        public bool success => false;
        public string error => "not-found-error";
    }
    
    
}