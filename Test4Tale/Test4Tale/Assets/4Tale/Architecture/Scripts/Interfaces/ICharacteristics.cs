namespace _4Tale
{
    public interface ICharacteristics
    {
        public int HP { get; }
        public int ArmorHP { get; }

        public void TakeDamage(int damage);


    }
}
