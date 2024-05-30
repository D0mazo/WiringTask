using Wiring.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wiring.Services;

namespace Wiring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Register the DbContext using the connection string from the configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString));
            // Configure services.
            ConfigureServices(builder.Services);

            var app = builder.Build();

            

            // Run any required setup or seeding here
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                context.Database.Migrate();

                // Call SeedData method with the harnessWires list
                SeedData(context);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGenerationService, GenerationService>();
        }
        private static void SeedData(DataContext context)
        {
            if (context.HarnessWires.Any())
            {
                return;
            }
            //data for HarnessWires
            var harnessWires = new List<HarnessWires>
                {
                    new HarnessWires { Harness_ID = 38643, Length = 950, Color = "R", Housing_1 = "C604:19", Housing_2 = "P2.BX2:1" },
                    new HarnessWires { Harness_ID = 38643, Length = 450, Color = "R", Housing_1 = "C604:23", Housing_2 = "C521:1" },
                    new HarnessWires { Harness_ID = 39077, Length = 665, Color = "BN", Housing_1 = "E71.B:1", Housing_2 = "C604:21" },
                    new HarnessWires { Harness_ID = 39077, Length = 665, Color = "GR", Housing_1 = "E71.B:4", Housing_2 = "C604:23" },
                    new HarnessWires { Harness_ID = 39087, Length = 465, Color = "W", Housing_1 = "E71.A:1", Housing_2 = "C681" },
                    new HarnessWires { Harness_ID = 39087, Length = 680, Color = "SB", Housing_1 = "E71.P:3", Housing_2 = "G504-2" },
                    new HarnessWires { Harness_ID = 40442, Length = 475, Color = "GN", Housing_1 = "P2.E85:1", Housing_2 = "C680" },
                    new HarnessWires { Harness_ID = 40442, Length = 980, Color = "R", Housing_1 = "P2.BX2:1", Housing_2 = "E30.P:1" },
                    new HarnessWires { Harness_ID = 40953, Length = 365, Color = "W", Housing_1 = "C621:6", Housing_2 = "C681" },
                    new HarnessWires { Harness_ID = 40953, Length = 305, Color = "SB", Housing_1 = "C620:24", Housing_2 = "G508-3" }
                };

            //data for HarnessDrawings
            var harnessDrawings = new List<HarnessDrawing>
            {
                new HarnessDrawing 
                { 
                    Harness = "S2563532M", 
                    Harness_version = "S-6", 
                    Drawing = "EP", 
                    Drawing_version = "S-4",
                    HarnessWires = harnessWires.Where(e => e.Harness_ID == 40953)
                    .Select(e => new HarnessWires
                    {
                        Length = e.Length,
                        Color = e.Color,
                        Housing_1 = e.Housing_1,
                        Housing_2 = e.Housing_2,
                        
                    }).ToArray(),
                },
                new HarnessDrawing 
                { 
                    Harness = "S2563545M", 
                    Harness_version = "S12", 
                    Drawing = "EP", 
                    Drawing_version = "S-4",
                    HarnessWires = harnessWires.Where(e => e.Harness_ID == 40442 )
                    .Select(e => new HarnessWires
                    {
                        Length = e.Length,
                        Color = e.Color,
                        Housing_1 = e.Housing_1,
                        Housing_2 = e.Housing_2,

                    }).ToArray(),
                },
                new HarnessDrawing 
                { 
                    Harness = "S2563549M",
                    Harness_version = "S-9",
                    Drawing = "EP", 
                    Drawing_version = "S-4",
                    HarnessWires = harnessWires.Where(e => e.Harness_ID == 39087)
                    .Select(e => new HarnessWires
                    {
                        Length = e.Length,
                        Color = e.Color,
                        Housing_1 = e.Housing_1,
                        Housing_2 = e.Housing_2,

                    }).ToArray(),
                },

                new HarnessDrawing 
                { 
                    Harness = "S2641137M",
                    Harness_version = "S-9",
                    Drawing = "EP",
                    Drawing_version = "S-4",
                     HarnessWires = harnessWires.Where(e => e.Harness_ID == 39077)
                    .Select(e => new HarnessWires
                    {
                        Length = e.Length,
                        Color = e.Color,
                        Housing_1 = e.Housing_1,
                        Housing_2 = e.Housing_2,

                    }).ToArray(),
                },
                new HarnessDrawing 
                { 
                    Harness = "S2656843M",
                    Harness_version = "5",
                    Drawing = "EP",
                    Drawing_version = "S-4",
                     HarnessWires = harnessWires.Where(e => e.Harness_ID == 38643)
                    .Select(e => new HarnessWires
                    {
                        Length = e.Length,
                        Color = e.Color,
                        Housing_1 = e.Housing_1,
                        Housing_2 = e.Housing_2,

                    }).ToArray(),
                }
            };
           
           
            // Add the data to the context and save changes
            context.HarnessDrawings.AddRange(harnessDrawings);
            //context.HarnessWires.AddRange(harnessWires);
            context.SaveChanges();
        }
    }
}
