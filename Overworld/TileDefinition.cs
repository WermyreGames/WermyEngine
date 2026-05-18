using Microsoft.Xna.Framework;

namespace WermyEngine.Overworld;

/// <summary>
/// Flyweight data object describing a type of tile.
/// One instance exists per tile type; map cells reference them by pointer —
/// so 1000 grass cells share a single TileDefinition in memory.
/// When adding sprites, replace <see cref="RenderColor"/> with a texture
/// source rectangle pointing into your sprite sheet.
/// </summary>
public sealed class TileDefinition
{
    public string Id          { get; }
    public string Name        { get; }
    public bool   IsWalkable  { get; }
    public Color  RenderColor { get; }

    public TileDefinition(string id, string name, bool isWalkable, Color renderColor)
    {
        Id          = id;
        Name        = name;
        IsWalkable  = isWalkable;
        RenderColor = renderColor;
    }
}
