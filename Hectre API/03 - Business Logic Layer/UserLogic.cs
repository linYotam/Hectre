using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;

namespace Hectre
{
    public class UserLogic : BaseLogic
    {
        // Init crypt object
        private readonly HectreCrypt crypt = new HectreCrypt();

        // Add new user
        public async Task<UserModel?> AddUserAsync(UserModel userModel)
        {
            // Check if user already exists
            var userExist = await DB.Users.FirstOrDefaultAsync(u => u.UserEmail == userModel.Email);
            if (userExist != null) throw new Exception("User with email: '" + userModel.Email + "' is already taken");

            // Decrypt user password to original state
            string originalPassword = crypt.Decrypt(userModel.Password);

            // Encrypt the password Before saving to DB
            userModel.Password = BCrypt.Net.BCrypt.HashPassword(originalPassword);

            User user = userModel.ConvertToUser();

            DB.Users.Add(user);
            await DB.SaveChangesAsync();
            userModel.ID = user.UserId;
            return userModel;
        }

        // Login user
        public async Task<UserModel?> GetUserByCredentials(CredentialsModel credentials)
        {
            // Get user and make sure he already exist in DB
            var user = await DB.Users.SingleOrDefaultAsync(u => u.UserEmail == credentials.Email);
            if (user != null)
            {
                // Decrypt user password to original state
                string originalPassword = crypt.Decrypt(credentials.Password);

                // Compare encrypted user password to encrypted user password from DB
                bool passwordMatches = BCrypt.Net.BCrypt.Verify(originalPassword, user.UserPassword);
                if (passwordMatches)
                {
                    // return user data if login is successful
                    return new UserModel(user);
                }
            }
            return null;
        }

    }
}
