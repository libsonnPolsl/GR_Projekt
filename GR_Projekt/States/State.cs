using GR_Projekt.States.Settings.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#nullable enable

namespace GR_Projekt.States
{
    public abstract class State
    {
        protected ContentManager _contentManager;
        protected Game1 _game;
        protected GraphicsDevice _graphicsDevice;
        protected StateTypeEnumeration _stateTypeEnumeration;
        protected KeyboardState _currentKeyboardState;
        protected KeyboardState _previousKeyboardState;
        protected SettingsModel _settingsModel;
        
        public State(ContentManager content, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel, StateTypeEnumeration stateTypeEnumeration)
        {
            this._contentManager = content;
            this._game = game;
            this._graphicsDevice = graphicsDevice;
            this._stateTypeEnumeration = stateTypeEnumeration;
            this._settingsModel = settingsModel;
        }

        public abstract void repositionComponents();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Dispose();
        public abstract void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState);

        public StateTypeEnumeration GetStateType => _stateTypeEnumeration;

    }
}
