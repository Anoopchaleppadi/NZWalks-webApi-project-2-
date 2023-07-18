namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LegthInKm { get; set; }
        public string? WalkImageUrl { get; set; }


        //one to one relationship btwn walk nd diffi walk nd regi
        public Guid DifficultyId { get; set; }//for relation ship

        public Guid RegionId { get; set; }

        // Navigation propertie
        public Difficulty Difficulty { get; set; }// walk will have difficulty linking walk to difficulty
        public Region Region { get; set; }

    }
}
