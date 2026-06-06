using Foster.Framework;
using System.Numerics;

namespace FosterFlow.Graphics;

public class Sprite(Subtexture spriteTexture, float spriteScale, float spriteRotation = 0.0f)
{
    private Subtexture _texture = spriteTexture;
    public Subtexture Texture
    {
        set => _texture = value;
    }

    private float _scale = spriteScale;
    public float Scale
    {
        get => _scale;
        set => _scale = value; 
    }

    private float _rotation = spriteRotation;
    public float Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }

    public float Width => _texture.Width * _scale;
    public float Height => _texture.Height * _scale;

    public void Draw(Batcher batcher, Vector2 position)
    {
        batcher.Image(_texture, position, new Vector2(0, 0), new Vector2(_scale), _rotation, Color.White);
    }
}