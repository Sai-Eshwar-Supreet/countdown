using UnityEngine;

namespace CountDown.Input
{
    public static class CursorUtility
    {
        public static void Lock()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
