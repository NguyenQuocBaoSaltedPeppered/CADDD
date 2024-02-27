using ErrorOr;
using FluentResults;
using rendezvousBistro.Application.Common.Errors;
using rendezvousBistro.Application.Common.Interfaces.Authentication;
using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Domain.Common.Errors;
using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Services.Authentication;

public class AuthenticationService(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository
) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1. Validate the user does exists
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the user password is correct
        if(user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 3. Generate Jwt Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
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