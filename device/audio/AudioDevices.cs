﻿using Kinect_Wrapper.device.audio.message;
using Kinect_Wrapper.structures;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Kinect_Wrapper.device.audio
{
    public partial class Audio:IAudio
    {
        private IDevice _device;

        public IDevice Device
        {
            get
            {
                return _device;
            }

            set
            {
                if (value == null)
                {
                    //audioSource.Stop();
                    if (SpeechRecognizer != null)
                    {
                        //SpeechRecognizer.RecognizeAsyncStop();
                        SpeechRecognizer.UnloadAllGrammars();
                        SpeechRecognizer.SpeechRecognized -= SreSpeechRecognized;
                        SpeechRecognizer.SpeechHypothesized -= SreSpeechHypothesized;
                        SpeechRecognizer.SpeechRecognitionRejected -= SreSpeechRecognitionRejected;
                        SpeechRecognizer.RecognizeAsyncCancel();
                        SpeechRecognizer = null;
                    }
                    if (worker != null)
                    {
                        //worker.CancelAsync();
                        worker.DoWork -= new DoWorkEventHandler(worker_DoWork);
                        //worker.Dispose();
                        worker = null;
                    }                    
                    return;
                }
                _device = value;
                if (_device.Type == DeviceType.KINECT_1)
                {
                    worker = new BackgroundWorker();
                    worker.DoWork += worker_DoWork;
                    worker.RunWorkerAsync();
                }

            }
        }


    }
}
