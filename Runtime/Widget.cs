using UnityEngine;

namespace UnityGameplayFramework.Runtime
{
    public class Widget : Object
    {
        [Header("Rendering")] [SerializeField] private int sortingOrder;

        // Show widget
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        // Hide widget
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual bool IsVisible()
        {
            return gameObject.activeSelf;
        }

        public int GetSortingOrder()
        {
            return sortingOrder;
        }
    }
}