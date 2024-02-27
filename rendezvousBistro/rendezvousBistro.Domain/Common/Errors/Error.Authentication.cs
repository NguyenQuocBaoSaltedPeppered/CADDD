using ErrorOr;

namespace rendezvousBistro.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "auth/invalid-cred",
                description: "Invalid credentials"
            );
        }
    }
}