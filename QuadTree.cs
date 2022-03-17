using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace QuadTreeTest
{
    class QuadTree{

        private CenterRectangle bounds;

        private static int maxPoints = 1;

        private List<CenterRectangle> points;

        private bool isDivided = false;

        public CenterRectangle Bounds{get {return bounds;} private set{}}

        public int depth;


        //QuadTree topLeft,topRight,bottomLeft,bottomRight;
        private QuadTree[] quadrants;
        public QuadTree(int x, int y, int w, int h, Texture2D texture){
            bounds = new CenterRectangle(x,y,w,h,texture){Stroke = 1,Color = Color.LightGray};
            points = new List<CenterRectangle>(maxPoints);
            quadrants = new QuadTree[4];

            this.depth =0;
        }

        public QuadTree(int x, int y, int w, int h, Texture2D texture, int depth){
            bounds = new CenterRectangle(x,y,w,h,texture){Stroke = 1,Color = Color.LightGray};
            points = new List<CenterRectangle>(maxPoints);
            quadrants = new QuadTree[4];

            this.depth = depth;
        }

        public void Draw(SpriteBatch sb){

            if(!isDivided){
                bounds.Draw(sb);
                //drawCounter++;
            }
            else{
                foreach(QuadTree quad in quadrants){
                    if(quad != null/* && quad.isDivided*/){
                        quad.Draw(sb);
                    }
                }
            }

            foreach(CenterRectangle point in points){
                point.Draw(sb);
            }
        }

        public bool Insert(CenterRectangle newPoint){

            //doesnt fit in bounds
            if(!bounds.Contains(newPoint)){
                return false;
            }

            if(!IsFull()){
                points.Add(newPoint);
                //point = newPoint;//new CenterRectangle(newPoint.X,newPoint.Y,newPoint.Width,newPoint.Height,newPoint.texture);
                return true;
            }
            else{
                if(!isDivided){
                    Subdivide();// init 4 quadrants
                }
                // They are never null because they always get set in subdivide
                //Only one of these will succesfully insert because of the contains check at the start

                foreach(QuadTree quad in quadrants){

                    if(quad.bounds.Width > 25 && quad.Insert(newPoint)){
                        return true;
                    }
                }
                return false;
            }
        }

        private bool IsFull(){
            return points.Count >= maxPoints;
        }

        int padding = 10;
        private void Subdivide(){
            int shift = padding*depth;

            int newWidth = bounds.Width/2 -shift*2;
            int newHeight = bounds.Width/2-shift*2;
            int newDepth = depth+1;
            quadrants[0] = new QuadTree(bounds.X - bounds.Width/2,bounds.Y - bounds.Height/2 , newWidth,newHeight,bounds.texture,newDepth);
            quadrants[1] = new QuadTree(bounds.X + bounds.Width/2,bounds.Y - bounds.Height/2, newWidth,newHeight,bounds.texture,newDepth);
            quadrants[2] = new QuadTree(bounds.X + bounds.Width/2,bounds.Y + bounds.Height/2, newWidth,newHeight,bounds.texture,newDepth);
            quadrants[3] = new QuadTree(bounds.X - bounds.Width/2,bounds.Y + bounds.Height/2, newWidth,newHeight,bounds.texture,newDepth);
            isDivided = true;
        }

    }
}