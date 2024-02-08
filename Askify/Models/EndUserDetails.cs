namespace Askify.Models
{
    public class EndUserDetails
    {
        public int FollowingCount { get; set; }
        public int FollowersCount { get; set; }
        public int AnswersCount { get; set; }
        public EndUser? EndUser { get; set; }
    }
}
