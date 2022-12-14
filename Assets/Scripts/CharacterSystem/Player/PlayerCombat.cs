namespace CharacterSystem
{
    public class PlayerCombat : CharacterCombat
    {
        void Start()
        {
            this.Stats = GetComponent<PlayerStats>();
        }
    }
}
