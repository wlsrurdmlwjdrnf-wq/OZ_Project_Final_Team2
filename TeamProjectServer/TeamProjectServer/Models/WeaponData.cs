using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProjectServer.Models
{
    [Table("Weapon")]
    public class WeaponData : IData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Element {  get; set; }
        public int Grade { get; set; }
        public int Level { get; set; }
        public float EquipATK { get; set; }
        public float PassiveATK { get; set; }
        public float CriticalDMG { get; set; }
        public float CriticalRate { get; set; }
        public float GoldPer {  get; set; }
        public string IconKey { get; set; }
        public string SoundKey { get; set; }
        public string EffectKey { get; set; }
    }
}
