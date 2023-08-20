using Dapper.Contrib.Extensions;
using MyMediaCollection.Enums;

namespace MyMediaCollection.Model
{
    public class Medium
    {
        [Key]
        public int Id { get; set;  }
        public string Name { get; set; }
        public ItemType MediaType { get; set; }
    }
}