namespace Tanks
{
    public class BotConditionComponent : ConditionComponent
    {
        private void Start()
        {
            _health = GameEvents.Singleton.GetBotsHp;
        }
    }
}
