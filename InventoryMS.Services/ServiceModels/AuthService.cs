using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Models.Entities.ApplicationUserModel.Dto;
using InventoryMS.Models.Response;
using InventoryMS.Services.IServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace InventoryMS.Services.ServiceModels
{
    public class AuthService(InventoryMSDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor, string secretKey) : IAuthService
    {
        public bool IsUniqueUser(string phoneNumber)
        {
            var user = context.ApplicationUsers.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            if(user == null)
            {
                return true;

            }
            return false;
            
        }

        public async Task<ApiResponse> Login(LoginRequestDto request)
        {
            var response = new ApiResponse();
            var loginRes = new LoginResponse();
            try
            {
                if (request == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
                var user = context.ApplicationUsers?.FirstOrDefault(u => u.UserName.ToLower() == request.UserName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
                bool isValid = await userManager.CheckPasswordAsync(user, request.Password);
                if (isValid == false)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
                var roles = await userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);
                //var secretKeyBytes = Environment.GetEnvironmentVariable("TokenSetting:SecretKey");
                //Console.WriteLine(secretKeyBytes);
                //if (string.IsNullOrEmpty(secretKeyBytes)) throw new InvalidOperationException("Security Configuration Missing");
                //var tokenExpire = request.RememberMe ? DateTime.UtcNow.AddDays(10) : DateTime.UtcNow.AddMinutes(30);
                //TimeSpan atExpirySpan = request.RememberMe ? TimeSpan.FromDays(10) : TimeSpan.FromMinutes(30);
                //var now = DateTime.UtcNow;
                //DateTime tokenExpire = now + atExpirySpan;

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                        ]),
                    //Expires = tokenExpire,
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };

                var token = tokenHandler.CreateToken(tokenDescription);


                loginRes.UserId = user.Id;
                loginRes.Role = roles.FirstOrDefault();
                loginRes.Token = tokenHandler.WriteToken(token);
                //loginRes.TokenExpire = tokenExpire.ToLocalTime();

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Login Successful";
                response.Results = loginRes;
                return response;
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }
        public async Task<ApiResponse> LoginWithCookieAuth(LoginRequestDto request)
        {
            var response = new ApiResponse();
            var loginRes = new LoginResponse();
            try
            {
                var user = context.ApplicationUsers?.FirstOrDefault(u => u.UserName.ToLower() == request.UserName.ToLower());
                if(user == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
                bool isValid = await userManager.CheckPasswordAsync(user, request.Password);
                if (isValid == false)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect";
                    return response;
                }
                var roles = await userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);
                var tokenExpire = request.RememberMe ? DateTime.UtcNow.AddDays(10) : DateTime.UtcNow.AddMinutes(30);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                        ]),
                    Expires = tokenExpire,
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };

                var token = tokenHandler.CreateToken(tokenDescription);
                var accessToken = tokenHandler.WriteToken(token);

                //long-lived refresh token (valid for 10-30 days)
                var refreshToken = Guid.NewGuid().ToString();
                var refreshTokenExpire = request.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1);

                // Store the refresh token and its expiration in the database
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = refreshTokenExpire;
                await context.SaveChangesAsync();

                //set cookies
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = tokenExpire,
                    SameSite = SameSiteMode.Strict,
                    Secure = true // Set to true in production for HTTPS
                };
                httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", accessToken, cookieOptions);


                //set refresh token cookies
                var refreshOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = refreshTokenExpire,
                    SameSite = SameSiteMode.Strict,
                    Secure = true // Set to true in production for HTTPS
                };
                httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", refreshToken, refreshOptions);

                //set session
                var sessionToken = Guid.NewGuid().ToString();
                var sessionExpire = request.RememberMe ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddMinutes(30);
                var sessionOptions = new CookieOptions
                {
                    HttpOnly = false,
                    Expires = sessionExpire,
                    Secure = true,
                    SameSite = SameSiteMode.Strict

                };
                httpContextAccessor.HttpContext.Response.Cookies.Append("session_token", sessionToken, sessionOptions);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Login Successful";

                return response;
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<ApiResponse> Registration(RegistrationReqDto request)
        {
            var response = new ApiResponse();
            try
            {
                if (request == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid registration data";
                    return response;
                }
                ApplicationUser user = new()
                {
                    UserName = request.UserName,
                    FullName = request.FullName,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Address = request.Address
                };

                var resultRes = await userManager.CreateAsync(user, request.Password);

                if (resultRes.Succeeded)
                {
                    var roleAssigned = await userManager.AddToRoleAsync(user, request.Role);


                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "User created successfully.";
                    //return response;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = $"{string.Join("\n", resultRes.Errors.Select(s => s.Code))}\n{string.Join("\n", resultRes.Errors.Select(s => s.Description))}";
                }

                return response;


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex?.Message + ex?.InnerException?.Message;
                return response;
            }
        }

        public Task<ApiResponse> ResetPassword()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdatePassword()
        {
            throw new NotImplementedException();
        }
    }
}
