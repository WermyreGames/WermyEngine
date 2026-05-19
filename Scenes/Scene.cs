using Microsoft.Xna.Framework;
using WermyEngine.Screens;
using WermyEngine.World;

namespace WermyEngine.Scenes;

/// <summary>
/// Base class for a game scene. A scene holds a collection of entities and
/// an optional tile map. Override <see cref="Load"/> to populate the scene,
/// and <see cref="Update"/>/<see cref="Draw"/> to add per-frame logic.
/// </summary>
public abstract class Scene : IScreen
{
    private readonly List<Entity> _entities = new();

    /// <summary>All entities currently active in the scene.</summary>
    public IReadOnlyList<Entity> Entities => _entities;

    /// <summary>The tile map for this scene, or <see langword="null"/> if the scene has no map.</summary>
    public TileMap? Map { get; protected set; }

    /// <summary>
    /// Called once when the scene becomes active.
    /// Create entities, build the map, and load any resources here.
    /// </summary>
    public abstract void Load();

    /// <summary>
    /// Called when this scene is replaced or the game exits.
    /// Release any resources acquired in <see cref="Load"/> here.
    /// </summary>
    public virtual void Unload() { }

    /// <summary>
    /// Updates all entities. Override to add scene-level logic;
    /// call <c>base.Update(gameTime)</c> to keep entity updates running.
    /// </summary>
    public virtual void Update(GameTime gameTime)
    {
        foreach (var entity in _entities)
            entity.Update(gameTime);
    }

    /// <inheritdoc/>
    public abstract void Draw(GameTime gameTime);

    /// <summary>Adds an entity to the scene.</summary>
    protected void AddEntity(Entity entity) => _entities.Add(entity);

    /// <summary>Removes an entity from the scene. Returns <see langword="true"/> if it was found.</summary>
    protected bool RemoveEntity(Entity entity) => _entities.Remove(entity);
}
