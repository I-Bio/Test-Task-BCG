using Players;

namespace Entities
{
    public interface IInteractable
    {
        public void Accept(IPlayerVisitor visitor);
    }
}