using DddInPractice.QueryHandlers.Models;
using System.Threading.Tasks;

namespace DddInPractice.QueryHandlers
{
    public interface ISnackTypesQueries
    {
        Task<SnackTypes[]> GetSnackTypes();
    }
}
