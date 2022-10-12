using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public abstract class ProjectileBehaviour : ComponentBase
    {

        public abstract void Set(Vector3 direction);

    }

}