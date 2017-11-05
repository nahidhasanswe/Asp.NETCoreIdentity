using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthApp.Models 
{
    public class Credentials 
    {
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

    }

    public class UserInfo 
    {
      [BsonId]
      [BsonRepresentation(BsonType.ObjectId)]
      public string Id { get; set; }
      [BsonIgnoreIfNull]
      public string Name { get; set; }
      [BsonIgnoreIfNull]
      public string Mobile { get; set; }
      [BsonIgnoreIfNull]
      public string Email { get; set; }
      [BsonIgnoreIfNull]
      public string Address { get; set; }
      [BsonIgnoreIfNull]
      public string Qualification { get; set; }
      [BsonIgnoreIfNull]
      public List<string> Certification { get; set; }
      [BsonIgnoreIfNull]
      public List<string> Expertise { get; set; }
      [BsonIgnoreIfNull]
      public List<string> Interest { get; set; }
    }
}