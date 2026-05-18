using Microsoft.Xna.Framework;

namespace WermyEngine.Overworld;

/// <summary>
/// A stationary NPC that can be talked to by the player.
/// Assign <see cref="OnInteract"/> to define what happens (open dialogue,
/// start a battle, etc.).
/// </summary>
public sealed class NpcEntity : Entity
{
    /// <summary>Invoked when the player faces this NPC and presses the interact key.</summary>
    public Action? OnInteract { get; set; }

    public NpcEntity(string name, Color color, int tileX, int tileY)
        : base(name, color, tileX, tileY) { }

    public void Interact() => OnInteract?.Invoke();
}
