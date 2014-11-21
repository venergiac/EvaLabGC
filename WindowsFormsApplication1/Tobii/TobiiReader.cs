//to enable SDK3 define TOBIISDK3 and add reference to Tobii Sample
//#define TOBIISDK3

using System;
using System.Collections.Generic;
using System.Text;
using EVALabGC.ASL;
#if TOBIISDK3
using Tobii.Util;
#else
using TetComp;
using EVALab.Util.Box;
using System.Windows.Forms;
#endif

namespace EVALabGC.Tobii
{
#if TOBIISDK3
	//SDK3 implementation
	public class TobiiReader : TrackerReader
	{
		private long startTime = 0;

		public string name = "";
		public TobiiReader(string name)
		{
			TobiiManager.Istance.Open(name);
		}

		public void Dispose()
		{
			TobiiManager.Istance.Close();
		}


		public void Calibrate()
		{
			TobiiManager.Istance.Calibrate();
		}

		public override void Start()
		{
			TobiiManager.Istance.Start();
			DoAslReaderEvent(startTime, GetTimeMS(), ReaderStatus.Started);
			status = ReaderStatus.Started;
		}

		public override void Stop()
		{
			TobiiManager.Istance.Stop();
			status = ReaderStatus.Stopped;
			DoAslReaderEvent(startTime, GetTimeMS(), ReaderStatus.Stopped);
		}

		public void OnData(double[] data, double time)
		{
			DoAslDataEvent(DoASLEventArgs(data, time));
		}


		private ASLDataEventArgs DoASLEventArgs(double[] data, double time)
		{

			Data dataC = new Data(0, data[0], data[1], time);
			if (stimulusController != null)
			{
				dataC.StimulusId = stimulusController.GetCurrentStimulusId(dataC);
			}

			return new ASLDataEventArgs(dataC);
		}
	}
	

#else 
	//SDK2 implementation
	public class TobiiReader : TrackerReader
	{
		private long startTime = 0;

		public string hostname = "";

		TetClient tetClient;
		TetCalibProc tetCalibProc;

		public TobiiReader(string hostname)
		{
			this.hostname = hostname;

			// Set up the calibration procedure object and it's events
			tetCalibProc = new TetCalibProc();
			_ITetCalibProcEvents_Event tetCalibProcEvents = (_ITetCalibProcEvents_Event)tetCalibProc;
			tetCalibProcEvents.OnCalibrationEnd += new _ITetCalibProcEvents_OnCalibrationEndEventHandler(tetCalibProcEvents_OnCalibrationEnd);
			tetCalibProcEvents.OnKeyDown += new _ITetCalibProcEvents_OnKeyDownEventHandler(tetCalibProcEvents_OnKeyDown);

			// Set up the TET client object and it's events
			tetClient = new TetClient();
			_ITetClientEvents_Event tetClientEvents = (_ITetClientEvents_Event)tetClient;
			tetClientEvents.OnTrackingStarted += new _ITetClientEvents_OnTrackingStartedEventHandler(tetClientEvents_OnTrackingStarted);
			tetClientEvents.OnTrackingStopped += new _ITetClientEvents_OnTrackingStoppedEventHandler(tetClientEvents_OnTrackingStopped);
			tetClientEvents.OnGazeData += new _ITetClientEvents_OnGazeDataEventHandler(tetClientEvents_OnGazeData);
		}

		private void UpdateCalib()
		{
			//TO BE IMPLEMENT
			
		}

		void tetCalibProcEvents_OnKeyDown(int virtualKeyCode)
		{
			// Interrupt the calibration on key events
			if (tetCalibProc.IsCalibrating) tetCalibProc.InterruptCalibration(); // Will trigger OnCalibrationEnd
		}

		void tetCalibProcEvents_OnCalibrationEnd(int hr)
		{
			// Calibration ended, hide the calibration window and update the calibration plot
			tetCalibProc.WindowVisible = false;
			UpdateCalib();
		}

		//trackig
		void tetClientEvents_OnGazeData(ref TetGazeData gazeData)
		{
			System.Diagnostics.Trace.WriteLine(" LeftX: " + gazeData.x_camerapos_lefteye + " LeftGX: " + gazeData.x_gazepos_lefteye);

			float x, y, distance, pupil;
			bool valid = false;

			pupil = x = y = -1.0F;
			distance = 400.0F;

			// Use data only if both left and right eye was found by the eye tracker
			if (gazeData.validity_lefteye == 0 || gazeData.validity_righteye == 0)
			{
				// Let the x, y and distance be the right and left eye average
				if (gazeData.validity_lefteye == 0 && gazeData.validity_righteye != 0)
				{
					pupil = gazeData.diameter_pupil_lefteye * 10;
					x = gazeData.x_gazepos_lefteye;
					y = gazeData.y_gazepos_lefteye;
					distance = gazeData.distance_lefteye;
					valid = true;
				}
				else if (gazeData.validity_lefteye != 0 && gazeData.validity_righteye == 0)
				{
					pupil = gazeData.diameter_pupil_righteye*10;
					x = gazeData.x_gazepos_righteye;
					y = gazeData.y_gazepos_righteye;
					distance = gazeData.distance_righteye;
					valid = true;
				}
				else if (gazeData.validity_lefteye == 0 && gazeData.validity_righteye == 0)
				{
					pupil = (gazeData.diameter_pupil_righteye + gazeData.diameter_pupil_lefteye) / 2 * 10;
					x = (gazeData.x_gazepos_lefteye + gazeData.x_gazepos_righteye) / 2;
					y = (gazeData.y_gazepos_lefteye + gazeData.y_gazepos_righteye) / 2;
					distance = (gazeData.distance_lefteye + gazeData.distance_righteye) / 2;
					valid = true;
				}
			}

			//pushes data
			OnData(new double[] { x, y, pupil }, GetTimeMS() - startTime);
		}

		void tetClientEvents_OnTrackingStopped(int hr)
		{
			context.Log("Tobii stopped (event).");
			this.stimulusController.OnStop();
			if (hr != (int)TetHResults.ITF_S_OK) ExceptionForm.Show(null, string.Format("Error {0} occured while tracking.", hr), new Exception());
			status = ReaderStatus.Stopped;
			DoAslReaderEvent(startTime, GetTimeMS(), ReaderStatus.Stopped);
		}

		void tetClientEvents_OnTrackingStarted()
		{
			context.Log("Tobii started (event).");
			this.stimulusController.OnStart();
			startTime = GetTimeMS();
			status = ReaderStatus.Started;
			DoAslReaderEvent(startTime, startTime, ReaderStatus.Started);
		}

		public void Dispose()
		{

			if (tetCalibProc.IsConnected)
			{
				if (tetCalibProc.IsCalibrating) tetCalibProc.InterruptCalibration();
				tetCalibProc.Disconnect();
			}

			if (tetClient.IsConnected)
			{
				if (tetClient.IsTracking) tetClient.StopTracking();
				tetClient.Disconnect();
			}
		}

		private bool isRecalibrating = false;

		public void Calibrate()
		{
			Calibrate(null);
		}

		public void Calibrate(Screen scrn)
		{
			// Connect the calibration procedure if necessary
			if (!tetCalibProc.IsConnected) tetCalibProc.Connect(hostname, (int)TetConstants.TetConstants_DefaultServerPort);

			// Initiate number of points to be calibrated
			tetCalibProc.NumPoints = TetNumCalibPoints.TetNumCalibPoints_9;

			// Initiate screen
			if (scrn != null) tetCalibProc.DisplayMonitor = scrn.DeviceName; 

			// Initiate window properties and start calibration
			tetCalibProc.WindowTopmost = false;
			tetCalibProc.WindowVisible = true;
			tetCalibProc.StartCalibration(isRecalibrating ? TetCalibType.TetCalibType_Recalib : TetCalibType.TetCalibType_Calib, false);
		}

		public override void Start()
		{
			// Connect to the TET server if necessary
			context.Log("Connecting...");
			if (!tetClient.IsConnected)
			{
				tetClient.Connect(hostname, (int)TetConstants.TetConstants_DefaultServerPort, TetSynchronizationMode.TetSynchronizationMode_Local);
			}
			else
			{
				context.Log("WARNING: trying to connect to an already connected Tobii on " + hostname);
			}
			context.Log("...connected.");

			// Start tracking gaze data
			context.Log("Starting...");
			startTime = GetTimeMS();
			tetClient.StartTracking();
		}

		public override void Stop()
		{
			context.Log("Stopping...");
			if (tetClient.IsTracking)
			{
				tetClient.StopTracking();
			}
			else
			{
				context.Log("WARNING: trying to stop to an already stopped Tobii on " + hostname);
			}
		}

		public void OnData(double[] data, double time)
		{
			DoAslDataEvent(DoASLEventArgs(data, time));
		}

		private ASLDataEventArgs DoASLEventArgs(double[] data, double time)
		{
			Data dataC = new Data(0, data[0], data[1], data[2], time);
			if (stimulusController != null)
			{
				dataC.StimulusId = stimulusController.GetCurrentStimulusId(dataC);
			}

			return new ASLDataEventArgs(dataC);
		}
	}

	#endif
}
