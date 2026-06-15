using FosterFlow.Graphics;
using System.Numerics;

namespace FosterFlow.Collisions;

public class CircleBound
{
    protected Vector2 Center;
    
    protected float Radius;

    public CircleBound(Vector2 circleCenter, Sprite sprite)
    {
        Center = circleCenter;
        Radius = sprite.Width / 2;
    }
    public CircleBound(Vector2 circleCenter, float radiusLength)
    {
        Center = circleCenter;
        Radius = radiusLength;
    }

    public void ScaleBounds(float scale)
    {
        Radius *= scale;
    }

    public bool Intersects(CircleBound other)
    {
        return Vector2.DistanceSquared(Center, other.Center) < Radius * other.Radius;
    }
}