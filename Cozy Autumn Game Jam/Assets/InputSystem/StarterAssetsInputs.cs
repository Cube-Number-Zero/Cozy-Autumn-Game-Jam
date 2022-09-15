using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Added things")]
        public bool sneak;
        public bool inventory;
		public bool interactL;
		public bool interactR;
		public bool useL;
		public bool useR;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnSneak(InputValue value)
		{
			SneakInput(value.isPressed);
		}

		public void OnInventory(InputValue value)
		{
			InventoryInput(value.isPressed);
		}

		public void OnInteractL(InputValue value)
		{
			InteractLInput(value.isPressed);
		}
		public void OnInteractR(InputValue value)
		{
			InteractRInput(value.isPressed);
		}

		public void OnUseL(InputValue value)
		{
			UseLInput(value.isPressed);
		}
		public void OnUseR(InputValue value)
		{
			UseRInput(value.isPressed);
		}
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void SneakInput(bool newSneakState)
        {
            sneak = newSneakState;
        }

        public void InventoryInput(bool newInventoryState)
        {
            inventory = newInventoryState;
        }

        public void InteractLInput(bool newInteractLState)
        {
            interactL = newInteractLState;
        }
        public void InteractRInput(bool newInteractRState)
        {
            interactR = newInteractRState;
        }

        public void UseLInput(bool newUseLState)
        {
            useL = newUseLState;
        }
        public void UseRInput(bool newUseRState)
        {
            useR = newUseRState;
        }


        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
    }
	
}