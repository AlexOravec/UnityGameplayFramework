using UnityEngine;

namespace UnityGameplayFramework.Runtime
{
    public abstract class Pawn : Actor
    {
        //Controller of the Pawn.
        private Controller _controller;


        //Method to set the controller of the pawn.
        public void SetController(Controller newController)
        {
            _controller = newController;
        }

        //Method to get the controller of the pawn.
        public Controller GetController()
        {
            return _controller;
        }

        public virtual void Teleport(Vector3 position)
        {
            transform.position = position;
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            // If the controller is not null
            if (_controller != null)
                // Unpossess the pawn
                _controller.UnPossess();
        }
    }
}