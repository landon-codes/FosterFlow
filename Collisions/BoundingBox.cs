using System.Numerics;
using FosterFlow.Graphics;

namespace FosterFlow.Collisions;

/// <summary>
/// A class that represents the hit-box of a sprite.
/// These are not meant to be appended into a class. They are meant to be calculated at the time they are needed.
/// </summary>
public class BoundingBox
{
    private readonly Vector2 _position;
    
    private float _width;
    private float _height;
    
    // Top and bottom Y positions of the bounding box
    public float Top => _position.Y - (_height / 2);
    public float Bottom => _position.Y + (_height / 2);

    // Left and right X positions of the bounding box
    public float Left => _position.X - (_width / 2);
    public float Right => _position.X + (_width / 2);

    public BoundingBox(Sprite sprite, Vector2 position)
    {
        _width = sprite.Width;
        _height = sprite.Height;
        _position = position;
    }
    
    public BoundingBox(Vector2 position, float width, float height)
    {
        _width = width;
        _height = height;
        _position = position;
    }
    
    // Used to cleanly scale your bounding box.
    public void ScaleBoundingBox(float scale)
    {
        _width *= scale;
        _height *= scale;
    }
    
    // Checks if two bounding boxes intersect.
    public bool Intersects(BoundingBox other)
    {
        return !(Right < other.Left ||
                 Left > other.Right ||
                 Bottom < other.Top ||
                 Top > other.Bottom);
    }
}