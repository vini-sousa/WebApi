using Midgar.Application.Interface;
using Midgar.Domain;
using Midgar.Persistence.Interface;

namespace Midgar.Application
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventPersist _eventPersist;
        public EventService(IGeneralPersist generalPersist, IEventPersist eventPersist)
        {
            _generalPersist = generalPersist;
            _eventPersist = eventPersist;
        }

        public async Task<Event> AddEvents(Event model)
        {
            try
            {
                _generalPersist.Add<Event>(model);

                return await _generalPersist.SaveChangesAsync() ? await _eventPersist.GetEventByIdAsync(model.Id, false) : null;
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> UpdateEvents(int id,Event model)
        {
            try
            {
                var updateEvent = await _eventPersist.GetEventByIdAsync(id, false);

                if (updateEvent == null)
                    return null;

                model.Id = updateEvent.Id;

                _generalPersist.Update(model);

                return await _generalPersist.SaveChangesAsync() ? await _eventPersist.GetEventByIdAsync(model.Id, false) : null;
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvents(int eventId)
        {
            try
            {
                var deleteEvent = await _eventPersist.GetEventByIdAsync(eventId, false) ?? throw new Exception("Delete event not found.");
                
                _generalPersist.Delete<Event>(deleteEvent);

                return await _generalPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {     
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event[]> GetAllEventsAsync(bool includedSpeakers = false)
        {
            try
            {
               var getAllEvent = await _eventPersist.GetAllEventsAsync(includedSpeakers);

               return getAllEvent == null ? null : getAllEvent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includedSpeakers = false)
        {
            try
            {
               var getAllEventByTheme = await _eventPersist.GetAllEventsByThemeAsync(theme, includedSpeakers);

               return getAllEventByTheme == null ? null : getAllEventByTheme;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includedSpeakers = false)
        {
            try
            {
               var getAllEventById = await _eventPersist.GetEventByIdAsync(eventId, includedSpeakers);

               return getAllEventById == null ? null : getAllEventById;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }
    }
}