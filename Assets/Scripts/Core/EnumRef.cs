public class EnumRef
{
    public enum CharacterType
    {
        Player,
        Npc,
        Mob,
        Boss
    }

    public enum DamageType
    {
        Physical,
        Magical,
        Environment,
        AbnormalStatus
    }

    public enum ItemCategory
    {
        Weapon,
        Armor,
        Potion,
        Skill,
        Currency,
        Miscellaneous
    }

    public enum EquipmentSlot
    {
        Head,
        UpperBody,
        LowerBody,
        MainHand
    }

    // P_ ~= positive effect and vice versa
    public enum BuffType
    {
        P_STR,
        P_INT,
        P_PhysicalResist,
        P_MagicalResist,
        P_HP,
        P_HPMax,
        P_MP,
        P_MPMax,
        N_STR,
        N_INT,
        N_PhysicalResist,
        N_MagicalResist,
        N_HP,
        N_HPMax,
        N_MP,
        N_MPMax
    }
}
