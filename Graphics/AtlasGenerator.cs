using System.Security.Cryptography.X509Certificates;
using Foster.Framework;
using System.IO;
using System.Collections.Generic;

namespace FosterFlow.Graphics;

/// <summary>
///  A class that uses Aseprite files and Foster's Packer class to generate
///  texture atlases for your game.
/// </summary>
public class AtlasGenerator
{
    private readonly GraphicsDevice _graphicsDevice;
    
    private Texture _atlas;
    
    // The packer that will be used internally to pack images.
    private readonly Packer _packer = new();
    
    // The root directory where all assets will be found.
    public string ContentRoot { get; set; }

    // The paths to the assets relevant to the ContentRoot.
    private readonly List<string> _assets;

    // This dictionary will contain all the textures that are used in an atlas.
    public Dictionary<string, Subtexture> Textures;
    
    public AtlasGenerator() { }

    public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice)
    {
        ContentRoot = contentRoot;
        _graphicsDevice = graphicsDevice;
    }

    public AtlasGenerator(string contentRoot, GraphicsDevice graphicsDevice, List<string> assets)
    {
        ContentRoot = contentRoot;
        _graphicsDevice = graphicsDevice;
        _assets = assets;

        Pack();
    }

    // Make sure to never include the folder when adding an asset.
    // The root folder for assets is always handeled using ContentRoot. 
    public void AddAsset(string assetPath) => _assets.Add(Path.Combine(ContentRoot, assetPath));

    public void Pack()
    {
        // Loads the assets that were given.
        foreach (string asset in _assets)
        {
            Aseprite file = new Aseprite(Path.Combine(ContentRoot, asset));
            Image[] fileFrames = file.RenderAllFrames();

            // Adds the images and their frames (if animated) to the packer.
            if (fileFrames.Length > 1)
                for (int i = 0; i < fileFrames.Length; i++)
                {
                    // For sprites with multiple frames, returns names like, "spriteName-0" or "spriteName-1".
                    string assetName = Path.GetFileNameWithoutExtension(asset);
                    _packer.Add($"{assetName}{i}", fileFrames[i]);
                }
            else // Used for single frame assets.
            {
                string assetName = Path.GetFileNameWithoutExtension(asset);
                _packer.Add(assetName, fileFrames[0]);
            }

            foreach (Image image in fileFrames) image.Dispose();

            Packer.Output atlasOutput = _packer.Pack();

            // Generates the atlas and adds entries to the Textures dictionary.
            _atlas = new Texture(_graphicsDevice, atlasOutput.Pages[0]);
            foreach (Packer.Entry entry in atlasOutput.Entries)
            {
                Textures[entry.Name] = new Subtexture(_atlas, entry.Source, entry.Frame);
            }
        }
    }
}