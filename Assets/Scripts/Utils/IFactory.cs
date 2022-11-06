using System;

public interface IFactory<out T> 
{
    T GetMoverBehaviour();
}

public interface IFactory<out T, in V> where V: Enum
{
    T Get(V arg);
}