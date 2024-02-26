using FluentResults;
using rendezvousBistro.Application.Common.Errors;
using rendezvousBistro.Application.Common.Interfaces.Authentication;
using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Services.Authentication;

public class AuthenticationService(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository
) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate the user does exists
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User not found");
        }

        // 2. Validate the user password is correct
        if(user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        // 3. Generate Jwt Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }

    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            return Result.Fail<AuthenticationResult>(new[] {new DuplicateEmailError()});
        }

        // 2. Create user (generate unique Id) & Persist to DB
        var newUser = new User{
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.AddUser(newUser);

        // Create token
        var token = _jwtTokenGenerator.GenerateToken(newUser);
        return new AuthenticationResult(
            newUser,
            token
        );
    }
}