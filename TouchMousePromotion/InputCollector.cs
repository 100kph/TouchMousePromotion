using System.Windows;
using System.Windows.Controls;

namespace org.Justforkix.TouchMousePromotion
{
    public class InputCollector : Border
    {
        public delegate void DrawCallbackDelegate(Point pt);

        public static readonly DependencyProperty DrawCallbackProperty = DependencyProperty.Register("DrawCallback", typeof(DrawCallbackDelegate), typeof(InputCollector));

        public InputCollector()
        {
            this.Loaded += onLoaded;
        }

        void onLoaded(object sender, RoutedEventArgs e)
        {
            this.IsManipulationEnabled = true;
        }

        public DrawCallbackDelegate DrawCallback
        {
            get
            {
                DrawCallbackDelegate callback = GetValue(DrawCallbackProperty) as DrawCallbackDelegate;

                return callback;
            }

            set
            {
                SetValue(DrawCallbackProperty, value);
            }
        }

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            Callback(e.GetPosition(this));

            base.OnMouseDown(e);
        }

        private void Callback(Point point)
        {
            DrawCallbackDelegate d = DrawCallback;

            if (d != null)
                d(point);
        }

        protected override void OnTouchDown(System.Windows.Input.TouchEventArgs e)
        {
            Callback(e.GetTouchPoint(this).Position);
            base.OnTouchDown(e);
        }
        protected override void OnTouchMove(System.Windows.Input.TouchEventArgs e)
        {
            Callback(e.GetTouchPoint(this).Position);
            base.OnTouchMove(e);
        }
        protected override void OnTouchEnter(System.Windows.Input.TouchEventArgs e)
        {
            base.OnTouchEnter(e);
        }
        protected override void OnTouchLeave(System.Windows.Input.TouchEventArgs e)
        {
            base.OnTouchLeave(e);
        }
        protected override void OnTouchUp(System.Windows.Input.TouchEventArgs e)
        {
            Callback(e.GetTouchPoint(this).Position);
            base.OnTouchUp(e);
        }
    }
}
