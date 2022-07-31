using System;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using NAudio.CoreAudioApi;
using Microsoft.Win32;
using System.Threading;

namespace ArduinoComunicacionSerie
{
    public partial class frmMain : Form
    {
        System.IO.Ports.SerialPort ArduinoPort;
        public MMDeviceCollection devices;
        public int deviceSelected;
        public bool toogleMuted;
        public bool toogleCamera;

        public frmMain()
        {
            InitializeComponent();
            timerChangeStatus.Stop();

            MMDeviceEnumerator en = new MMDeviceEnumerator();
            devices = en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

            for (int i = 0; i < devices.Count; i++)
            {
                //Console.WriteLine(devices[i].ToString());
                cmbDevices.Items.Add(devices[i].ToString());
                if (devices[i].ToString().Contains("Micrófono")) { cmbDevices.SelectedIndex = i; deviceSelected = i; }
            }
        }

        private void onLoadFrm(object sender, EventArgs e)
        {
            try
            {
                ArduinoPort = new SerialPort();
                ArduinoPort.PortName = "COM4";
                ArduinoPort.BaudRate = 9600;
                ArduinoPort.DataReceived += new SerialDataReceivedEventHandler(arduinoReceivedSerial);
                ArduinoPort.Open();
                timerChangeStatus.Start();
                chkToogleWebcam.Checked = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Conecte Arduino e inicie de nuevo la aplicación", "No está conectado Arduino");
                timerChangeStatus.Stop();
                this.Close();
            }

        }

        private void arduinoReceivedSerial(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                int input = ArduinoPort.ReadChar();
                Console.WriteLine(input);
                if (input.Equals('z'))
                {
                    Console.WriteLine("***** CONNECTED *****");
                }

                if (input.Equals('g'))
                {
                    Console.WriteLine("***** BUTTON PRESSED MICRO *****");
                    MMDeviceEnumerator en = new MMDeviceEnumerator();
                    var devices = en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
                    toogleMuted = !toogleMuted;
                    devices[deviceSelected].AudioSessionManager.AudioSessionControl.SimpleAudioVolume.Mute = toogleMuted;
                }
                else if (input.Equals('h'))
                {
                    Console.WriteLine("***** BUTTON PRESSED CAMERA *****");
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam", true);
                    Console.WriteLine(key);
                    toogleCamera = !toogleCamera;
                    if (toogleCamera)
                    {
                        //adding/editing a value 
                        key.SetValue("Value", "Allow");
                    }
                    else
                    {
                        key.SetValue("Value", "Deny");
                    }
                    key.Close();
                }
            }
        }

        private void chkToogleMute_CheckedChanged(object sender, EventArgs e)
        {
            MMDeviceEnumerator en = new MMDeviceEnumerator();
            var devices = en.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            toogleMuted = chkToogleMute.Checked;
            devices[cmbDevices.SelectedIndex].AudioSessionManager.AudioSessionControl.SimpleAudioVolume.Mute = toogleMuted;
        }

        private void chkToogleWebcam_CheckedChanged(object sender, EventArgs e)
        {
            toogleCamera = chkToogleWebcam.Checked;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam", true);
            Console.WriteLine(key);
            if (chkToogleWebcam.Checked)
            {
                //adding/editing a value 
                key.SetValue("Value", "Allow");
            }
            else
            {
                key.SetValue("Value", "Deny");
            }
            key.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ArduinoPort.IsOpen) ArduinoPort.Close();
        }

        private void timerChangeStatus_Tick(object sender, EventArgs e)
        {
            chkToogleMute.Checked = toogleMuted;
            chkToogleWebcam.Checked = toogleCamera;

            try
            {
                if (cmbDevices.SelectedItem != null)
                {
                    Console.WriteLine("Mute      : " + devices[cmbDevices.SelectedIndex].AudioSessionManager.AudioSessionControl.SimpleAudioVolume.Mute);
                    Console.WriteLine("Webcam    : " + IsWebCamInUse());
                    Console.WriteLine("Microphone: " + IsMicrophoneInUse());

                    if (IsWebCamInUse())
                    {
                        chkCamera.Checked = true;
                        ArduinoPort.Write("e");
                    }
                    else
                    {
                        chkCamera.Checked = false;
                        ArduinoPort.Write("f");
                    }

                    if (IsMicrophoneInUse())
                    {
                        chkMicrophone.Checked = true;
                        ArduinoPort.Write("a");
                    }
                    else
                    {
                        chkMicrophone.Checked = false;
                        ArduinoPort.Write("b");
                    }

                    if (devices[cmbDevices.SelectedIndex].AudioSessionManager.AudioSessionControl.SimpleAudioVolume.Mute)
                    {
                        chkMicroMute.Checked = true;
                        ArduinoPort.Write("c");
                    }
                    else
                    {
                        chkMicroMute.Checked = false;
                        ArduinoPort.Write("d");
                    }
                }
            }catch(Exception ex)
            {
                timerChangeStatus.Stop();
                MessageBox.Show("Se ha perdido la conexión con Arduino\nVuelve a abrir la aplicación una vez esté solucionado.", "Comprueba la conexión con Arduino");
                this.Close();
            }


        }

        private bool IsWebCamInUse()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\NonPackaged"))
            {
                foreach (var subKeyName in key.GetSubKeyNames())
                {
                    using (var subKey = key.OpenSubKey(subKeyName))
                    {
                        if (subKey.GetValueNames().Contains("LastUsedTimeStop"))
                        {
                            var endTime = subKey.GetValue("LastUsedTimeStop") is long ? (long)subKey.GetValue("LastUsedTimeStop") : -1;
                            if (endTime <= 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool IsMicrophoneInUse()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\microphone\NonPackaged"))
            {
                foreach (var subKeyName in key.GetSubKeyNames())
                {
                    using (var subKey = key.OpenSubKey(subKeyName))
                    {
                        if (subKey.GetValueNames().Contains("LastUsedTimeStop"))
                        {
                            var endTime = subKey.GetValue("LastUsedTimeStop") is long ? (long)subKey.GetValue("LastUsedTimeStop") : -1;
                            if (endTime <= 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

    }
}
