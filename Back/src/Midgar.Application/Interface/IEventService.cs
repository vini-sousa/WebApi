using Midgar.Domain;

namespace Midgar.Application.Interface
{
    public interface IEventService
    {
        Task<Event> AddEvents(Event model);

        Task<Event> UpdateEvents(int id, Event model);

        Task<bool> DeleteEvents(int eventId);

        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includedSpeakers = false);

        Task<Event[]> GetAllEventsAsync(bool includedSpeakers = false);

        Task<Event> GetEventByIdAsync(int eventId, bool includedSpeakers = false);
    }
}