using System.ComponentModel.DataAnnotations;

namespace Wiring
{
    public class HarnessWires
    {
        public int ID { get; set; }
        public int Harness_ID { get; set; }
        public float Length { get; set; }
        public string Color { get; set; }
        public string Housing_1 { get; set; }
        public string Housing_2 { get; set; }
        public HarnessDrawing HarnessDrawing { get; set; }
    }
}