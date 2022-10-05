public class Spell
{
    public Spell(Channel channel, IChannelable channeled, SpellType spellType, float power_points = 0, float drainPower = 0.001)
    {
        public float drainPower = drainPower;

        Channel channel = channel;
        float power_points = power_points;
        SpellType spellType = spellType;
        IChannelable channeled = channeled;
    }

    public void ChannelInterrupted()
    {
        Debug.Log("Channel Interrupted for this spell");
    }


    public void AddPower(float amount)
    {
        power_points += amount;
    }


    public void Decrease(float decrease)
    {
        power_points = Mathf.Min(0, power_points - decrease);
        if (power_points == 0)
        {
            Extinguish();
        }

    }

    public void Extinguish()
    {
        Debug.Log($"Spell of type {spellType} went extinguished");
    }

    public void Release()
    {
        Debug.Log($"Releasing {power_points} points of type {spellType}");
    }
}

public enum SpellType
{
    BASIC,
    FIRE,
    WATER
}