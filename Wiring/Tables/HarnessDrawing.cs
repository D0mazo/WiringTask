using System.ComponentModel.DataAnnotations;

namespace Wiring
{
    public class HarnessDrawing
    {
        public int ID { get; set; }
        public string Harness { get; set; }
        public string Harness_version { get; set; }
        public string Drawing { get; set; }
        public string Drawing_version { get; set; }
        public ICollection<HarnessWires> HarnessWires { get; set; } = new HashSet<HarnessWires>();
    }
}