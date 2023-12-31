﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hectre
{
    public class UserModel : IValidatableObject
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Missing User Name")]
        [MinLength(2, ErrorMessage = "Name must be a minimum of 2 chars")]
        [MaxLength(250, ErrorMessage = "Name can't exceeds 250 chars")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Missing Email")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Missing User Password")]
        public string Password { get; set; } = null!;

        public string? Type { get; set; } = "customer";

        public string JwtToken { get; set; } = "";

        public UserModel() { }

        // Set all UserModel params
        public UserModel(User user)
        {
            ID = user.UserId;
            Name = user.UserName;
            Email = user.UserEmail;
            Password = user.UserPassword;
            Type = user.UserType;
            JwtToken = user.JwtToken;
        }

        // Convert UserModel params to User params
        public User ConvertToUser()
        {

            return new User
            {
                UserId = ID,
                UserName = Name,
                UserEmail = Email,
                UserPassword = Password,
                UserType = Type,
                JwtToken = JwtToken,
            };
        }

        // Custom validation of Name, Password and Email as one
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Name == "" && Password == "" && Email == "")
            {
                List<string> members = new List<string> { nameof(Name), nameof(Password), nameof(Email) };
                errors.Add(new ValidationResult($"All {nameof(Name)} and {nameof(Password)} and {nameof(Email)} must have valid values", members));
            }
            return errors;
        }
    }
}
