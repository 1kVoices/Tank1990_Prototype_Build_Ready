using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class BotConditionComponent : ConditionComponent
    {
        void Start()
        {
            _health = GameEvents.Singleton.GetBotsHp;
        }
    }
}
