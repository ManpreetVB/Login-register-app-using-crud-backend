using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.BAL;
using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserBAL userBAL;
        public UserController(UserBAL BAL)
        {
            userBAL = BAL;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(userBAL.GetAllUsers());
        }
        [HttpPost]
        public IActionResult Add(AddUserDTO userDTO)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.UserName = userDTO.UserName;
            userDetails.Email = userDTO.Email;
            userDetails.Password = userDTO.Password;
            userDetails.Gender = userDTO.Gender;
            userDetails.Address = userDTO.Address;
            return Ok(userBAL.AddUser(userDetails));
        }
        [HttpPut]
        public IActionResult Update(UpdateUserDTO userDTO)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.UserId = userDTO.UserId;
            userDetails.UserName = userDTO.UserName;
            userDetails.Email = userDTO.Email;
            userDetails.Password = userDTO.Password;
            userDetails.Gender = userDTO.Gender;
            userDetails.Address = userDTO.Address;
            userDetails.IsActive = userDTO.IsActive;
            return Ok(userBAL.UpdateUser(userDetails));
        }
        [HttpGet("{UserId}")]
        public IActionResult GetUserById(int UserId)
        {
           return Ok(userBAL.GetUserById(UserId));
        }

        [HttpDelete]
        public IActionResult DeleteUser(int UserId)
        {
            return Ok(userBAL.DeleteUser(UserId));
        }
    }
}

