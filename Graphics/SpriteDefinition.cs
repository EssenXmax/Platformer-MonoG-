namespace Platformer_MonoG.Graphics
{
    public struct SpriteDefinition
    {

        public int X {  get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }



        public SpriteDefinition(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

    }
}
