using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MazeGeneration
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Dot[,] grid;

        int screenWidth = 800;
        int screenHeight = 800;

        int dimension = 10;

        BasicEffect lineTest;
        VertexPositionColor[] vertices;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            Content.RootDirectory = "Content";
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

            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Line stuff
            lineTest = new BasicEffect(graphics.GraphicsDevice);
            lineTest.VertexColorEnabled = true;
            lineTest.Projection = Matrix.CreateOrthographicOffCenter(0, graphics.GraphicsDevice.Viewport.Width,
                                                                    graphics.GraphicsDevice.Viewport.Height, 0, 0, 1);
            

            vertices = new VertexPositionColor[dimension * dimension];
            vertices[0].Position = new Vector3(100, 100, 0);
            vertices[0].Color = Color.Black;
            vertices[1].Position = new Vector3(200, 200, 0);
            vertices[1].Color = Color.Black;    

            // TODO: use this.Content to load your game content here
            grid = new Dot[dimension, dimension];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    float currentX = i * (screenWidth + 76) / dimension;
                    float currentY = j * (screenHeight + 64) / dimension;

                    grid[i, j] = new Dot(Content.Load<Texture2D>("dot"), new Vector2(currentX, currentY));
                    vertices[dimension * i + j].Position = new Vector3(currentX, currentY, 0);
                    vertices[dimension * i + j].Color = Color.Black;
                    Console.WriteLine(dimension * i + j);
                }
            }

            //testDot = new Dot(Content.Load<Texture2D>("dot"), new Vector2(screenWidth / 2, screenHeight / 2));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();


            lineTest.CurrentTechnique.Passes[0].Apply();
            graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, dimension);


            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j].Draw(spriteBatch);
                }
            }
            

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
