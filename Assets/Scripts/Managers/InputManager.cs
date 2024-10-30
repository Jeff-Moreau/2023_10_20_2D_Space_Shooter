/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: InputManager.cs
 * Date Created: October 23, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 23, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

//ENUMERATORS
#region Public Enumerator Declarations Only

// public enum eEnumName  // Example
// {
// 		Hey,
//		You
// }

#endregion

namespace TrenchWars.Manager
{
	public class InputManager : MonoBehaviour
	{
		//SINGLETON
		#region Singleton
		
		private static InputManager mInstance;
		
		private void InitializeSingleton()
		{
			if (mInstance != null && mInstance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				mInstance = this;
				DontDestroyOnLoad(gameObject);
			}
		}
	
		public static InputManager Access => mInstance;

        #endregion

        //VARIABLES
        #region Private Variable Declarations Only

        private enum eKeyboardMouseKeys
        {
            None,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,
            Escape,
            F1,
            F2,
            F3,
            F4,
            F5,
            F6,
            F7,
            F8,
            F9,
            F10,
            F11,
            F12,
            Tilde,
            Alpha1,
            Alpha2,
            Alpha3,
            Alpha4,
            Alpha5,
            Alpha6,
            Alpha7,
            Alpha8,
            Alpha9,
            Alpha0,
            Minus,
            Plus,
            Backspace,
            Tab,
            LeftBracket,
            RightBracket,
            Backslash,
            CapsLock,
            Semicolon,
            Quote,
            Return,
            LeftShift,
            Comma,
            Period,
            Slash,
            RightShift,
            LeftControl,
            LeftAlt,
            Space,
            RightAlt,
            RightControl,
            Insert,
            Home,
            PageUp,
            Delete,
            End,
            PageDown,
            UpArrow,
            DownArrow,
            LeftArrow,
            RightArrow,
            KeypadDivide,
            KeypadMultiply,
            KeypadMinus,
            Keypad7,
            Keypad8,
            Keypad9,
            KeypadPlus,
            Keypad4,
            Keypad5,
            Keypad6,
            Keypad1,
            Keypad2,
            Keypad3,
            Keypad0,
            KeypadPeriod,
            KeypadEnter,
            MouseLeft,
            MouseRight,
            MouseMiddle,
            MouseFour,
            MouseFive,
            MouseSix,
            MouseSeven
        }
        private enum eControllerButtons
        {
            None,
            A,
            B,
            X,
            Y,
            LeftBumper,
            RightBumper,
            Select,
            Start,
            LeftThumbPush,
            RightThumbPush,
            Home
        }

        private KeyCode[] mAvailableControllerKeys;
        private KeyCode[] mAvailableKeyboardMouseKeys;

        //UI Inputs
        private KeyCode mPauseMenuKey;
        private KeyCode mPauseMenuControllerKey;

        private KeyCode mInteractKey;
        private KeyCode mFireKey;
        private KeyCode mSpecialKey;

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        private void Awake() => InitializeSingleton();

        private void Start() => InitializeVariables();

        private void InitializeVariables()
        {
            mAvailableKeyboardMouseKeys = new KeyCode[]
            {
                //Alpha Keys
                KeyCode.None,
                KeyCode.A,
                KeyCode.B,
                KeyCode.C,
                KeyCode.D,
                KeyCode.E,
                KeyCode.F,
                KeyCode.G,
                KeyCode.H,
                KeyCode.I,
                KeyCode.J,
                KeyCode.K,
                KeyCode.L,
                KeyCode.M,
                KeyCode.N,
                KeyCode.O,
                KeyCode.P,
                KeyCode.Q,
                KeyCode.R,
                KeyCode.S,
                KeyCode.T,
                KeyCode.U,
                KeyCode.V,
                KeyCode.W,
                KeyCode.X,
                KeyCode.Y,
                KeyCode.Z,
                KeyCode.Escape,
                KeyCode.F1,
                KeyCode.F2,
                KeyCode.F3,
                KeyCode.F4,
                KeyCode.F5,
                KeyCode.F6,
                KeyCode.F7,
                KeyCode.F8,
                KeyCode.F9,
                KeyCode.F10,
                KeyCode.F11,
                KeyCode.F12,
                KeyCode.Tilde,
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
                KeyCode.Alpha4,
                KeyCode.Alpha5,
                KeyCode.Alpha6,
                KeyCode.Alpha7,
                KeyCode.Alpha8,
                KeyCode.Alpha9,
                KeyCode.Alpha0,
                KeyCode.Minus,
                KeyCode.Plus,
                KeyCode.Backspace,
                KeyCode.Tab,
                KeyCode.LeftBracket,
                KeyCode.RightBracket,
                KeyCode.Backslash,
                KeyCode.CapsLock,
                KeyCode.Semicolon,
                KeyCode.Quote,
                KeyCode.Return,
                KeyCode.LeftShift,
                KeyCode.Comma,
                KeyCode.Period,
                KeyCode.Slash,
                KeyCode.RightShift,
                KeyCode.LeftControl,
                KeyCode.LeftAlt,
                KeyCode.Space,
                KeyCode.RightAlt,
                KeyCode.RightControl,
                KeyCode.Insert,
                KeyCode.Home,
                KeyCode.PageUp,
                KeyCode.Delete,
                KeyCode.End,
                KeyCode.PageDown,
                KeyCode.UpArrow,
                KeyCode.DownArrow,
                KeyCode.LeftArrow,
                KeyCode.RightArrow,
                KeyCode.KeypadDivide,
                KeyCode.KeypadMultiply,
                KeyCode.KeypadMinus,
                KeyCode.Keypad7,
                KeyCode.Keypad8,
                KeyCode.Keypad9,
                KeyCode.KeypadPlus,
                KeyCode.Keypad4,
                KeyCode.Keypad5,
                KeyCode.Keypad6,
                KeyCode.Keypad1,
                KeyCode.Keypad2,
                KeyCode.Keypad3,
                KeyCode.Keypad0,
                KeyCode.KeypadPeriod,
                KeyCode.KeypadEnter,
                KeyCode.Mouse0, //Left Click
                KeyCode.Mouse1, //Right Click
                KeyCode.Mouse2, //Middle Click
                KeyCode.Mouse3, //Fourth Button
                KeyCode.Mouse4, //Fifth Button
                KeyCode.Mouse5,
                KeyCode.Mouse6
            };

            mAvailableControllerKeys = new KeyCode[]
            {
                KeyCode.None,
                KeyCode.Joystick1Button0, //Xbox A Button, PS X Button
                KeyCode.Joystick1Button1, //Xbox B Button, PS O Button
                KeyCode.Joystick1Button2, //Xbox X Button, PS □ Button
                KeyCode.Joystick1Button3, //Xbox Y Button, PS △ Button
                KeyCode.Joystick1Button4, //Xbox Left Bumper (LB), PS Left Bumper (L1)
                KeyCode.Joystick1Button5, //Xbox Right Bumper (RB), PS Right Bumper (R1)
                KeyCode.Joystick1Button6, //Xbox Select Button, PS Select Button
                KeyCode.Joystick1Button7, //Xbox Menu/Start Button, PS Menu Start Button
                KeyCode.Joystick1Button8, //Xbox Left Thumb Stick Click, PS Left Thumb Stick Click
                KeyCode.Joystick1Button9, //Xbox Right Thumb Stick Click, PS Right Thumb Stick Click
                KeyCode.Joystick1Button10, //Xbox Home/Xbox Button, PS Home/PlayStation Button
            };

            // Replace with save data for the int
            // UI Inputs

            mFireKey = mAvailableKeyboardMouseKeys[(int)eKeyboardMouseKeys.MouseLeft];
            mSpecialKey = mAvailableKeyboardMouseKeys[(int)eKeyboardMouseKeys.MouseRight];
            mPauseMenuKey = mAvailableKeyboardMouseKeys[(int)eKeyboardMouseKeys.Escape];
            mInteractKey = mAvailableKeyboardMouseKeys[(int)eKeyboardMouseKeys.E];
        }

        #endregion
        #region Implementation Methods/Functions

        private void Update()
        {
            if (Input.GetKeyDown(mInteractKey))
            {
                InputActions.InteractKey?.Invoke();
            }

            if (Input.GetKeyDown(mPauseMenuKey) || Input.GetKeyDown(mPauseMenuControllerKey))
            {
                
            }

            if (Input.GetKey(mFireKey))
            {
                InputActions.FireKey?.Invoke();
            }

            if (Input.GetKeyDown(mSpecialKey))
            {
                InputActions.SpecialKey?.Invoke();
            }
        }

        public void Save(string aSaveName) => throw new System.NotImplementedException();
        public void Load(string aSaveName) => throw new System.NotImplementedException();

        #endregion
    }
}