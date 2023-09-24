using System;

public class MeleeAttack : IAttack, IDisposable
{
    private readonly IAttackData _attackData;
    public event Action Exit;

    public MeleeAttack(IAttackData attackData)
    {
        _attackData = attackData;
    }

    public void Enter()
    {
        
    }

    public void Dispose()
    {
        
    }
}