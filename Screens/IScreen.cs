using Microsoft.Xna.Framework;

namespace WermyEngine.Screens;

public interface IScreen
{
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime);
}
