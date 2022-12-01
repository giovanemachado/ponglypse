using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RouteTeamStudio
{
    public class Movement : Controller
    {
        PlayerData _playerData;
        Rigidbody2D _rigidbody2;

        public override void OnAwake(ScriptableObject dataObject)
        {
            _playerData = (PlayerData) dataObject;
            _rigidbody2 = GetComponent<Rigidbody2D>();
        }

        public void Move(float horizontal, float vertical)
        {
            _rigidbody2.velocity = new Vector2(horizontal * _playerData.Speed, vertical * _playerData.Speed);
        }
    }
}
