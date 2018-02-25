using Microsoft.AspNetCore.Builder;

namespace DataInitializer
{
    public interface IDbInitializer
    {
        void Initialize(IApplicationBuilder app);
    }
}
