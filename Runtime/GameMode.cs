﻿using UnityEngine;

namespace UnityGameplayFramework.Runtime
{
    public class GameMode : Object
    {
        private static GameMode _latestInstance;

        [Header("Game Mode Prefabs")] [SerializeField] [Tooltip("Player Controller Prefab")]
        public PlayerController playerController;

        [SerializeField] [Tooltip("Player Character Prefab")]
        public Character playerCharacter;

        [SerializeField] [Tooltip("HUD Prefab")]
        public HUD hud;

        private HUD _hudInstance;

        //Saved instances
        private PlayerController _playerControllerInstance;
        private Character _playerCharacterInstance;

        protected override void Initialize()
        {
            base.Initialize();

            RegisterInstance();

            Spawn();
        }

        protected void RegisterInstance()
        {
            _latestInstance = this;
        }

        //Spawn player
        protected void Spawn()
        {
            //Check if there is already some PlayerController
            if (FindFirstObjectByType<PlayerController>() != null)
            {
                //Save PlayerController
                _playerControllerInstance = FindFirstObjectByType<PlayerController>();
            }
            else
            {
                //Spawn Player Controller
                _playerControllerInstance = Instantiate(playerController, Vector3.zero, Quaternion.identity);

                //Set parent
                _playerControllerInstance.transform.SetParent(transform);
            }

            //Spawn HUD
            _hudInstance = Instantiate(hud, Vector3.zero, Quaternion.identity);

            //Set parent
            _hudInstance.transform.SetParent(transform);

            Respawn();
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            if (_playerControllerInstance != null) Destroy(_playerControllerInstance);

            if (_playerCharacterInstance != null) Destroy(_playerCharacterInstance);

            if (_hudInstance != null) Destroy(_hudInstance);
        }

        // Respawns player
        public void Respawn()
        {
            // Find player start
            var playerStart = GetPlayerStart();

            var playerTransform = playerStart != null ? playerStart.transform : null;

            var worldCharacter = FindFirstObjectByType<Character>();

            //Check if there is already some PlayerCharacter
            if (worldCharacter != null)
            {
                //Possess the character
                _playerControllerInstance.Possess(worldCharacter);

                //Save PlayerCharacter
                _playerCharacterInstance = worldCharacter;
            }
            else
            {
                // Spawn player controller
                if (playerTransform != null)
                {
                    _playerCharacterInstance =
                        Instantiate(playerCharacter, playerTransform.position, playerTransform.rotation);

                    //Set parent
                    _playerCharacterInstance.transform.SetParent(transform);
                }
                else
                {
                    Debug.LogWarning("No player start found");
                }

                //If player character is not null
                if (_playerCharacterInstance != null)
                    //Possess the character
                    _playerControllerInstance.Possess(_playerCharacterInstance);
            }
        }

        protected virtual Transform GetPlayerStart()
        {
            var playerStart = FindFirstObjectByType<PlayerStart>();

            if (playerStart != null) return playerStart.transform;

            Debug.LogWarning("No player start found, returning null");
            return gameObject.transform;
        }

        //Static methods
        public static GameMode GetGameMode()
        {
            return _latestInstance;
        }

        public static PlayerController GetPlayerController()
        {
            return GetGameMode()?._playerControllerInstance;
        }

        public static Character GetPlayerCharacter()
        {
            return GetGameMode()?._playerCharacterInstance;
        }

        public static HUD GetHUD()
        {
            return GetGameMode()?._hudInstance;
        }
    }
}