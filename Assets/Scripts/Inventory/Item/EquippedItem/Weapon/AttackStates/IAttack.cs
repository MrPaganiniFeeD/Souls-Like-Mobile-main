using System;

public interface IAttack
{
    event Action Exit;

    
    void Enter();
}