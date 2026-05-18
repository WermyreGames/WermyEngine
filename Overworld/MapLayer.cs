namespace WermyEngine.Overworld;

/// <summary>
/// A single 2D layer of tile references.
/// Cells are <see langword="null"/> where the layer is transparent (no tile drawn,
/// no walkability contributed). Stack multiple layers in <see cref="OverworldMap"/>
/// to build ground, decoration, and collision layers independently.
/// </summary>
public sealed class MapLayer
{
    private readonly TileDefinition?[,] _tiles;

    public int Width  { get; }
    public int Height { get; }

    public MapLayer(int width, int height)
    {
        Width  = width;
        Height = height;
        _tiles = new TileDefinition?[width, height];
    }

    public TileDefinition? GetTile(int x, int y) =>
        x >= 0 && x < Width && y >= 0 && y < Height ? _tiles[x, y] : null;

    public void SetTile(int x, int y, TileDefinition? tile)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
            _tiles[x, y] = tile;
    }
}
