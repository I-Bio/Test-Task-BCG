using Players;

namespace Entities
{
    public interface IDoor
    {
        public bool TryInteract(Stage stage);
    }
}