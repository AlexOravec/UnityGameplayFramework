using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityGameplayFramework.Runtime
{
    [RequireComponent(typeof(Canvas))]
    public class HUD : Object
    {
        // List of widgets on the screen
        private readonly List<Widget> _widgets = new();

        // Reference to the canvas
        private Canvas _canvas;

        // Initialize the HUD
        protected override void Initialize()
        {
            base.Initialize();

            _canvas = GetComponent<Canvas>();

            //Find all widgets children
            var widgets = GetComponentsInChildren<Widget>();

            // Add all widgets to the list
            _widgets.AddRange(widgets);
        }

        public void SetCamera(Camera newCamera)
        {
            _canvas.worldCamera = newCamera;
        }

        // Add to screen
        public Widget AddToScreen(Widget widget)
        {
            //Instantiate widget
            var widgetInstance = Instantiate(widget, _canvas.transform);

            // Add to the list
            _widgets.Add(widgetInstance);

            // Return widget
            return widgetInstance;
        }

        protected override void BeginPlay()
        {
            base.BeginPlay();

            // Sort widgets in hierarchy
            SortWidgetsInHierarchy();
        }

        private void SortWidgetsInHierarchy()
        {
            var sortedWidgets = _widgets.OrderBy(w => w.GetSortingOrder()).ToList();

            for (var i = 0; i < sortedWidgets.Count; i++) sortedWidgets[i].transform.SetSiblingIndex(i);
        }


        // Remove from screen   
        public void RemoveFromScreen(Widget widget)
        {
            // If contains
            if (_widgets.Contains(widget))
            {
                // Remove from the list
                _widgets.Remove(widget);

                // Destroy widget
                Destroy(widget.gameObject);
            }
        }

        //Get widget by type
        public T GetWidget<T>() where T : Widget
        {
            // Find widget
            var widget = _widgets.Find(w => w is T);

            // Return widget
            return widget as T;
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            // Destroy all widgets
            foreach (var widget in _widgets) Destroy(widget.gameObject);

            // Find all widgets children
            var widgets = GetComponentsInChildren<Widget>();

            // Destroy all widgets children
            foreach (var widget in widgets) Destroy(widget.gameObject);
        }


        // Hide all widgets
        public Widget[] HideAllWidgets()
        {
            var widgets = _widgets.Where(w => w.gameObject.activeSelf).ToArray();

            // Hide all widgets
            foreach (var widget in _widgets) widget.Hide();

            // Return widgets
            return widgets;
        }

        // Show all widgets
        public void ShowWidgets(Widget[] widgets)
        {
            // Show all widgets
            foreach (var widget in widgets) widget.Show();
        }
    }
}