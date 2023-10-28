using System.Collections.Generic;
using Hero.States.Transition;

namespace Fabrics
{
    public interface IFabricTransition
    {
        ITransition CreateTransition(TypeTransitions typeTransitions);
        List<ITransition> CreatTransitions(IEnumerable<TypeTransitions> typeTransitions);
    }
}
