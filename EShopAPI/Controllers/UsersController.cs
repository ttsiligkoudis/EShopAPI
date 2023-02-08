using Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DataModels.Dtos;
using DataModels;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns></returns>
        // GET: api/Users
        [HttpGet(Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<UserDto>>(users));
        }

        /// <summary>
        /// Get a specific user
        /// </summary>
        /// <param name="id"> The user's id</param>
        /// <returns></returns>
        // GET api/Users/id
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Get a specific user by email and password
        /// </summary>
        /// <param name="email"> The user's email</param>
        /// <param name="password"> The user's password</param>
        /// <returns></returns>
        // GET api/Users/GetUserByEmailAndPassword/?email=email&password=password
        [HttpGet("GetUserByEmailAndPassword", Name = "GetUserByEmailAndPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _userRepository.GetUser(email,password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Get a specific user by email
        /// </summary>
        /// <param name="email"> The user's email</param>
        /// <returns></returns>
        // GET api/Users/GetUserByEmail/?email=email
        [HttpGet("GetUserByEmail", Name = "GetUserByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUser(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="userDto"> An instance of a user</param>
        /// <returns></returns>
        // POST api/Users
        [HttpPost(Name = "PostUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user = await _userRepository.Post(user);
            _mapper.Map(user, userDto);
            return CreatedAtAction("GetUser", new { id = userDto.Id }, userDto);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"> The user's id</param>
        /// <param name="userDto"> The instance of the user</param>
        /// <returns></returns>
        // PUT api/Users/id
        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, UserDto userDto)
        {
            var userInDb = await _userRepository.GetUser(id);
            if (userInDb == null || id != userDto.Id)
            {
                return NotFound();
            }
            userInDb = _mapper.Map<User>(userDto);
            await _userRepository.Put(userInDb);
            return NoContent();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"> The user's id</param>
        /// <returns></returns>
        // DELETE api/Users/id
        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.Delete(id);
            return NoContent();
        }
    }
}
