using Microsoft.Xna.Framework.Input;

namespace GR_Projekt.Core
{
    public static class KeyboardHandler
    {
        public static bool WasKeyPressedAndReleased(KeyboardState currentState, KeyboardState previousState, Keys keys) {
            return previousState.IsKeyDown(keys) && currentState.IsKeyUp(keys);
        }
}
}
