# [FosterFlow](https://github.com/landon-codes/FosterFlow)
A custom engine build off the [Foster Framework](https://github.com/FosterFramework/Foster).

FosterFlow uses the basics provided by Foster to provide features like:
* A simple texture atlas pipeline
* Sprite creation and animated sprites
* Collision detection

## Usage

The first step to use FosterFlow for your games is to install it.
You can do this either by running `dotnet package add landon-codes.FosterFlow` in your project directory or downloading the source code from the GitHub repository and adding a reference to the project in your game.

Once you have access to the package, you can use it in your project by adding a using reference (`using FosterFlow;`) to your project.

*Tip: In game development, developers often use many using statements for the same project to simplify some other calls.*
*You may want to do this for `FosterFlow.Graphics`.*

## Documentation

### Graphics
`FosterFlow.Graphics` contains a handful of tools that make managing graphics simpler in your Foster projects.

#### [Sprite](Graphics/Sprite.cs)
`FosterFlow.Graphics.Sprite` represents sprites in your game. 
Sprites are intended to be included as a public or private property in your entities. They will contain the texture (`Foster.Framework.Subtexture`) that you will show on the screen.

##### Properties
* `public Subtexture Texture`

This is the actual texture that will be drawn to the screen. It can be changed but for animation the [animated sprite](Graphics/AnimatedSprite.cs) would be more suitable.

* `public float Scale`

This is the scale at which your sprite will be drawn. Some frameworks/engines require boilerplate to cleanly handle scaling with pixel art, but Foster handles this automatically.

The scale can be changed but is also set during initialization.

* `public float Rotation`

This is the rotation at which the sprite will be drawn. 
During initialization, it does not need to be explicitly defined because it has a default value of `0.0f` (no rotation).

* `public float Width`

This returns the Width of the sprite.
This width is automatically scaled using the `Scale` property with the `Texture.Width` property.

It is automatically calculated and does not need to be updated.

* `public float Height`

This is the same as the width. It is also automatically updated and does not need to be updated.

##### Methods

* `public Sprite()`

This constructor returns an empty sprite. Nothing is pre-initialized.

* `public Sprite(Subtexture spriteTexture, float spriteScale, float spriteRotation = 0.0f)`

This constructor returns a full, ready for use sprite.

* `Draw(Batcher batcher, Vector2 position)`

This is simple drawing function for your sprite. 
It takes the batcher for your game and the position where you want your sprite to be drawn as input.

* `public void Draw(Batcher batcher, Vector2 position, bool mirrorX, bool mirrorY)`

This function does the same thing as the previous draw function, but allows you to flip the sprite on the x and/or y axis.