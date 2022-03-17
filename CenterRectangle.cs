using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
class CenterRectangle{
        public Texture2D texture;

        private int _x;
        public int X { get{return _x;} private set{} }

        private int _y;
        public int Y { get{return _y;} private set{} }

        private int _width;
        public int Width { get{return _width;} private set{} }

        private int _height;
        public int Height { get{return _height;} private set{} }

        public int Area {get{return Width*Height;} private set{}}

        public Color Color;
        public int Stroke;

        public CenterRectangle(int x, int y, int w, int h, Texture2D texture){
            this.texture = texture;

            _x = x;
            _y= y;
            _width = w;
            _height = h;

            Color = Color.CornflowerBlue;
            Stroke = 0;
        }

        public void Draw(SpriteBatch sb){

            if(Stroke > 0){
                sb.Draw(texture, new Vector2(X-Width,Y-Height), null, Color.Black, 0f, Vector2.Zero, new Vector2(2*Width, 2*Height),SpriteEffects.None, 0f);
                sb.Draw(texture, new Vector2(X-Width+Stroke, Y-Height+Stroke), null, Color, 0f, Vector2.Zero, new Vector2(2*Width-Stroke*2, 2*Height-Stroke*2),SpriteEffects.None, 0f);
            }
            else{
                sb.Draw(texture, new Vector2(X-Width,Y-Height), null, Color, 0f, Vector2.Zero, new Vector2(2*Width, 2*Height),SpriteEffects.None, 0f);
            }

        }
        public bool Contains(int targetX, int targetY){

            return ( targetX >= X - Width && targetX <= X + Width &&
                    targetY >= Y - Height && targetY <= Y + Height);
        }

        public bool Contains(CenterRectangle target){

            return ( target.X-target.Width >= X - Width && target.X+target.Width <= X + Width &&
                    target.Y-target.Height >= Y - Height && target.Y+target.Height <= Y + Height);
        }
}