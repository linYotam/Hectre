using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hectre
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase, IDisposable
    {

        private readonly JwtHelper jwtHelper;
        private readonly UserLogic logic;

        // Init jwtHelper and user logic
        public AuthController(JwtHelper jwtHelper, UserLogic logic)
        {
            this.jwtHelper = jwtHelper;
            this.logic = logic;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserModel userModel) // Add new user
        {
            try
            {
                // Add new user to DB
                UserModel? addedUser = await logic.AddUserAsync(userModel);

                // Set user token
                addedUser.JwtToken = jwtHelper.GetJwtToken(addedUser.Email, addedUser.Type);

                return Created("api/users/" + addedUser.ID, addedUser);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(CredentialsModel model)
        {
            try
            {
                // Check if user exist 
                UserModel? user = await logic.GetUserByCredentials(model);

                // If user not exist --> return Unauthorized status
                if (user == null) return Unauthorized("Incorrect email or password");

                // Set user token
                user.JwtToken = jwtHelper.GetJwtToken(user.Email, user.Type);

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public void Dispose()
        {

            logic.Dispose();

        }
    }
}
