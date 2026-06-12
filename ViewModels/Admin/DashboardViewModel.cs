namespace KUETResearchSociety.ViewModels.Admin
{
    public class DashboardViewModel
    {
        public int TotalTeamMembers { get; set; }
        public int TotalActivities { get; set; }
        public int TotalEvents { get; set; }
        public int TotalGalleryItems { get; set; }
        public int TotalMessages { get; set; }
        public int UnreadMessages { get; set; }
        public List<KUETResearchSociety.Models.ContactMessage> RecentMessages { get; set; } = new();
    }
}
