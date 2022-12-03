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
        Rigidbody2D _rigidbody;
        float horizontal = 0;
        float vertical = 0;
        public override void OnAwake(ScriptableObject dataObject)
        {
            _playerData = (PlayerData) dataObject;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + new Vector2(horizontal * _playerData.Speed, vertical * _playerData.Speed) * Time.fixedDeltaTime);
        }
    }
}
