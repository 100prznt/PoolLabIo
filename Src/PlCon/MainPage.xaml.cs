using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace PlCon
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Adapter m_Adapter;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            m_Adapter = new Adapter();

            m_Adapter.StartWatcher();
            m_Adapter.DeviceAddedEvent += Adapter_DeviceAddedEvent;
            m_Adapter.EnumCompletedEvent += Adapter_HandlerEnumCompleted;

        }

        private async void Adapter_DeviceAddedEvent(DeviceInformation devInfo)
        {
            if (PoolLabIo.IsPoolLabServiceDevice(devInfo))
            {
                m_Adapter.StopWatcher();
                Debug.WriteLine("Connect PoolLab");

                await PoolLabIo.Connect(devInfo.Id);




                //await Task.Delay(TimeSpan.FromSeconds(2));

                //byte[] cmd = new byte[127];
                //cmd[0] = 0x01;
                ////cmd[1] = 0x00;
                ////cmd[2] = 0x01;
                ////cmd[3] = 0x01;
                ////cmd[4] = 0x01;
                ////cmd[5] = 0x01;

                //PoolLabIo.SendCommand(cmd);
            }
        }

        private void Adapter_HandlerEnumCompleted()
        {
            Debug.WriteLine("ENUM END");

            if (PoolLabIo.IsConnected == false)
                Debug.WriteLine("PoolLab not found!");
        }

        private async void Btn_Read_Click(object sender, RoutedEventArgs e)
        {
            var res = await PoolLabIo.ReadCmdMiso();
        }

        private async void Btn_Send_Click(object sender, RoutedEventArgs e)
        {
            var cmdBytes = txt_Cmd.Text.Split(' ');

            var writer = new DataWriter() { ByteOrder = ByteOrder.LittleEndian };
            foreach (var b in cmdBytes)
            {
                byte cmdByte = Convert.ToByte(b, 16);
                writer.WriteByte(cmdByte);
            }

            await PoolLabIo.SendCommand(writer.DetachBuffer());
        }

        private void Btn_SetTime_Click(object sender, RoutedEventArgs e)
        {
            PoolLabIo.SetTime();
        }

        private void Btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            PoolLabIo.Restart();
        }

        private void Btn_ShutDown_Click(object sender, RoutedEventArgs e)
        {
            PoolLabIo.ShutDown();
        }

        private void Btn_GetMeas_Click(object sender, RoutedEventArgs e)
        {
            PoolLabIo.GetMeasurements();
        }
    }
}
