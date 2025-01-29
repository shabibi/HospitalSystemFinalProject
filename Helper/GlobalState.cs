namespace HospitalSystemTeamTask.Helper
{
    public static class GlobalState
    {
        public static int UID { get; set; }
        public static string Name { get; set; }
        public static bool IsAuthenticated { get; set; }
        public static string Role { get; set; } // E.g., "Patient", "Doctor", "Staff"
    }
}
