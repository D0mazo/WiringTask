using Wiring.Models;

namespace Wiring.Services
{
    public interface IGenerationService
    {
        Task<IEnumerable<HarnessModel>> GenerateRandomHarness();
    }
}
