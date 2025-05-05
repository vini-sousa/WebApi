using Midgar.Domain;

namespace Midgar.Persistence.Interface
{
    public interface ISpeakerPersist
    {
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includedEvents = false);

        Task<Speaker[]> GetAllSpeakersAsync(bool includedEvents = false);

        Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includedEvents = false);
    }
}