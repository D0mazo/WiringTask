using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiring.Models;

namespace Wiring.Services
{
    public class GenerationService : IGenerationService
    {
        private readonly DataContext dataContext;

        public GenerationService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<HarnessModel>> GenerateRandomHarness()
        {
            // Take random
            Random random = new Random();
            int numberOfRecordsToSelect = random.Next(3, 5);

            // Load all harness wires including their related HarnessDrawing
            var harnessWires = await dataContext.HarnessWires
                .OrderBy(x => Guid.NewGuid())
                .Select(x => new HarnessModel
                {
                    Drawing = x.HarnessDrawing.Drawing,
                    Drawing_version = x.HarnessDrawing.Drawing_version,
                    Harness = x.HarnessDrawing.Harness,
                    Harness_version = x.HarnessDrawing.Harness_version,
                    Housing_1 = x.Housing_1,
                    Housing_2 = x.Housing_2
                })
                .Take(numberOfRecordsToSelect)
                .ToListAsync();

            // Find duplicates in Housing1 and Housing2
            var housing1Duplicates = harnessWires.GroupBy(x => x.Housing_1)
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Select(x => x.Housing_1))
                .ToHashSet();

            var housing2Duplicates = harnessWires.GroupBy(x => x.Housing_2)
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Select(x => x.Housing_2))
                .ToHashSet();

            // Find duplicates between Housing1 and Housing2
            var housing1Set = harnessWires.Select(x => x.Housing_1).ToHashSet();
            var housing2Set = harnessWires.Select(x => x.Housing_2).ToHashSet();
            var crossDuplicates = housing1Set.Intersect(housing2Set).ToHashSet();

            // Map to HarnessModel and set the Error flag if there are duplicates in Housing1 or Housing2 or between them
            var harnessModels = harnessWires.Select(x => new HarnessModel
            {
                Drawing = x.Drawing,
                Drawing_version = x.Drawing_version,
                Harness = x.Harness,
                Harness_version = x.Harness_version,
                Housing_1 = x.Housing_1,
                Housing_2 = x.Housing_2,
                Error = housing1Duplicates.Contains(x.Housing_1) ||
                        housing2Duplicates.Contains(x.Housing_2) ||
                        crossDuplicates.Contains(x.Housing_1) ||
                        crossDuplicates.Contains(x.Housing_2)

            }).ToList();

            // Select 3 or 4 random records from the processed list
            var randomHarnessModels = harnessModels
                .ToList();

            return randomHarnessModels;
        }
    }
}
