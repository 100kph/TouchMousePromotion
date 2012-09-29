using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace org.Justforkix.TouchMousePromotion
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // http://stackoverflow.com/questions/9687238/why-do-mouse-events-get-promoted-beside-touch-events

        public InputCollector.DrawCallbackDelegate Drawcallback
        {
            get
            {
                return onDrawCallback;
            }
        }
        private string _commentary = String.Empty;
        public string Commentary
        {
            get
            {
                return _commentary;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _commentary = String.Concat(_commentary, value, Environment.NewLine);
                else
                    _commentary = value;
                RaisePropertyChanged("Commentary");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            /* This would promote touch to mouse
             * 
             * playground.IsManipulationEnabled = true;
            playground.TouchUp += new EventHandler<TouchEventArgs>(touchUp);
            playground.TouchDown += new EventHandler<TouchEventArgs>(touchDown);
            playground.TouchMove += new EventHandler<TouchEventArgs>(touchMove);
            playground.TouchEnter += new EventHandler<TouchEventArgs>(touchEnter);
            playground.TouchLeave += new EventHandler<TouchEventArgs>(touchLeave);

            playground.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(onManipulationDelta);
            playground.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(onManipulationStarting);
            playground.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(onManipulationCompleted);
            */

            playground.MouseDown += onMouseDown;
            playground.MouseUp += onMouseUp;
            playground.MouseMove += onMouseMove;

            playground.PreviewMouseDown += onPreviewMouseDown;
            playground.PreviewMouseUp += onPreviewMouseUp;
            playground.PreviewMouseMove += onPreviewMouseMove;
        }

        void onMouseDown(object sender, MouseButtonEventArgs e)
        {
            Commentary = "OnMouseDown";
            SetHandled(e, true);
        }
        void onMouseUp(object sender, MouseButtonEventArgs e)
        {
            Commentary = "OnMouseUp";
            SetHandled(e, true);
        }
        void onMouseMove(object sender, MouseEventArgs e)
        {
            Commentary = "OnMouseMove";
            SetHandled(e, true);
        }

        void onPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Commentary = "OnPreviewMouseDown";
            SetHandled(e, true);
        }
        void onPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Commentary = "OnPreviewMouseUp";
            SetHandled(e, true);
        }
        void onPreviewMouseMove(object sender, MouseEventArgs e)
        {
            Commentary = "OnPreviewMouseMove";
            SetHandled(e, true);
        }

        void touchDown(object sender, TouchEventArgs e)
        {
            Commentary = "OnTouchDown";
            Plot(e.GetTouchPoint(playground).Position);
            SetHandled(e, true);
        }

        void touchUp(object sender, TouchEventArgs e)
        {
            Commentary = "OnTouchUp";
            Plot(e.GetTouchPoint(playground).Position);
            ClearPoints();
            SetHandled(e, true);
        }

        private void ClearPoints()
        {
            plot.Points.Clear();
        }

        private void Plot(Point point)
        {
            plot.Points.Add(point);
        }
        void touchMove(object sender, TouchEventArgs e)
        {
            Commentary = "OnTouchMove";
            Plot(e.GetTouchPoint(playground).Position);
            SetHandled(e, true);
        }
        void touchLeave(object sender, TouchEventArgs e)
        {
            Commentary = "OnTouchLeave";
            SetHandled(e, true);
        }

        void touchEnter(object sender, TouchEventArgs e)
        {
            Commentary = "OnTouchEnter";
            SetHandled(e, true);
        }


        void onManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Commentary = "onManipulationDelta";
            SetHandled(e, true);
        }

        void onManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            Commentary = "onManipulationStarting";
            SetHandled(e, true);
        }
        void onManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            Commentary = "onManipulationCompleted";
            SetHandled(e, true);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name)
        {
            var func = PropertyChanged;
            if (func != null)
                func(this, new PropertyChangedEventArgs(name));
        }
        private void SetHandled(InputEventArgs e, bool handled)
        {
            e.Handled = handled;
        }

        private void onDrawCallback(Point pt)
        {
            Plot(pt);
        }
    }
}
