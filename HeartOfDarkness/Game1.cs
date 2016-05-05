using HeartOfDarkness.Dialogue;
using HeartOfDarkness.Scripting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HeartOfDarkness
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D test;
        SpriteFont uiFont;
        KeyboardState prevState;

        LuaContext scriptContext;

        NPC player;

        DialogueScene scene;

        Rectangle screenBounds = new Rectangle(0, 0, 800, 480);

        Rectangle[] choices = new Rectangle[0];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new NPC();
            player.Name = "Player";

            scriptContext = new LuaContext();
            scriptContext.RegisterVariable("player", player);
            
            XmlDocument doc = new XmlDocument();
            doc.Load("test.xml");
            scene = DialogueScene.ReadFromXml(doc["scene"], scriptContext);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Global.Textures = new TextureCache(Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            uiFont = Content.Load<SpriteFont>("uiFont");

            // NOTE: scene can be built before, but any access to it NEEDS to be after Global.Textures is defined
            scene.GotoFirst();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
                scene.MoveNext();

            MouseState state = Mouse.GetState();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                for(int index = 0; index < choices.Length; index ++)
                {
                    if (choices[index].Contains(state.X, state.Y))
                    {
                        scene.SelectChoice(scene.CurrentChoices[index]);
                    }
                }
            }

            prevState = keyState;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            scene.Render(spriteBatch, uiFont);
           
            int numChoices = scene.CurrentChoices.Length;
            choices = new Rectangle[numChoices];

            for (int index = 0; index < numChoices; index ++)
            {
                DialogueChoice choice = scene.CurrentChoices[index];
                string text = choice.GetMessage(scriptContext);
                spriteBatch.DrawString(uiFont, text, new Vector2(index * 100, 380), Color.Black);
                choices[index] = new Rectangle(index * 100, 380, (int)uiFont.MeasureString(text).X, (int)uiFont.MeasureString(text).Y);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
