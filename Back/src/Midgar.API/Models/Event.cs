namespace Midgar.API.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string Local { get; set; }

        public string EventDate { get; set; }

        public string Theme { get; set; }

        public int PeopleCount { get; set; }

        public string Lote { get; set; }

        public string ImageURL { get; set; }
    }
}