namespace Moon.DAL
{
    public interface ICultureQuery<T> : ISimpleQuery<T>
    {
        System.Globalization.CultureInfo Culture { get; set; }
    }
}
