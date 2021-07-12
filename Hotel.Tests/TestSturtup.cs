using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Tests
{
    class TestSturtup
    {
        public TestSturtup(IConfiguration _) { }

        public void ConfigureServices(IServiceCollection _) { }

        public void Configure(IApplicationBuilder _) { }
    }
}
