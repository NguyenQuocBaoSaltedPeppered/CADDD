using ErrorOr;
using MediatR;
using rendezvousBistro.Application.Authentication.Common;
using rendezvousBistro.Application.Common.Interfaces.Authentication;
using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Domain.Common.Errors;
using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Authentication.Queries.Login;

/// <summary>
/// Login query handler
/// </summary>
public class LoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository
) :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Handle login query
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // 1. Validate the user does exists
        if(_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2. Validate the user password is correct
        if(user.Password != query.Password)
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
}