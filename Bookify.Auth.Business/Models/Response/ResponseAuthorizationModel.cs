using System;
using System.Collections.Generic;

namespace Bookify.Auth.Business.Models.Response
{
    public class ResponseAuthorizationModel
    {
        /// <summary>
        /// Guid of User
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        public string Email { get; set; }

        public List<ResponseUserClaimModel> Claims { get; set; }
    }
}
