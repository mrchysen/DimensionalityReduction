namespace ClippingAlgorithms.Utils.Equalizers;

public interface IEqualizer<T>
{
    bool IsEquals(T obj1, T obj2);
}
