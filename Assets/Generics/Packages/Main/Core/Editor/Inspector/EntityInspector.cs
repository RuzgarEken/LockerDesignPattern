using Generics.Behaviours;
//using Generics.Packages.Animation;
using UnityEditor;

namespace Generics.Editor.Inspector
{

    [CustomEditor(typeof(Behaviours.Entity), true)]
    public class EntityInspector : ComponentInspectorBase<Entity>
    {
        protected override void DrawOptions()
        {
            base.DrawOptions();

            if (!FoldoutActive)
            {
                return;
            }

            //DrawComponentOption<AnimatorBehaviour>("Animator");
            //DrawComponentOption<ObjectIdleAnimationBehaviour>("Idle Animator");
            //DrawComponentOption<ButtonClickAnimationBehaviour>("Idle Animator");

        }

    }

}