using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }

        public void VirtualSneakInput(bool virtualSneakState)
        {
            starterAssetsInputs.SneakInput(virtualSneakState);
        }

        public void VirtualInventoryInput(bool virtualInventoryState)
        {
            starterAssetsInputs.InventoryInput(virtualInventoryState);
        }

        public void VirtualInteractLInput(bool virtualInteractLState)
        {
            starterAssetsInputs.InteractLInput(virtualInteractLState);
        }

        public void VirtualInteractRInput(bool virtualInteractRState)
        {
            starterAssetsInputs.InteractRInput(virtualInteractRState);
        }

        public void VirtualUseLInput(bool virtualUseLState)
        {
            starterAssetsInputs.UseLInput(virtualUseLState);
        }

        public void VirtualUseRInput(bool virtualUseRState)
        {
            starterAssetsInputs.UseRInput(virtualUseRState);
        }

    }

}
