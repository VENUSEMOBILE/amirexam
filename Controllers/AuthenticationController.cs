using Anbaedari_Exam.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SQLitePCL;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Anbaedari_Exam.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        public class Product_group_characteristicsDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int? ParentGroupId { get; set; }
            public string GroupCode { get; set; } = string.Empty;

            public Product_group_characteristicsDto(int id, string name, int? parentGroupId, string groupCode)
            {
                Id = id;
                Name = name;
                ParentGroupId = parentGroupId;
                GroupCode = groupCode;
            }
        }
        public class ProductfeaturesDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int ProductGroupId { get; set; }
            public decimal Price { get; set; }
            public DateTime Expirationdate { get; set; }

            public ProductfeaturesDto(int id, string name, int productGroupId, decimal price, DateTime expirationdate)
            {
                Id = id;
                Name = name;
                ProductGroupId = productGroupId;
                Price = price;
                Expirationdate = expirationdate;
            }
        }
        public IConfiguration _configuration;
        private readonly InventoryContext _context;
        public AuthenticationController(IConfiguration configuration,InventoryContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("authenticate")]
        public  ActionResult<string> Authenticate(AuthenticationRequestBody
           authenticationRequestBody)
        {
            var user = ValidateUserCredentials(authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                );
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256
                );
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("Id", user.id.ToString()));
            claimsForToken.Add(new Claim("GroupCode", user.FirstName.ToString()));
            claimsForToken.Add(new Claim("ParentGroupId", user.ParentGroupId.ToString()));
            claimsForToken.Add(new Claim("Name", user.Name.ToString()));

            var jwtSecurityToke = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToke);
            return Ok(tokenToReturn);
        }

        private object ValidateUserCredentials(string? userName, string? password)
        {
            throw new NotImplementedException();
        }
        //[HttpPost]
        //public async Task<IActionResult> Post(Product_group_characteristicsDto _userData)
        //{

        //    if (_userData != null && _userData.Id != null && _userData.Name != null)
        //    {
        //        var user = await GetGroup(_userData.Id, _userData.Name);

        //        if (user != null)
        //        {
        //            //create claims details based on the user information
        //            var claims = new[] {
        //            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //            new Claim("Id", user.UserId.ToString()),
        //            new Claim("Name", user.Name),
        //            new Claim("ParentGroupId", user.ParentGroupId),
        //            new Claim("GroupCode", user.GroupCode)
        //           };

        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

        //            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid credentials");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //private async Task<Product_group_characteristicsDto> GetGroup(string email, string password)
        //{
        //    return await _context.Product_Group_CharacteristicsDtos.FirstOrDefaultAsync(u => u.Id == id && u.Password == password);
        //}
        //private async Task<ProductfeaturesDto> GetProduct(string email, string password)
        //{
        //    return await _context.ProductfeaturesDtos.FirstOrDefaultAsync(u => u.Id == id && u.Password == password);
        //}
    }
}
