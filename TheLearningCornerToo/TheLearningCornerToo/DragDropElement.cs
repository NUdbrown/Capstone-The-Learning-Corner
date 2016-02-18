using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Kinect.Input;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;

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
        private DragDropElement dragDropElement;

        private ManipulatableModel inputModel;
        private KinectRegion kinectRegion;

        public DragDropElementController(IInputModel inputModel, KinectRegion kinectRegion)
        {
            this.inputModel = inputModel as ManipulatableModel;
            this.kinectRegion = kinectRegion;
            dragDropElement = this.inputModel.Element as DragDropElement;

            this.inputModel.ManipulationStarted += InputModel_ManipulationStarted;
            this.inputModel.ManipulationUpdated += InputModel_ManipulationUpdated;
            this.inputModel.ManipulationCompleted += InputModel_ManipulationCompleted;
        }

        FrameworkElement IKinectController.Element
        {
            get { return inputModel.Element as FrameworkElement; }
        }

        ManipulatableModel IKinectManipulatableController.ManipulatableInputModel
        {
            get { return inputModel; }
        }

        private void InputModel_ManipulationCompleted(object sender, KinectManipulationCompletedEventArgs e)
        {
        }

        private void InputModel_ManipulationUpdated(object sender, KinectManipulationUpdatedEventArgs e)
        {
            var parentCanvas = dragDropElement.Parent as Canvas;
            if (parentCanvas != null)
            {
                var delta = e.Delta.Translation;
                var y = Canvas.GetTop(dragDropElement);
                var x = Canvas.GetLeft(dragDropElement);
                if (double.IsNaN(y)) y = 0;
                if (double.IsNaN(x)) x = 0;

                // delta values are 0.0 to 1.0, so we need to scale it to the number of pixels in the kinect region
                var yDelta = delta.Y*kinectRegion.ActualHeight;
                var xDelta = delta.X*kinectRegion.ActualWidth;

                Canvas.SetTop(dragDropElement, y + yDelta);
                Canvas.SetLeft(dragDropElement, x + xDelta);
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

                kinectRegion = null;
                inputModel = null;
                dragDropElement = null;

                inputModel.ManipulationStarted -= InputModel_ManipulationStarted;
                inputModel.ManipulationUpdated -= InputModel_ManipulationUpdated;
                inputModel.ManipulationCompleted -= InputModel_ManipulationCompleted;

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