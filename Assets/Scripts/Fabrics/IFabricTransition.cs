using System.Collections.Generic;
using PlayerLogic.States.Transition;

namespace Fabrics
{
    public interface IFabricTransition
    {
        ITransition CreateTransition(TypeTransitions typeTransitions);
        List<ITransition> CreatTransitions(IEnumerable<TypeTransitions> typeTransitions);
    }
}
