using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProjectServer.Models
{
    public class AccessoryData : IData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Element {  get; set; }
        public int Grade { get; set; }
        public int Level { get; set; }
        public float Hp { get; set; }
        public float HPPer {  get; set; }
        public float MPPer { get; set; }
        public float GoldPer { get; set; }
        public string IconKey { get; set; }
    }
}
