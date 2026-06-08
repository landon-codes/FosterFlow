using Foster.Framework;
using System.Numerics;

namespace FosterFlow.Graphics;

/// <summary>
/// Represents sprites
/// </summary>
/// <param name="spriteTexture">A Foster.Framework.Graphics.SubTexture that represents the texture of the sprite</param>
/// <param name="spriteScale">A floating point integer representing the scale of the sprite </param>
/// <param name="spriteRotation">A floating point integer representing the rotation of the sprite (defaults to 0.0)</param>
public class Sprite(Subtexture spriteTexture, float spriteScale, float spriteRotation = 0.0f)
{
    public Subtexture Texture = spriteTexture;

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

    public float Width => Texture.Width * _scale;
    public float Height => Texture.Height * _scale;

    public void Draw(Batcher batcher, Vector2 position)
    {
        batcher.Image(Texture, position, new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f), new Vector2(_scale), _rotation, Color.White);
    }
}