namespace WermyEngine.Overworld;

/// <summary>
/// A map composed of multiple stacked <see cref="MapLayer"/>s.
/// Walkability is determined by scanning all layers: if any non-null tile
/// at a position has <c>IsWalkable = false</c>, that cell is blocked.
/// </summary>
public sealed class OverworldMap
{
    public int Width  { get; }
    public int Height { get; }

    public IReadOnlyList<MapLayer> Layers { get; }

    public OverworldMap(int width, int height, int layerCount)
    {
        Width  = width;
        Height = height;
        var layers = new MapLayer[layerCount];
        for (int i = 0; i < layerCount; i++)
            layers[i] = new MapLayer(width, height);
        Layers = layers;
    }

    /// <summary>
    /// Returns <see langword="false"/> if the tile is out of bounds or any
    /// layer places a non-walkable tile at <paramref name="tileX"/>, <paramref name="tileY"/>.
    /// </summary>
    public bool IsWalkable(int tileX, int tileY)
    {
        if (tileX < 0 || tileX >= Width || tileY < 0 || tileY >= Height)
            return false;
        foreach (var layer in Layers)
        {
            var tile = layer.GetTile(tileX, tileY);
            if (tile is { IsWalkable: false })
                return false;
        }
        return true;
    }
}
