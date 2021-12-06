using GR_Projekt.States.Settings.Models;
using GR_Projekt.Utils.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GR_Projekt.States.Game
{

    public class Map : State
    {
        private readonly int WALL_HEIGHT = 200;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D texture;
        public Texture2D corruptionWall;
        public Texture2D strongholdWall;
        public Texture2D floor;
        public Texture2D floorInner1;
        public Texture2D floorInner2;
        public Texture2D floorInner3;
        public Texture2D floorRedCarpet;
        public Texture2D corruption;
        public Texture2D stronghold;
        public Texture2D fallback;

        private TransformationEffects transformEffects;
        private RandomMap randomMap;

        public SpriteFont font;

        public Vector3 cameraPosition = new Vector3(4900.0f, 3800.0f, 4000.0f);
        public Vector3 cameraTarget = new Vector3(3500.0f, 0.0f, 500.0f);

        public List<List<Block>> map;

        public Map(ContentManager content, GraphicsDevice graphicsDevice, Game1 game, SettingsModel settingsModel) : base(content, graphicsDevice, game, settingsModel, StateTypeEnumeration.Game)
        {
            _spriteBatch = _game.GameSpriteBatch;
            _graphics = _game.GameGraphicsDeviceManager;

            LoadContent();
            
        }

        public List<int>[] GetBaseMap()
        {
            return this.randomMap.baseMap;
        }

        public bool Collide(Point point)
        {
            int x = (int)Math.Ceiling(point.X / 100.0f) - 1;
            int y = -(int)Math.Ceiling(point.Y / 100.0f) - 1;

            try
            {
                return this.randomMap.getBlockType(this.GetBaseMap()[x][y]) == BlockType.Wall;
            }
            catch (System.IndexOutOfRangeException)
            {
                Trace.WriteLine("Collide IndexOutOfRangeException");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Trace.WriteLine("Collide ArgumentOutOfRangeException");
            }

            return true;
        }

        private void LoadContent()
        {
            texture = _contentManager.Load<Texture2D>(@"Images\Map\Walls\GRAYBIG");
            corruptionWall = _contentManager.Load<Texture2D>(@"Images\Map\Walls\SLADRIP3");
            strongholdWall = _contentManager.Load<Texture2D>(@"Images\Map\Walls\STONE3");
            floor = _contentManager.Load<Texture2D>(@"Images\Map\Floors\FLAT3");
            floorInner1 = _contentManager.Load<Texture2D>(@"Images\Map\Floors\FLAT18");
            floorInner2 = _contentManager.Load<Texture2D>(@"Images\Map\Floors\FLOOR0_5");
            floorInner3 = _contentManager.Load<Texture2D>(@"Images\Map\Floors\FLAT23");
            floorRedCarpet = _contentManager.Load<Texture2D>(@"Images\Map\Floors\FLOOR5_2");
            corruption = _contentManager.Load<Texture2D>(@"Images\Map\Floors\NUKAGE1");
            stronghold = _contentManager.Load<Texture2D>(@"Images\Map\Floors\CEIL3_5");
            fallback = _contentManager.Load<Texture2D>(@"Images\Map\Floors\FLAT22");

            this.transformEffects = new TransformationEffects(_graphics, _graphicsDevice);
            this.randomMap = new RandomMap();

            this.map = this.randomMap.getBlockMap();
        }

        public override void Dispose()
        {
            texture.Dispose();
            corruptionWall.Dispose();
            strongholdWall.Dispose();
            floor.Dispose();
            floorInner1.Dispose();
            floorInner2.Dispose();
            floorInner3.Dispose();
            floorRedCarpet.Dispose();
            corruption.Dispose();
            stronghold.Dispose();
            fallback.Dispose();
        }

        public void updateCamera(Vector3 camPosition, Vector3 camTarget)
        {
            this.cameraPosition = camPosition;
            this.cameraTarget = camTarget;
        }

        public override void Update(GameTime gameTime, KeyboardState previousState, KeyboardState currentState)
        {

        }

        public override void repositionComponents()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.End();
            }
            catch (InvalidOperationException e) {

            }

            Block block;
            for (int x = 0; x < map.Count; x++)
            {
                for (int y = 0; y < map[0].Count; y++)
                {
                    block = map[x][y];
                    this.drawFloor(block.blockType, x * 100, y * 100);
                }
            }

            for (int x = 0; x < map.Count; x++)
            {
                for (int y = 0; y < map[0].Count; y++)
                {
                    block = map[x][y];
                    if (block.blockType == BlockType.Floor || block.blockType == BlockType.Corruption || block.blockType == BlockType.Stronghold || block.blockType == BlockType.FloorInner3)
                    {
                        if (block.walls[0]) this.drawTopNWall(block.blockType, x * 100, y * 100);
                        if (block.walls[1]) this.drawBottomNWall(block.blockType, x * 100, y * 100);
                        if (block.walls[2]) this.drawRightNWall(block.blockType, x * 100, y * 100);
                        if (block.walls[3]) this.drawLeftNWall(block.blockType, x * 100, y * 100);
                    }
                }
            }
        }

        private Texture2D GetFloorTexture(BlockType type)
        {
            Texture2D txt;
            if (type == BlockType.Wall)
            {
                return null;
            }
            else if (type == BlockType.Floor)
            {
                txt = floor;
            }
            else if (type == BlockType.FloorInner1)
            {
                txt = floorInner1;
            }
            else if (type == BlockType.FloorInner2)
            {
                txt = floorInner2;
            }
            else if (type == BlockType.FloorInner3)
            {
                txt = floorInner3;
            }
            else if (type == BlockType.RedCarpet)
            {
                txt = floorRedCarpet;
            }
            else if (type == BlockType.Corruption)
            {
                txt = corruption;
            }
            else if (type == BlockType.Stronghold)
            {
                txt = stronghold;
            }
            else
            {
                txt = fallback;
            }

            return txt;
        }

        private void drawFloor(BlockType type, int x, int y)
        {
            Texture2D texture = GetFloorTexture(type);
            if (texture is null)
            {
                return;
            }
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect floorEffect = this.transformEffects.getFloorEffect(view);

            _spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, floorEffect);

            _spriteBatch.Draw(texture, new Rectangle(x, y, 100, 100), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
            _spriteBatch.End();
        }

        private Texture2D GetWallTexture(BlockType type)
        {
            Texture2D txt = texture;
            if (type == BlockType.Corruption)
            {
                txt = corruptionWall;
            }
            if (type == BlockType.Stronghold)
            {
                txt = strongholdWall;
            }

            return txt;
        }

        private void drawLeftNWall(BlockType type, int x, int y)
        {
            Texture2D txt = GetWallTexture(type);
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect leftWallEfect = this.transformEffects.getLeftWallEffect(view, x, y);

            _spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, leftWallEfect);

            _spriteBatch.Draw(txt, new Rectangle(x, 0, 100, WALL_HEIGHT), new Rectangle(0, 0, txt.Width, txt.Height), Color.White);
            _spriteBatch.End();
        }

        private void drawRightNWall(BlockType type, int x, int y)
        {
            Texture2D txt = GetWallTexture(type);
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect rightWallEfect = this.transformEffects.getRightWallEffect(view, x, y);

            _spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, rightWallEfect);

            _spriteBatch.Draw(txt, new Rectangle(x, 0, 100, WALL_HEIGHT), new Rectangle(0, 0, txt.Width, txt.Height), Color.White);
            _spriteBatch.End();

        }

        private void drawBottomNWall(BlockType type, int x, int y)
        {
            Texture2D txt = GetWallTexture(type);
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect bottomWallEfect = this.transformEffects.getBottomWallEffect(view, x, y);

            _spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, bottomWallEfect);

            _spriteBatch.Draw(txt, new Rectangle(0, 0, 100, WALL_HEIGHT), new Rectangle(0, 0, txt.Width, txt.Height), Color.White);
            _spriteBatch.End();
        }

        private void drawTopNWall(BlockType type, int x, int y)
        {
            Texture2D txt = GetWallTexture(type);
            Matrix view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            BasicEffect topWallEfect = this.transformEffects.getTopWallEffect(view, x, y);

            _spriteBatch.Begin(0, null, null, DepthStencilState.DepthRead, RasterizerState.CullNone, topWallEfect);

            _spriteBatch.Draw(txt, new Rectangle(0, 0, 100, WALL_HEIGHT), new Rectangle(0, 0, txt.Width, txt.Height), Color.White);
            _spriteBatch.End();
        }
    }
}
