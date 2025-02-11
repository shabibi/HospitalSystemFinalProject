namespace HospitalSystemTeamTask.Shared.Helper
{
    public static class GlobalState
    {
        public static int UID { get; set; }
        public static string Name { get; set; }
        public static bool IsAuthenticated { get; set; } = false; 
        public static string Role { get; set; } // E.g., "Patient", "Doctor", "Staff"

        public static event Action OnAuthStateChanged;

        public static void NotifyAuthStateChanged()
        {
            OnAuthStateChanged?.Invoke();
        }
    }
}
