using System;

namespace Bookify.Auth.Business.Models.Response
{
    public class ResponseUserClaimModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
