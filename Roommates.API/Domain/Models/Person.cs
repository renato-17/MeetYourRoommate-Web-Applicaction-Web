using AutoMapper.Configuration.Annotations;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roommates.API.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate    { get; set; }
        public string Mail { get; set; }
        public string Discriminator { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string Token { get; set; }

        //Relation with Comment
        public IList<Comment> Comments { get; set; } = new List<Comment>();

        //Relation with Friend Requests Sent
        public IList<Request> RequestsSent { get; set; } = new List<Request>();

        //Relation with Friend Request Received
        public IList<Request> RequestsReceived { get; set; } = new List<Request>();
    }
}
