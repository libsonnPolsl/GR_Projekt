using GR_Projekt.Core;
using GR_Projekt.Utils.Dialogue;
using GR_Projekt.Utils.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GR_Projekt.States.Game
{
    class Dialogue
    {
        private GraphicsDevice _graphics;
        private BasicEffect basicEffect;
        private Rectangle dialogueWindow;
        private Vector2 textLocation;
        private SpriteFont font;
        private Texture2D frame;
        private List<DialogueItem> dialogueItems;
        private float dialogueDisplayDelay, sumDelay;
        private int currentDialogue;
        private bool displayFrameAndText;
        private Keys key;
        bool haveHerb, killedDictator, collisionWithNPC, drawHerb;
        Herb herb;
        public List<List<Block>> map;
        public Vector3 cameraPosition = new Vector3(4900.0f, 3800.0f, 4000.0f);
        public Vector3 cameraTarget = new Vector3(3500.0f, 0.0f, 500.0f);

        public Dialogue(GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            _graphics = graphicsDevice;

            dialogueItems = new List<DialogueItem>();

            //odczytanie z pliku do listy
            using (StreamReader r = new StreamReader(FilesPaths.getDialogueOptions))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    dialogueItems.Add(new DialogueItem((int)item.id, item.content.ToString(),
                        item.character.ToString(), (int)item.nextId));
                }
            }

            currentDialogue = 0;
            dialogueDisplayDelay = 25;
            sumDelay = 0;
            key = Keys.None;

            displayFrameAndText = false;
            haveHerb = false;
            killedDictator = false;
            drawHerb = false;
            //do zmiany jak ogarne kolizje
            collisionWithNPC = true;
            this.basicEffect = basicEffect;

            font = content.Load<SpriteFont>(@"Fonts/font");
            frame = content.Load<Texture2D>(@"Images/Dialogue/bgDialog");

            textLocation = new Vector2(0.15f * graphicsDevice.Viewport.Width, 0.75f * graphicsDevice.Viewport.Height);
            dialogueWindow = new Rectangle(0, (graphicsDevice.Viewport.Height / 3) * 2, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            herb = new Herb(graphicsDevice, basicEffect, content, graphicsDeviceManager);
        }

        public void UpdateDialogue(GameTime gameTime, Vector3 camPosition, Vector3 camTarget)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F1)) key = Keys.F1;
            if (Keyboard.GetState().IsKeyDown(Keys.F2)) key = Keys.F2;
            if (Keyboard.GetState().IsKeyDown(Keys.F3)) key = Keys.F3;
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) key = Keys.Space;
            if (Keyboard.GetState().IsKeyDown(Keys.P)) key = Keys.P;
            if (Keyboard.GetState().IsKeyDown(Keys.J)) killedDictator = true;
            if (Keyboard.GetState().IsKeyDown(Keys.H)) haveHerb = true;

            sumDelay += gameTime.ElapsedGameTime.Milliseconds;

            if (key.Equals(Keys.Space) && ((currentDialogue == 7 && killedDictator) || (currentDialogue == 13 && haveHerb)))
            {
                displayFrameAndText = true;
            }

            if (currentDialogue == 13 && haveHerb || key.Equals(Keys.P))
                drawHerb = false;

            if (dialogueDisplayDelay <= sumDelay)
            {
                if (key.Equals(Keys.Space) && collisionWithNPC)
                {
                    displayFrameAndText = true;
                }

                if (dialogueItems.Count - 1 > currentDialogue)
                {
                    if (dialogueItems[currentDialogue].Id == 2)
                    {
                        if (key != Keys.None && displayFrameAndText)
                        {
                            if (key.Equals(Keys.F1))
                            {
                                currentDialogue = 3;
                            }
                            else if (key.Equals(Keys.F2))
                            {
                                currentDialogue = 9;
                                drawHerb = true;
                            }
                            else if (key.Equals(Keys.F3))
                            {
                                currentDialogue = 17;
                            }
                        }
                    }
                    else
                    {
                        if (dialogueItems[currentDialogue].NextId <= 19 && displayFrameAndText)
                        {
                            if (sumDelay >= 2000 && currentDialogue == 0 && key.Equals(Keys.Space))
                            {
                                sumDelay = 0;
                                currentDialogue = 0;
                            }
                            else
                            {
                                if (currentDialogue == 7 && !killedDictator)
                                    currentDialogue = 3;
                                if (currentDialogue == 13 && !haveHerb)
                                    currentDialogue = 9;
                                currentDialogue = dialogueItems[currentDialogue].NextId;
                            }
                        }

                    }

                    if (currentDialogue == 19 || key.Equals(Keys.P) || currentDialogue == 13 || currentDialogue == 7)
                    {
                        displayFrameAndText = false;
                    }
                    if (currentDialogue == 19 || key.Equals(Keys.P))
                        currentDialogue = 0;
                }

                sumDelay = 0;
                key = Keys.None;
            }

            this.cameraPosition = camPosition;
            this.cameraTarget = camTarget;
        }

        public void updateCamera(Vector3 camPosition, Vector3 camTarget)
        {
            
        }

        public void DrawDialogue(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            if (displayFrameAndText)
            {
                spriteBatch.Draw(frame, dialogueWindow, Colors.frameColorBlack);

                spriteBatch.DrawString(font, dialogueItems[currentDialogue].Character + ": " + dialogueItems[currentDialogue].Content, textLocation, Colors.defaultDrawColor);
            }

            spriteBatch.End();
            Block block;
                    block = map[100][50];
                    if (block.blockType == BlockType.Floor || block.blockType == BlockType.Corruption || block.blockType == BlockType.Stronghold || block.blockType == BlockType.FloorInner3)
                    {
                    herb.DrawHerb(gameTime, spriteBatch, cameraPosition, cameraTarget);
             }

            //if (drawHerb)
               // herb.DrawHerb(gameTime, spriteBatch, cameraPosition, cameraTarget);

        }
    }
}
