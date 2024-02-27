using ErrorOr;

namespace rendezvousBistro.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict(
                code: "user/duplicate-email",
                description: "Email is already in use"
            );
        }
    }
}