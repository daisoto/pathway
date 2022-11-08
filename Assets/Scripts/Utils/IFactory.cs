public interface IFactory<out T> 
{
    T GetMoverBehaviour();
}

public interface IFactory<out T, in V>
{
    T Get(V arg);
}