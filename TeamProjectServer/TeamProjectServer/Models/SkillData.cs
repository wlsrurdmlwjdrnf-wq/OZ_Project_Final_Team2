using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProjectServer.Models
{
    public class SkillData : IData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Elemnet {  get; set; }
        public int Grade { get; set; }
        public int Level { get; set; }
        public string Icon { get; set; }
        public string Sound { get; set; }
        public string Effect { get; set; }
    }
}
