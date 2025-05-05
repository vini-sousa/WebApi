using Microsoft.EntityFrameworkCore;
using Midgar.Domain;
using Midgar.Persistence.Context;
using Midgar.Persistence.Interface;

namespace Midgar.Persistence
{
    public class SpeakerPersist : ISpeakerPersist
    {
        private readonly MidgarContext _context;
        public SpeakerPersist(MidgarContext context)
        {
            _context = context;
        }

        async Task<Speaker[]> ISpeakerPersist.GetAllSpeakersByNameAsync(string name, bool includedEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);
            
            if (includedEvents) 
                query = query.Include(s => s.SpeakerEvents).ThenInclude(se => se.Event);

            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        async Task<Speaker[]> ISpeakerPersist.GetAllSpeakersAsync(bool includedEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);
            
            if (includedEvents) 
                query = query.Include(s => s.SpeakerEvents).ThenInclude(se => se.Event);

            query = query.AsNoTracking().OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }

        async Task<Speaker> ISpeakerPersist.GetSpeakerByIdAsync(int speakerId, bool includedEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);
            
            if (includedEvents) 
                query = query.Include(s => s.SpeakerEvents).ThenInclude(se => se.Event);

            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.Id == speakerId);

            return await query.FirstOrDefaultAsync();
        }
    }
}