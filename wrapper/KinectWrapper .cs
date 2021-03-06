﻿using Kinect_Wrapper.device;
using Kinect_Wrapper.device.audio.message;
using Kinect_Wrapper.device.stream;
using Kinect_Wrapper.frame;
using Kinect_Wrapper.statistic;
using Kinect_Wrapper.structures;
using Microsoft.Kinect;
using SharedLibJG.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kinect_Wrapper.device.audio;
using Kinect_Wrapper.device.video;

namespace Kinect_Wrapper.wrapper
{
    public partial class KinectWrapper: IKinectWrapper, INotifyPropertyChanged
    {
        #region singleton 
        private static IKinectWrapper _instance;
        static readonly object _locker = new object();
        public static IKinectWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KinectWrapper();
                }
                return _instance;
            }
        }
        #endregion
        
        
        private KinectWrapper()
        {
            initAudioVideo();
            UIEnable = true;
            initStatistics();
            initStreams();
            initDevices();
            initWorker();
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ImageSource> DisplayImageReady;

        

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }







    }
}
