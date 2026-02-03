using System.ComponentModel.DataAnnotations;

namespace TeamProjectServer.Models
{
    public interface IData
    {
        [Key]
        int ID { get; set; }
        string Name { get; set; }
    }
}
