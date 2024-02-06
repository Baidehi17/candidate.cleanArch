namespace Candidate.API.Utils
{
    public static class AppRole
    {
        public const string TaskUser = "TaskUser";
        public const string TaskAdmin = "TaskAdmin";
    }

    public static class AuthorizationPolicies
    {
        public const string AssignmentToTaskUserRoleRequired = "AssignmentToTaskUserRoleRequired";
        public const string AssignmentToTaskAdminRoleRequired = "AssignmentToTaskAdminRoleRequired";
    }
}
