using ErrorOr;
using MediatR;
using rendezvousBistro.Application.Authentication.Common;
using rendezvousBistro.Application.Common.Interfaces.Authentication;
using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Domain.Common.Errors;
using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository
) :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // 1. Validate the user doesn't exist
        if(_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // 2. Create user (generate unique Id) & Persist to DB
        var newUser = new User{
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
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