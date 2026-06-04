using Foster.Framework;
using System.Numerics;

namespace FosterFlow.Graphics;

public class Sprite(Subtexture spriteTexture, Vector2 spritePosition, float spriteScale)
{
    public Subtexture Texture = spriteTexture;
    public Vector2 Position = spritePosition;
    public float Scale = spriteScale;
    public float Rotation = 0.0f;

    public void Draw(Batcher batcher)
    {
        batcher.Image(Texture, Position, new Vector2(0, 0), new Vector2(Scale), Rotation, Color.White);
    }
}