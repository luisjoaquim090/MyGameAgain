
namespace Game.Interfaces
{
    public interface IPeg: ICell
    {
        enum PegType
        {
            None,
            Player,
            Enemy,
            NPC,
            Item,
            Exit
        }
        PegType Type { get; }
        int Health { get; }
        int Attack { get; }
        int Defense { get; }
        int AttackDamage { get; }
        void MoveTo(ICoord newCoords);

    }
}
