using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using App.BLL.Mappers.Identity;
using App.Contracts.BLL;
using App.DAL.EF;
using App.DAL.EF.Mappers.Identity;
using App.Domain.Identity;
using Base.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Public.DTO.v1.Identity;
using App.Public.Mappers.Identity;
using AppUser = App.Public.DTO.v1.Identity.AppUser;
using AppUserMapper = App.Public.Mappers.Identity.AppUserMapper;

namespace WebApp.ApiControllers.Identity;

[ApiVersion( "1.0" )]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<App.Domain.Identity.AppUser> _signInManager;
    private readonly UserManager<App.Domain.Identity.AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _rnd = new Random();
    // private readonly AppDbContext _context;
    
    private readonly IAppBLL _bll;
    
    
    public AccountController(SignInManager<App.Domain.Identity.AppUser> signInManager,
        UserManager<App.Domain.Identity.AppUser> userManager,
        ILogger<AccountController> logger,
        IConfiguration configuration, IAppBLL bll)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _configuration = configuration;
        // _context = context;
        _bll = bll;
    }

    /// <summary>
    /// Get AppUser data by user id
    /// </summary>
    /// <param name="userId">Supply user id</param>
    /// <returns>AppUser by id</returns>
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [ProducesResponseType( typeof( AppUser ), StatusCodes.Status200OK )] 
    [ProducesResponseType( StatusCodes.Status404NotFound )] 
    [HttpGet("{userId}")]
    public async Task<ActionResult<AppUser>> GetUserById(Guid userId)
    {
        var res = await _bll.AppUsers.FirstOrDefaultAsync(userId);
        return AppUserMapper.MapFromBll(res!);
    }

    /// <summary>
    /// Login into the rest backend - generates JWT to be included in
    /// Authorize: Bearer xyz
    /// </summary>
    /// <param name="loginData">Supply email and password</param>
    /// <returns>JWT and refresh token</returns>
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [ProducesResponseType( typeof( Login ), StatusCodes.Status200OK )] 
    [ProducesResponseType( typeof( Login ), StatusCodes.Status404NotFound)] 
    [HttpPost]
    public async Task<ActionResult<JwtResponse>> LogIn([FromBody] Login loginData)
    {
        // verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            var errorResponse = new RestApiErrorResponse
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["email"] = new List<string>()
                    {
                        "User/Password problem"
                    }
                }
            };

            Console.WriteLine("ERROR 404!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return NotFound(errorResponse);
        }
        // verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["login"] = new List<string>()
            {
                "User/Password problem"
            };
            return NotFound(errorResponse);
        }
        
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["claims"] = new List<string>()
            {
                "User/Password problem"
            };
            return NotFound(errorResponse);
        }
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
            );

        // var asfds = await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
        //     .Query()
        //     .Where(t => t.AppUserId == appUser.Id)
        //     .ToListAsync();
        var refreshTokensList = (await _bll.RefreshTokens.GetTokensByUserId(appUser.Id)).ToList();
        // appUser.RefreshTokens = refreshTokensList;
        // var domainRefreshTokenList = new List<App.Domain.Identity.RefreshToken>();
        // foreach (var token in refreshTokensList!)
        // {
        //     // var a = RefreshTokenMapperBLL.MapFromBll(token);
        //     // var b = RefreshTokenMapperDAL.MapFromDAL(a);
        //     // domainRefreshTokenList.Add(b);
        // }

        // appUser.RefreshTokens = domainRefreshTokenList;


        if (refreshTokensList.Count > 0)
        {
            foreach (var userRefreshToken in refreshTokensList!)
            {
                if (userRefreshToken.TokenExpirationDateTime < DateTime.UtcNow
                    && userRefreshToken.PreviousTokenExpirationDateTime < DateTime.UtcNow)
                {
                    _bll.RefreshTokens.Remove(userRefreshToken);
                }
            }
        }

        var refreshToken = new App.BLL.DTO.Identity.RefreshToken();
        // _bll.RefreshTokens.Add(refreshToken);
        // await _bll.SaveChangesAsync();
        
        // var res = new JwtResponse()
        // {
        //     Token = jwt,
        //     RefreshToken = refreshToken.Token
        // };
        
        // make new refresh token, obsolete old ones
        if (refreshTokensList != null && refreshTokensList.Count != 0)
        {
            refreshToken = refreshTokensList.First();
            refreshToken.Id = new Guid();
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);
            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);
            _bll.RefreshTokens.Add(refreshToken);
            await _bll.SaveChangesAsync();
        }
        else
        {
            refreshToken = new App.BLL.DTO.Identity.RefreshToken()
            {
                AppUserId = appUser.Id,
                Token = Guid.NewGuid().ToString(),
                TokenExpirationDateTime = DateTime.UtcNow.AddDays(7)
            };
            // refreshTokensList = new List<App.BLL.DTO.Identity.RefreshToken>();
            _bll.RefreshTokens.Add(refreshToken);
            await _bll.SaveChangesAsync();
        }

        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Email = appUser.Email,
            AppUserId = appUser.Id
            
        };
        return Ok(res);
    }

    /// <summary>
    /// Register user in the rest backend - generates JWT
    /// to be included in Authorized: Bearer xyz
    /// </summary>
    /// <param name="registrationData">Supply email, password, firstname and lastname</param>
    /// <returns>JWT and refresh token</returns>
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [ProducesResponseType( typeof( Login ), StatusCodes.Status200OK )] 
    [ProducesResponseType( typeof( Login ), StatusCodes.Status404NotFound)] 
    [ProducesResponseType( typeof( Login ), StatusCodes.Status400BadRequest)] 
    [HttpPost]
    public async Task<ActionResult<JwtResponse>> Register(Register registrationData)
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["email"] = new List<string>()
            {
                "Email already registered"
            };
            return BadRequest(errorResponse);
        }

        var refreshToken = new App.Domain.Identity.RefreshToken();
        appUser = new App.Domain.Identity.AppUser()
        {
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName,
            Email = registrationData.Email,
            UserName = registrationData.Email,
            RefreshTokens = new List<App.Domain.Identity.RefreshToken>()
            {
                refreshToken
            }
        };
        
        // create user (system will do it)
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        result = await _userManager.AddClaimAsync(appUser, new Claim("aspnet.firstname", appUser.FirstName));
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        result = await _userManager.AddClaimAsync(appUser, new Claim("aspnet.lastname", appUser.LastName));
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        // get full user from system with fixed data
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User with email {} is not found after registration", registrationData.Email);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["email"] = new List<string>()
            {
                $"User with email {registrationData.Email} is not found after registration"
            };
            return BadRequest(errorResponse);
        }
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", registrationData.Email);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["claims"] = new List<string>()
            {
                "User/Password problem"
            };
            return NotFound(errorResponse);
        }
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Email = appUser.Email
        };

        return Ok(res);
    }

    /// <summary>
    /// Generates refreshed jwt and refresh token,
    /// Authorize: Bearer xyz
    /// </summary>
    /// <param name="refreshTokenModel">Supply jwt and refresh token</param>
    /// <returns>New token and refresh token and also user firstname and last name</returns>
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [ProducesResponseType( typeof( Login ), StatusCodes.Status200OK )] 
    [ProducesResponseType( StatusCodes.Status404NotFound)] 
    [ProducesResponseType( StatusCodes.Status400BadRequest)] 
    [ProducesResponseType( StatusCodes.Status500InternalServerError)] 
    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
    {
        JwtSecurityToken jwtToken;
        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "App error",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                };

                errorResponse.Errors["jwt"] = new List<string>()
                {
                    "No token"
                };
                return BadRequest(errorResponse);
            }
        }
        catch (Exception e)
        {
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["token"] = new List<string>()
            {
                $"Cant parse the token, {e.Message}"
            };
            return BadRequest(errorResponse);
        }
        
        // validate token signature

        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["email"] = new List<string>()
            {
                "No email in jwt"
            };
            return BadRequest(errorResponse);
        }
        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["email"] = new List<string>()
            {
                $"User with email {userEmail} not found"
            };
            return NotFound(errorResponse);
        }
        // load and compare refresh tokens
        var refreshTokens = (await _bll.RefreshTokens.GetTokensByUserId(appUser.Id)).ToList();
        // await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
        //     .Query()
        //     .Where(x =>
        //         (x.Token == refreshTokenModel.RefreshToken && x.TokenExpirationDateTime > DateTime.UtcNow) ||
        //         (x.PreviousToken == refreshTokenModel.RefreshToken && x.PreviousTokenExpirationDateTime > DateTime.UtcNow)
        //         )
        //     .ToListAsync();
        var validRefreshTokens = new List<App.BLL.DTO.Identity.RefreshToken>();
        if (refreshTokens != null && refreshTokens.Count != 0)
        {
            foreach (var token in refreshTokens)
            {
                if (
                    (token.Token == refreshTokenModel.RefreshToken && token.TokenExpirationDateTime > DateTime.UtcNow) ||
                    (token.PreviousToken == refreshTokenModel.RefreshToken && token.PreviousTokenExpirationDateTime > DateTime.UtcNow)
                    )
                {
                    validRefreshTokens.Add(token);
                }
            }
        }
        else
        {
            return Problem("RefreshTokens collection is null");
        }
        if (validRefreshTokens.Count == 0)
        {
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");
        }        
        if (validRefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found.");
        }
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", userEmail);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["claims"] = new List<string>()
            {
                "User/Password problem"
            };
            return NotFound(errorResponse);
        }
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );
        
        // make new refresh token, obsolete old ones
        var refreshToken = validRefreshTokens.First();
        if (refreshToken.Token == refreshTokenModel.RefreshToken)
        {
            refreshToken.Id = new Guid();
            
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);
            
            // refreshToken = refreshTokensList.First();
            // refreshToken.Id = new Guid();
            // refreshToken.PreviousToken = refreshToken.Token;
            // refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);
            // refreshToken.Token = Guid.NewGuid().ToString();
            // refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);
            // _bll.RefreshTokens.Add(refreshToken);
            // await _bll.SaveChangesAsync();

            _bll.RefreshTokens.Add(refreshToken);
            await _bll.SaveChangesAsync();
        }
        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            AppUserId = appUser.Id,
            Email = appUser.Email
        };

        return Ok(res);
    }
}