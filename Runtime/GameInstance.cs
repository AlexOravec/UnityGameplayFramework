using UnityEngine;

namespace UnityGameplayFramework.Runtime
{
    public class GameInstance : Object
    {
        private bool _hasLoaded;

        //Static Instance
        private static GameInstance Instance { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            //Set Instance
            Instance = this;

            //Limit FPS to 60
            Application.targetFrameRate = 60;
        }

        public static GameInstance GetGameInstance()
        {
            return Instance;
        }
    }
}