using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace QuadTreeTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private QuadTree quadTree;
        int pointCounter =0;
        private Texture2D whiteRectangle;
        private Color backgroundColor = Color.LightGray;

        private List<CenterRectangle> points;

        private int screenWidth = 1000, screenHeight = 1000;

        private SpriteFont spriteFont;

        private String debugString = "Test!";
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });

            quadTree = new QuadTree(500,500,400,400,whiteRectangle);
            quadTree.Bounds.Color = backgroundColor;
            quadTree.Bounds.Stroke = 1;

            points = new List<CenterRectangle>(1000);
            //RandomizePoints();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("Font");
            // TODO: use this.Content to load your game content here
        }

        MouseState prevMouseState;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.Space)){
                RandomizePoints();
            }

            debugString = $"X:{Mouse.GetState().X} Y:{Mouse.GetState().Y}";

            if(Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                //points.Add(new CenterRectangle(Mouse.GetState().X,Mouse.GetState().Y,2,2,whiteRectangle){Color = Color.BlanchedAlmond});
                Random r  = new Random();

                quadTree.Insert(new CenterRectangle(Mouse.GetState().X,Mouse.GetState().Y,2,2,whiteRectangle){Color = Color.Lime});
                pointCounter= quadTree.depth;
            }


            prevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(spriteFont,debugString,new Vector2(10,10),Color.Black);
            _spriteBatch.DrawString(spriteFont,"Depth: " + pointCounter,new Vector2(10,30),Color.Black);
            quadTree.Draw(_spriteBatch);
            //DrawPoints(_spriteBatch);

			_spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void RandomizePoints(){
            //points.Clear();
            int padding = 2;
            int pointSize = 2;
            int numPoints = 10;
            Random r;
            for(int i =0; i < numPoints; i++){
                r = new Random();
                int x = quadTree.Bounds.X - quadTree.Bounds.Width + quadTree.Bounds.Stroke+padding+pointSize + r.Next(quadTree.Bounds.Width*2 - quadTree.Bounds.Stroke*2-padding*2-pointSize*2);
                int y = quadTree.Bounds.Y - quadTree.Bounds.Height + quadTree.Bounds.Stroke+padding+pointSize + r.Next(quadTree.Bounds.Height*2 - quadTree.Bounds.Stroke*2-padding*2-pointSize*2);
                quadTree.Insert(new CenterRectangle(x,y,pointSize,pointSize,whiteRectangle)
                {
                    Stroke=0,
                    //Color=new Color(r.Next(256),r.Next(256),r.Next(256)),
                    Color = Color.Black
                });

            }
        }

        private void DrawPoints(SpriteBatch sb){
            foreach(CenterRectangle point in points){
                point.Draw(sb);
            }
        }
    }
}
