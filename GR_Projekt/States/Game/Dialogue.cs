using GR_Projekt.Core;
using GR_Projekt.Utils.Dialogue;
using GR_Projekt.Utils.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        NPC npc;
        Map map;
        public Vector3 cameraPosition = new Vector3(4900.0f, 3800.0f, 4000.0f);
        public Vector3 cameraTarget = new Vector3(3500.0f, 0.0f, 500.0f);
        private GraphicsDevice graphicsDevice;
        private Point respNPC, respHerb;
        private ContentManager content;
        private GraphicsDeviceManager graphicsDeviceManager;

        public Dialogue(GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content, GraphicsDeviceManager graphicsDeviceManager, Map map)
        {
            _graphics = graphicsDevice;
            dialogueItems = new List<DialogueItem>();
            this.basicEffect = basicEffect;
            this.map = map;
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            this.graphicsDeviceManager = graphicsDeviceManager;
            ReadJSONFile();

            Load(graphicsDevice, basicEffect, content, graphicsDeviceManager);
        }

        public void UpdateDialogue(GameTime gameTime, Vector3 camPosition, Vector3 camTarget)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F1)) key = Keys.F1;
            if (Keyboard.GetState().IsKeyDown(Keys.F2)) key = Keys.F2;
            if (Keyboard.GetState().IsKeyDown(Keys.F3)) key = Keys.F3;
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) key = Keys.Space;
            if (Keyboard.GetState().IsKeyDown(Keys.P)) key = Keys.P;
            if (Keyboard.GetState().IsKeyDown(Keys.J)) killedDictator = true;

            if (herb.collisionPlayerWithHerb(camPosition))
                haveHerb = true;

            if (npc.collisionPlayerWithNPC(camPosition))
                collisionWithNPC = true;

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
                                herb = new Herb(graphicsDevice, basicEffect, content, graphicsDeviceManager, collisePoint());
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

            if (drawHerb)
                haveHerb = false;

            if (collisionWithNPC)
                collisionWithNPC = false;

            cameraPosition = camPosition;
            cameraTarget = camTarget;

            herb.UpdateHerb(gameTime);
        }

        private void ReadJSONFile()
        {/*
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
            */
            dialogueItems.Add(new DialogueItem(0, "Witaj w świecie gry. Mam dla Ciebie poważne zadanie,\nktóre pozwoli uratować nasz region przed niebezpieczeństwem.", "NPC", 1));
            dialogueItems.Add(new DialogueItem(1, "Oczywiście! W jaki sposób mogę pomóc?", "Player", 2));
            dialogueItems.Add(new DialogueItem(2, "Możesz nam pomóc:\nF1. Zabiję dyktatora. \nF2.Poszukam zioła!\nF3.Nie mam czasu na zadania!!!", "NPC", 3));
            dialogueItems.Add(new DialogueItem(3, "Zabiję dyktatora. Tylko nie wiem gdzie go mogę znaleźć?", "Player", 4));
            dialogueItems.Add(new DialogueItem(4, "Bardzo często się on przemieszcza wraz z jego posiłkami.\nMusisz przeszukać całą okolicę i wtedy na pewno spotkasz go\nna swojej drodze. Bądź ostrożny\ntowarzyszy mu armia posiłków.", "NPC", 5));
            dialogueItems.Add(new DialogueItem(5, "Dziękuję za cenne informacje. Wyruszam w drogę !", "Player", 6));
            dialogueItems.Add(new DialogueItem(6, "Uważaj na siebie!", "NPC", 7));
            dialogueItems.Add(new DialogueItem(7, "Pokonałem wroga! Udało mi się!", "Player", 8));
            dialogueItems.Add(new DialogueItem(8, "Dziękuję będziemy wdzięczni za twoją pomoc.", "NPC", 18));
            dialogueItems.Add(new DialogueItem(9, "Poszukam zioła! Jak ono wygląda?", "Player", 10));
            dialogueItems.Add(new DialogueItem(10, "Jest koloru zielonego, natomiast jego\nkorzenie mają odcienie koloru czerwonego. \nCzęsto występuje w suchych terenach", "NPC", 11));
            dialogueItems.Add(new DialogueItem(11, "Mam już wystarczające informacje.\nWyruszam w poszukiwaniu leczniczego zioła.", "NPC", 12));
            dialogueItems.Add(new DialogueItem(12, "Trzymaj się!", "NPC", 13));
            dialogueItems.Add(new DialogueItem(13, "Znalazłem to zioło!", "Player", 14));
            dialogueItems.Add(new DialogueItem(14, "Właśnie takiego potrzebuję!\nDzięki Tobie moja córka będzie żyć.", "NPC", 15));
            dialogueItems.Add(new DialogueItem(15, "To jest mój obowiązek pomagać.", "Player", 16));
            dialogueItems.Add(new DialogueItem(16, "Jeszcze raz dziękuję!", "NPC", 18));
            dialogueItems.Add(new DialogueItem(17, "Dobrze, skoro taka jest twoja decyzja. ", "NPC", 19));
            dialogueItems.Add(new DialogueItem(18, "Mogę jeszcze jakoś pomóc?", "Player", 2));
            dialogueItems.Add(new DialogueItem(19, "Żegnaj graczu!", "NPC", 999));
        }

        private void Load(GraphicsDevice graphicsDevice, BasicEffect basicEffect, ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            currentDialogue = 0;
            dialogueDisplayDelay = 2500;
            sumDelay = 0;
            key = Keys.None;

            displayFrameAndText = false;
            haveHerb = false;
            killedDictator = false;
            drawHerb = false;
            collisionWithNPC = true;

            font = content.Load<SpriteFont>(@"Fonts/font");
            frame = content.Load<Texture2D>(@"Images/Dialogue/bgDialog");

            textLocation = new Vector2(0.15f * graphicsDevice.Viewport.Width, 0.625f * graphicsDevice.Viewport.Height);
            dialogueWindow = new Rectangle(0, (graphicsDevice.Viewport.Height / 5) * 3, graphicsDevice.Viewport.Width, (graphicsDevice.Viewport.Height / 5) * 4);

            herb = new Herb(graphicsDevice, basicEffect, content, graphicsDeviceManager, collisePoint());
            npc = new NPC(graphicsDevice, basicEffect, content, graphicsDeviceManager, collisePoint());
        }
        public void DrawDialogue(GameTime gameTime, SpriteBatch spriteBatch)
        {
            npc.DrawNPC(gameTime, spriteBatch, cameraPosition, cameraTarget);

            if (drawHerb)
                herb.DrawHerb(gameTime, spriteBatch, cameraPosition, cameraTarget);

            spriteBatch.Begin();

            if (displayFrameAndText)
            {
                spriteBatch.Draw(frame, dialogueWindow, Color.Black);

                spriteBatch.DrawString(font, dialogueItems[currentDialogue].Character + ": " + dialogueItems[currentDialogue].Content, textLocation, Colors.defaultDrawColor);
            }

            spriteBatch.End();
        }


        private Point collisePoint()
        {
            bool collide;
            Point point;
            while (true)
            {
                Random rnd = new Random();
                int x = rnd.Next(1, map.map.Count);
                int y = rnd.Next(1, map.map[0].Count);
                point = new Point(x, y);
                collide = map.Collide(point);
                if (collide)
                    return point;
            }
        }
    }
}