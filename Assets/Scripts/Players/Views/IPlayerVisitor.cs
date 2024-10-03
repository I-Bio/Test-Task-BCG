using Entities;

namespace Players
{
    public interface IPlayerVisitor
    {
        public void Visit(IObstacle obstacle);

        public void Visit(IMoney money);

        public void Visit(IFlag flag);

        public void Visit(IDoor door);

        public void Visit(IRotationTrigger trigger);
    }
}