using Microsoft.Xna.Framework;

namespace WermyEngine.Scenes;

/// <summary>
/// Manages the active <see cref="Scene"/>. Transitioning to a new scene
/// automatically unloads the previous one.
/// Forward your game's <c>Update</c> and <c>Draw</c> calls here.
/// </summary>
public sealed class SceneManager
{
    /// <summary>The currently active scene, or <see langword="null"/> if none is loaded.</summary>
    public Scene? Current { get; private set; }

    /// <summary>
    /// Unloads the current scene (if any) then loads and activates <paramref name="scene"/>.
    /// </summary>
    public void LoadScene(Scene scene)
    {
        Current?.Unload();
        Current = scene;
        Current.Load();
    }

    /// <summary>Unloads the current scene and sets <see cref="Current"/> to <see langword="null"/>.</summary>
    public void UnloadScene()
    {
        Current?.Unload();
        Current = null;
    }

    /// <summary>Forwards the update tick to the active scene.</summary>
    public void Update(GameTime gameTime) => Current?.Update(gameTime);

    /// <summary>Forwards the draw tick to the active scene.</summary>
    public void Draw(GameTime gameTime) => Current?.Draw(gameTime);
}
