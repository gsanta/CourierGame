
public interface ItemFactory<T, U>
{
    public U Create(T config);
}
