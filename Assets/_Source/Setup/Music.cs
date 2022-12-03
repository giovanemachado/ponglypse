using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudio
{
    public class Music : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
