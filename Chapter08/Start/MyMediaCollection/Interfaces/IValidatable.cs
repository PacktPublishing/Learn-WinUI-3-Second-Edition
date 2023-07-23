namespace MyMediaCollection.Interfaces
{
    public interface IValidatable
    {
        void Validate(string memberName, object value);
    }
}