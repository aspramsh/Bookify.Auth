using System;

namespace Bookify.Auth.Business.Models.Response
{
    public class ResponseUserClaimModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }
    }
}
