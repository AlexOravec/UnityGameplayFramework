using UnityEngine;

namespace UnityGameplayFramework.Runtime
{
    public class CameraManager : Object
    {
        // Reference to the camera
        private Camera _camera;

        //Set the camera
        public void SetView(Object actor)
        {
            _camera = actor.GetComponentInChildren<Camera>();

            // Check if the camera is null
            if (_camera == null) Debug.LogError("Failed to find camera component on actor " + actor.name);

            //Update HUD camera
            if (GameMode.GetHUD() != null)
                GameMode.GetHUD().SetCamera(_camera);
        }

        //Get the camera
        public Camera GetView()
        {
            return _camera;
        }
    }
}