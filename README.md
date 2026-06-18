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

#### [Animated Sprite](Graphics/AnimatedSprite.cs)
The animated sprite allows you to use sprites that automatically manage different animations.
This class inherits from [Sprite](Graphics/Sprite.cs).

##### Properties

* `private Dictionary<string, List<Subtexture>> _animations`

This contains the different animations that are attached to the sprite.
It is interacted with using public methods.

* `private string _currentAnimation`

This stores the key to the current animation that is being played from _animations.

* `private int _currentFrame`

This stores the current frame of the animation being played. 
It is reset to 0 whenever a new animation is played and updated in the sprite's `Update` method.

* `private float _animationDelay`

This is the delay between animation frames in seconds.

* `private float _elapsedTime`

This keeps track of the time that has passed between animation frames.
It is used in the `Update` method to check if it has surpassed `_elapsedTime`, and continue to the next frame.

##### Methods

* `public AnimatedSprite(Dictionary<string, List<Subtexture>> spriteAnimations, string startingAnimation, float animationDelay, float spriteScale = 1, float spriteRotation = 0)`

This is the constructor for an animated sprite. It takes the following arguments:
1. The dictionary containing the animations for your sprite
2. The key to the first animation that will be played
3. The delay between frames
4. The scale at which your sprite will be drawn
5. The rotation at which your sprite will be drawn

* `public void PlayAnimation(string animationName)`

Plays an animation from the dictionary. The input is the key to the animation in the dictionary.

* `private void UpdateTexture()`

This is an internally used function that updates the `Texture` property to the proper frame.
It uses the `_animations`, `_currentAnimation`, and `_currentFrame` properties.

* `public void Update(float deltaTime)`

This updates the sprite. It must be called in your games update loop to function properly.

#### [AtlasGenerator](Graphics/AtlasGenerator.cs)
The atlas generator encapsulates the `Packer` from Foster to simplify the process of generating texture atlases.

You can also use Aseprite file (including animated ones) and .png files. The only required inputs are a root directory for assets and the paths to the assets to be included themselves.

*Note: Foster does not automatically pack assets with the project.*
*This must be done in the .csproj file.*

You can add this snippet to your project configuration file to include an `Assets` folder in your project:
```xml
<!-- Include Assets folder in builds -->
<ItemGroup>
    <None Include="Assets/**/*.*">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
</ItemGroup>
```

##### Properties

* `private readonly GraphicsDevice _graphicsDevice`

A reference to your game's graphic device.

* `private Texture _atlas`

This is the atlas that will be generated.

Notice how it is not public. Subtextures are accessed through the `GetTexture` method, which takes the name of a texture as input.
Texture names are inherited from the file name. For example, if you have a file named "Coin.ase", its name will be "Coin." For animated textures, you append the index (starting at 0) to the end of the name.
So if our coin sprite was animated (in the context of a single animated Aseprite file), and had three frame, these would be the names of the frames:
1. Coin0
2. Coin1
3. Coin2

* `private readonly Packer _packer`

This is the packer that is used internally.

* `public string ContentRoot { get; }`

This contains the path to the root directory where your image assets are stored.
This saves you frome having to include "Assets" (or whatever directory you choose to use) in a `Path.Combine` method.

It is still recomended to use `Path.Combine` when you have subfolders in your asset directory (which is likely) to ensure cross-platform compatibility. 
Just remember NOT to include your root directory, as paths are always combined with `ContentRoot` internally.

* `private readonly List<string> _assets`

This is the list of assets that is provided to the packer.

* `private readonly Dictionary<string, Subtexture> _textures`

This contains the textures that are generated from the packer.

It is private because it is exposed through methods.

##### Methods

* `public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice)`

This provides the minimal setup with a content root and graphics device.
You can add or remove assets from the generator using the `AddAsset` and `RemoveAsset` methods.
You will tell the generator to pack your assets using the `Pack` method.

* `public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice, List<string> assts)`

This provides a full generator and automatically packs the assets for you.

* `public void AddAsset(string assetPath)`

This adds an asset to the generator.

**IMPORTANT: This method does not automatically pack assets once they are added. If you add a new asset you will have to re-pack your atlas.**

* `public void RemoveAsset(string assetPath)`

This removes an asset from the generator.

This would really only be useful if you want to prevent a texture from being used, but in order for that to work you will have to repack the atlas.

* `public Subtexture GetTexture(string textureName)`

Returns a texture from the atlas. 

Textures share the name as their file name, without the extension.
Animated Aseprite files append the frame index (starting at 0) to the end of a frame's name.

* `public void Pack()`

This packs the atlas and resets the texture collection if there are any.