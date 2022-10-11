using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.Walking
{

    public abstract class WalkingMoveBehaviour : ComponentExtension<WalkingBehaviour>
    {
        public WalkingBehaviour Walking => BaseComponent;

        public abstract void Move(float fixedDeltaTime);

        public virtual bool IsDestinationValid(Vector3 destination) => true;

    }

}