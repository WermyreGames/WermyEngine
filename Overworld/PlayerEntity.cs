using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WermyEngine.Overworld;

/// <summary>
/// The player's overworld avatar. Moves one tile at a time with smooth animation.
/// Movement walkability is supplied as a delegate so the caller can include
/// entity collision on top of map tile collision without coupling this class
/// to the entity list.
/// </summary>
public sealed class PlayerEntity : Entity
{
    private const float MoveTilesPerSecond = 7f;

    private readonly Func<Point, bool> _canMoveTo;

    private Point   _targetTile;
    private Vector2 _targetPixel;

    /// <summary>True while the player is animating between tiles.</summary>
    public bool IsMoving { get; private set; }

    /// <summary>The tile the player is currently facing (last attempted move direction).</summary>
    public Point FacingTile { get; private set; }

    /// <summary>Unit direction vector the player is facing (e.g. (0,-1) = up).</summary>
    public Point FacingDirection { get; private set; }

    public PlayerEntity(int tileX, int tileY, Func<Point, bool> canMoveTo)
        : base("Player", new Color(220, 80, 80), tileX, tileY)
    {
        _canMoveTo      = canMoveTo;
        _targetTile     = TilePosition;
        _targetPixel    = PixelPosition;
        FacingDirection = new Point(0, -1);          // default: facing up
        FacingTile      = new Point(tileX, tileY - 1);
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsMoving)
        {
            var keys = Keyboard.GetState();
            Point dir = Point.Zero;

            if      (keys.IsKeyDown(Keys.Up))    dir = new Point( 0, -1);
            else if (keys.IsKeyDown(Keys.Down))  dir = new Point( 0,  1);
            else if (keys.IsKeyDown(Keys.Left))  dir = new Point(-1,  0);
            else if (keys.IsKeyDown(Keys.Right)) dir = new Point( 1,  0);

            if (dir != Point.Zero)
            {
                // Always update facing so the player "looks" in the pressed direction
                // even if a wall blocks movement — required for interaction detection.
                FacingDirection = dir;
                FacingTile      = _targetTile + dir;

                if (_canMoveTo(FacingTile))
                {
                    _targetTile  = FacingTile;
                    _targetPixel = new Vector2(_targetTile.X * TileSize, _targetTile.Y * TileSize);
                    IsMoving     = true;
                }
            }
        }

        if (IsMoving)
        {
            float step = MoveTilesPerSecond * TileSize * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 diff = _targetPixel - PixelPosition;

            if (diff.LengthSquared() <= step * step)
            {
                PixelPosition = _targetPixel;
                TilePosition  = _targetTile;
                IsMoving      = false;
            }
            else
            {
                PixelPosition += Vector2.Normalize(diff) * step;
            }
        }
    }

    /// <summary>
    /// Draws the player square plus a small directional indicator on the facing edge.
    /// </summary>
    public override void Draw(SpriteBatch sb, Texture2D pixel, SpriteFont font)
    {
        base.Draw(sb, pixel, font);

        int px   = (int)PixelPosition.X + Margin;
        int py   = (int)PixelPosition.Y + Margin;
        int size = TileSize - Margin * 2;   // 26

        const int noseLen  = 4;   // depth (into the square edge)
        const int noseSpan = 8;   // width of the indicator

        Rectangle nose;
        if      (FacingDirection == new Point( 0, -1))   // up
            nose = new Rectangle(px + (size - noseSpan) / 2, py,                          noseSpan, noseLen);
        else if (FacingDirection == new Point( 0,  1))   // down
            nose = new Rectangle(px + (size - noseSpan) / 2, py + size - noseLen,         noseSpan, noseLen);
        else if (FacingDirection == new Point(-1,  0))   // left
            nose = new Rectangle(px,                          py + (size - noseSpan) / 2, noseLen,  noseSpan);
        else                                              // right
            nose = new Rectangle(px + size - noseLen,        py + (size - noseSpan) / 2, noseLen,  noseSpan);

        sb.Draw(pixel, nose, new Color(255, 240, 200));
    }
}
