using KUETResearchSociety.Models;

namespace KUETResearchSociety.ViewModels
{
    public class HomeViewModel
    {
        public SiteSettings Settings { get; set; } = new();
        public List<TeamMember> TeamMembers { get; set; } = new();
        public List<Activity> Activities { get; set; } = new();
        public List<Event> UpcomingEvents { get; set; } = new();
        public List<GalleryItem> GalleryItems { get; set; } = new();
    }
}
