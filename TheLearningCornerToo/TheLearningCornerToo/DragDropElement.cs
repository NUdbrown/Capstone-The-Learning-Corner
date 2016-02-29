using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Kinect.Input;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;
using TheLearningCornerToo.Pages;

namespace TheLearningCornerToo
{
    public class DragDropElement : Decorator, IKinectControl
    {
        bool IKinectControl.IsManipulatable
        {
            get { return true; }
        }

        bool IKinectControl.IsPressable
        {
            get { return false; }
        }

        IKinectController IKinectControl.CreateController(IInputModel inputModel, KinectRegion kinectRegion)
        {
            return new DragDropElementController(inputModel, kinectRegion);
        }
    }

    /// <summary>
    ///     This controller listens to kinect manipulation events associated with an element
    ///     and changes state on the element accordingly.
    /// </summary>
    public class DragDropElementController : IKinectManipulatableController
    {
        private DragDropElement _dragDropElement;

        private ManipulatableModel _inputModel;
        private KinectRegion _kinectRegion;

        public DragDropElementController(IInputModel inputModel, KinectRegion kinectRegion)
        {
            this._inputModel = inputModel as ManipulatableModel;
            this._kinectRegion = kinectRegion;
            var manipulatableModel = this._inputModel;
            if (manipulatableModel != null)
            {
                _dragDropElement = manipulatableModel.Element as DragDropElement;

                manipulatableModel.ManipulationStarted += InputModel_ManipulationStarted;
                manipulatableModel.ManipulationUpdated += InputModel_ManipulationUpdated;
                manipulatableModel.ManipulationCompleted += InputModel_ManipulationCompleted;
            }
        }

        FrameworkElement IKinectController.Element => _inputModel.Element as FrameworkElement;

        ManipulatableModel IKinectManipulatableController.ManipulatableInputModel
        {
            get { return _inputModel; }
        }

        private void InputModel_ManipulationCompleted(object sender, KinectManipulationCompletedEventArgs e)
        {
            
        }

        private void InputModel_ManipulationUpdated(object sender, KinectManipulationUpdatedEventArgs e)
        {
            var parentCanvas = _dragDropElement.Parent as Canvas;
            if (parentCanvas != null)
            {
                var delta = e.Delta.Translation;
                var y = Canvas.GetTop(_dragDropElement);
                var x = Canvas.GetLeft(_dragDropElement);
                if (double.IsNaN(y)) y = 0;
                if (double.IsNaN(x)) x = 0;

                // delta values are 0.0 to 1.0, so we need to scale it to the number of pixels in the kinect region
                var yDelta = delta.Y*_kinectRegion.ActualHeight;
                var xDelta = delta.X*_kinectRegion.ActualWidth;

                Canvas.SetTop(_dragDropElement, y + yDelta);
                Canvas.SetLeft(_dragDropElement, x + xDelta);
            }
        }

        private void InputModel_ManipulationStarted(object sender, KinectManipulationStartedEventArgs e)
        {
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).          
                }

                _kinectRegion = null;
                _inputModel = null;
                _dragDropElement = null;

                _inputModel.ManipulationStarted -= InputModel_ManipulationStarted;
                _inputModel.ManipulationUpdated -= InputModel_ManipulationUpdated;
                _inputModel.ManipulationCompleted -= InputModel_ManipulationCompleted;

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion
    }
}