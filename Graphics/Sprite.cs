using Foster.Framework;
using System.Numerics;

namespace FosterFlow.Graphics;

public class Sprite(Subtexture spriteTexture, float spriteScale)
{
    public Subtexture Texture = spriteTexture;
    public float Scale = spriteScale;
    public float Rotation = 0.0f;

    public void Draw(Batcher batcher, Vector2 position)
    {
        batcher.Image(Texture, position, new Vector2(0, 0), new Vector2(Scale), Rotation, Color.White);
    }
}