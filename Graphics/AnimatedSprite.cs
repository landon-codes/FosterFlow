using System.Collections.Generic;
using Foster.Framework;

namespace FosterFlow.Graphics;

public class AnimatedSprite: Sprite
{
    // Contains the different animations for a sprite organized by name
    private Dictionary<string, List<Subtexture>> _animations;
    
    // The current animation that is being played from the dictionary ↑
    private string _currentAnimation;
    private int _currentFrame;

    // The delay between animation frames in seconds
    private float _animationDelay;
    
    // The amount of time that has passed between frames
    private float _elapsedTime;

    public AnimatedSprite() { }

    /// <summary>
    /// The main constructor for animated sprites
    /// </summary>
    /// <param name="spriteAnimations">The dictionary containing the different animations for your sprite</param>
    /// <param name="startingAnimation">The key to the animation your sprite will start using from the dictionary</param>
    /// <param name="animationDelay">The floating point value delay in seconds between animation frames</param>
    /// <param name="spriteScale">The scale at which your sprite will be drawn</param>
    /// <param name="spriteRotation">The initial rotation your sprite will have.</param>
    public AnimatedSprite(Dictionary<string, List<Subtexture>> spriteAnimations,
        string startingAnimation,
        float animationDelay,
        float spriteScale = 1,
        float spriteRotation = 0)
    {
        _animations = spriteAnimations;
        _currentAnimation = startingAnimation;
        _animationDelay = animationDelay;
        _elapsedTime = 0;
        
        Scale = spriteScale;
        Rotation = spriteRotation;
    }

    public void PlayAnimation(string animationName)
    {
        _currentAnimation = animationName;
        
        // Resets the frame so it will start at the first frame.
        _currentFrame = 0;
        UpdateTexture();
    }

    // Set's the texture to the current frame in the current animation
    private void UpdateTexture()
    {
        Texture = _animations[_currentAnimation][_currentFrame];
    }

    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;

        // Updates the frame and time values
        if (_elapsedTime >= _animationDelay)
        {
            _elapsedTime -= _animationDelay;
            _currentFrame = (_currentFrame + 1) % _animations[_currentAnimation].Count;

            UpdateTexture();
        }
    }
}