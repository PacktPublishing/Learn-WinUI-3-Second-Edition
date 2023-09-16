using UnoMediaCollection.Enums;

namespace UnoMediaCollection.Model
{
    public class Medium
    {
        public int Id { get; set;  }
        public string? Name { get; set; }
        public ItemType MediaType { get; set; }
    }
}