using System;
using System.Collections.Generic;
using System.Text;
using Tobii.Eyetracking.Sdk;
using Tobii.Eyetracking.Sdk.Time;
using Tobii.Eyetracking.Sdk.Exceptions;
using System.Windows.Forms;

namespace Tobii.Util
{
    public class TobiiManager
    {

        private EyetrackerBrowser _trackerBrowser;
        private Clock _clock;

        private IEyetracker _connectedTracker;
        private SyncManager _syncManager;
        private string _connectionName;
        private bool _isTracking;

        private Dictionary<string, EyetrackerInfo> eyeTrackerAvailable = new Dictionary<string,EyetrackerInfo>();

        private TobiiManager()
        {
            _clock = new Clock();

            _trackerBrowser = new EyetrackerBrowser();
            _trackerBrowser.EyetrackerFound += EyetrackerFound;
            _trackerBrowser.EyetrackerUpdated += EyetrackerUpdated;
            _trackerBrowser.EyetrackerRemoved += EyetrackerRemoved;

            _trackerBrowser.Start();
        }

        private static TobiiManager mngr = null;
        public static TobiiManager Istance
        {
            get
            {
                if (mngr == null) mngr = new TobiiManager();
                return mngr;
            }
        }
        private void EyetrackerFound(object sender, EyetrackerInfoEventArgs e)
        {
            // When an eyetracker is found on the network we add it to the listview
            this.eyeTrackerAvailable.Add(e.EyetrackerInfo.ProductId, e.EyetrackerInfo);
        }

        private void EyetrackerRemoved(object sender, EyetrackerInfoEventArgs e)
        {
            // When an eyetracker disappears from the network we remove it from the listview
            this.eyeTrackerAvailable.Remove(e.EyetrackerInfo.ProductId);
        }

        private void EyetrackerUpdated(object sender, EyetrackerInfoEventArgs e)
        {
            this.eyeTrackerAvailable.Remove(e.EyetrackerInfo.ProductId);
            this.eyeTrackerAvailable.Add(e.EyetrackerInfo.ProductId, e.EyetrackerInfo);
        }

        public void Open(string name)
        {
            Open(eyeTrackerAvailable[name]);
        }

        private void Open(EyetrackerInfo info)
        {
            try
            {
                _connectedTracker = EyetrackerFactory.CreateEyetracker(info);
                _connectedTracker.ConnectionError += new EventHandler<ConnectionErrorEventArgs>(_connectedTracker_ConnectionError);
                _connectionName = info.ProductId;

                _syncManager = new SyncManager(_clock, info);

                _connectedTracker.GazeDataReceived += _connectedTracker_GazeDataReceived;
            }
            finally { Close(); }
        }

        public void Close()
        {
            if (_connectedTracker != null)
            {
                _connectedTracker.GazeDataReceived -= _connectedTracker_GazeDataReceived;
                _connectedTracker.Dispose();
                _connectedTracker = null;
                _connectionName = string.Empty;
                _isTracking = false;

                _syncManager.Dispose();
            }
        }

        public void _connectedTracker_ConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            throw new NotImplementedException("Error on ConnectionError");
        }

        private void _connectedTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            // Send the gaze data to the track status control.
            GazeDataItem gd = e.GazeDataItem;

            //HERE we receive data
            //gd.

            

            if (_syncManager.SyncState.StateFlag == SyncStateFlag.Synchronized)
            {
                Int64 convertedTime = _syncManager.RemoteToLocal(gd.TimeStamp);
                Int64 localTime = _clock.GetTime();
            }
            else
            {
                Console.WriteLine("Warning. Sync state is " + _syncManager.SyncState.StateFlag);
            }
        }

        public void Start()
        {
            if (_isTracking)
            {
                // Unsubscribe from gaze data stream
                _connectedTracker.StopTracking();
                _isTracking = false;
            }
            else
            {
                // Start subscribing to gaze data stream
                _connectedTracker.StartTracking();
                _isTracking = true;
            }
        }

        public void Stop()
        {
            if (_isTracking)
            {
                // Unsubscribe from gaze data stream
                _connectedTracker.StopTracking();
                _isTracking = false;
            }
        }

        public void Calibrate()
        {
            var runner = new CalibrationRunner();

            try
            {
                // Start a new calibration procedure
                var result = runner.RunCalibration(_connectedTracker);

                // Show a calibration plot if everything went OK
                if (result != null)
                {
                    var resultForm = new CalibrationResultForm();
                    resultForm.SetPlotData(result);
                    resultForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Not enough data to create a calibration (or calibration aborted).");
                }
            }
            catch (EyetrackerException ee)
            {
                MessageBox.Show("Failed to calibrate. Got exception " + ee,
                    "Calibration Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}
