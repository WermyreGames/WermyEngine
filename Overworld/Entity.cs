using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WermyEngine.Overworld;

/// <summary>
/// Base class for all overworld objects (player, NPCs, items).
/// Tracks position both in tile-grid coordinates and in screen pixels
/// so subclasses can animate smoothly between tiles.
/// </summary>
public abstract class Entity
{
    protected const int TileSize = 32;
    protected const int Margin   = 3;

    public string  Name          { get; }
    public Color   Color         { get; }
    public Point   TilePosition  { get; protected set; }
    public Vector2 PixelPosition { get; protected set; }

    protected Entity(string name, Color color, int tileX, int tileY)
    {
        Name          = name;
        Color         = color;
        TilePosition  = new Point(tileX, tileY);
        PixelPosition = new Vector2(tileX * TileSize, tileY * TileSize);
    }

    public virtual void Update(GameTime gameTime) { }

    /// <summary>Draws the entity as a colored square with its initial letter centred inside.</summary>
    public virtual void Draw(SpriteBatch sb, Texture2D pixel, SpriteFont font)
    {
        int px   = (int)PixelPosition.X + Margin;
        int py   = (int)PixelPosition.Y + Margin;
        int size = TileSize - Margin * 2;

        sb.Draw(pixel, new Rectangle(px, py, size, size), Color);

        var initial  = Name[..1];
        var textSize = font.MeasureString(initial) * 0.65f;
        sb.DrawString(font, initial,
            new Vector2(px + (size - textSize.X) / 2f, py + (size - textSize.Y) / 2f),
            Color.White, 0f, Vector2.Zero, 0.65f, SpriteEffects.None, 0f);
    }
}
