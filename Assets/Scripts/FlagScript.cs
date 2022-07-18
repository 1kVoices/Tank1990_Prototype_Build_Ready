using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class FlagScript : MonoBehaviour
    {
        [SerializeField]
        private SideType _side;
        public SideType GetSideType => _side;
    }
}
