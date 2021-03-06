﻿using Rca.PoolLabIo.Helpers;
using Rca.PoolLabIo.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Rca.PoolLabIo
{
    /// <summary>
    /// Communication class fpr the PoolLab photometer
    /// </summary>
    public static class PoolLab
    {
        #region Constants
        /// <summary>
        /// The first byte of every data-block
        /// </summary>
        public const byte PREAMBLE = 0xAB;

        /// <summary>
        /// Maximum allowed command length
        /// </summary>
        public const int COMMAND_LENGTH = 20;

        /// <summary>
        /// BLE UUIDS of the PoolLab Bluetooth API
        /// </summary>
        public struct Uuids
        {
            /// <summary>
            /// BLE PoolLab communictation service
            /// </summary>
            public static Guid PoolLabSvc = new Guid("A7EE04A9-507B-4910-A528-B619D5501924");

            /// <summary>
            /// BLE characteristic for sending commands to the PoolLab device
            /// </summary>
            public static Guid CmdMosi = new Guid("91BFA536-3036-4901-8813-3635FCED7B90");

            /// <summary>
            /// BLE characteristic for reading commands from the PoolLab device
            /// </summary>
            public static Guid CmdMiso = new Guid("2FF18B59-195D-4EE1-B78C-0CBDE3EFF9C2");

            /// <summary>
            /// BLE characteristic for receiving notifications from the PoolLab device
            /// </summary>
            public static Guid MisoSig = new Guid("C2296C06-C7E0-4657-B42E-C8330826454C");
        }

        #endregion Constants

        #region Properties
        public static bool IsConnected { get; set; }

        /// <summary>
        /// Write/Read timeout in ms
        /// </summary>
        public static int Timeout { get; set; } = 5000;

        #endregion Properties

        #region Fields
        static GattCharacteristic cmdMisoCharacteristic = null;
        static GattCharacteristic cmdMosiCharacteristic = null;
        static GattCharacteristic misoSigCharacteristic = null;

        static BluetoothLEDevice deviceReference;

        static TaskCompletionSource<PoolLabInformation> tcsPoolLabInfo = null;
        static TaskCompletionSource<Measurement[]> tcsMeasurements = null;

        #endregion Fields

        #region Services
        public static bool IsPoolLabServiceDevice(DeviceInformation devInfo)
        {
            //debugDeviceList.Add(devInfo);

            string devId = devInfo.Id.ToLower();

            return devId.Contains(Uuids.PoolLabSvc.ToString().ToLower());

            //return string.Equals(devInfo.Id, Uuids.PoolLabSvc.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        public static async Task Connect(string deviceId)
        {
            deviceReference = await BluetoothLEDevice.FromIdAsync(deviceId);

            await deviceReference.RequestAccessAsync();

            GattDeviceServicesResult result = await deviceReference.GetGattServicesAsync();
            //Allways check result!
            if (result.Status == GattCommunicationStatus.Success)
            {
                //Put following two lines in try/catch to or check for null!!
                var characs = await result.Services.Single(s => s.Uuid == Uuids.PoolLabSvc).GetCharacteristicsAsync();

                cmdMisoCharacteristic = characs.Characteristics.Single(c => c.Uuid == Uuids.CmdMiso);
                cmdMosiCharacteristic = characs.Characteristics.Single(c => c.Uuid == Uuids.CmdMosi);
                misoSigCharacteristic = characs.Characteristics.Single(c => c.Uuid == Uuids.MisoSig);

#if DEBUG
                if (BitConverter.IsLittleEndian)
                    Debug.WriteLine("LittleEndian");
                else
                    Debug.WriteLine("BigEndian");

                if (cmdMisoCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                    Debug.WriteLine("CommandMISO characteristic supports reading from it.");
                if (cmdMisoCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                    Debug.WriteLine("CommandMISO characteristic supports writing.");
                if (cmdMisoCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.ReliableWrites))
                    Debug.WriteLine("MISO_Signal characteristic supports reliable writing.");
                if (cmdMisoCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                    Debug.WriteLine("MISO_Signal characteristic supports writing without response.");
                if (cmdMisoCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                    Debug.WriteLine("CommandMISO characteristic supports subscribing to notifications.");

                if (cmdMosiCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                    Debug.WriteLine("CommandMOSI characteristic supports reading from it.");
                if (cmdMosiCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                    Debug.WriteLine("CommandMOSI characteristic supports writing.");
                if (cmdMosiCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.ReliableWrites))
                    Debug.WriteLine("MISO_Signal characteristic supports reliable writing.");
                if (cmdMosiCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                    Debug.WriteLine("MISO_Signal characteristic supports writing without response.");
                if (cmdMosiCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                    Debug.WriteLine("CommandMOSI characteristic supports subscribing to notifications.");

                if (misoSigCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                    Debug.WriteLine("MISO_Signal characteristic supports reading from it.");
                if (misoSigCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                    Debug.WriteLine("MISO_Signal characteristic supports writing.");
                if (misoSigCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.ReliableWrites))
                    Debug.WriteLine("MISO_Signal characteristic supports reliable writing.");
                if (misoSigCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                    Debug.WriteLine("MISO_Signal characteristic supports writing without response.");
                if (misoSigCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                    Debug.WriteLine("MISO_Signal characteristic supports subscribing to notifications.");
                if (misoSigCharacteristic.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Indicate))
                    Debug.WriteLine("MISO_Signal characteristic supports indicate.");
#endif

                IsConnected = true;
            }
            else
            {
                Debug.WriteLine("No services found");
            }
        }

        /// <summary>
        /// Get information about the connected PoolLab
        /// </summary>
        /// <returns>PoolLab device infromations</returns>
        public static async Task<PoolLabInformation> GetInfoAsync()
        {
            tcsPoolLabInfo = new TaskCompletionSource<PoolLabInformation>();

            //Bsp: https://stackoverflow.com/questions/18760252/timeout-an-async-method-implemented-with-taskcompletionsource
            //var cts = new CancellationTokenSource(Timeout);
            //cts.Token.Register(() => tcsPoolLabInfo.TrySetCanceled(), useSynchronizationContext: false);

            CmdGetInfo();
            
            return await tcsPoolLabInfo.Task;
        }

        /// <summary>
        /// Get measurements form the connected PoolLab
        /// </summary>
        /// <param name="count">Number of measurements to get</param>
        /// <returns>Measurement data</returns>
        public static async Task<IEnumerable<Measurement>>GetMeasurementsAsync(int count)
        {
            var measurements = new List<Measurement>();

            for (int i = 0; i <= count / 8; i++)
            {
                tcsMeasurements = new TaskCompletionSource<Measurement[]>();

                //Bsp: https://stackoverflow.com/questions/18760252/timeout-an-async-method-implemented-with-taskcompletionsource
                //var cts = new CancellationTokenSource(Timeout);
                //cts.Token.Register(() => tcsMeasurements.TrySetCanceled(), useSynchronizationContext: false);

                CmdGetMeasurements(i * 8);

                measurements.AddRange(await tcsMeasurements.Task);
                tcsMeasurements = null;
            }

            return measurements;
        }

        #region Commands
        /// <summary>
        /// Get information about the device
        /// </summary>
        public static async Task CmdGetInfo()
        {
            await RegisterNotification(ReadPoolLabInformation);
            
            SendCommand(PREAMBLE, (byte)CommandType.PCMD_API_GET_INFO);
        }

        /// <summary>
        /// Set the PoolLab´s date/time to current system-time
        /// </summary>
        public static async Task CmdSetTime()
        {
            await CmdSetTime(DateTime.Now);
        }

        /// <summary>
        /// Set the PoolLab´s date/time
        /// </summary>
        /// <param name="time">Settime</param>
        public static async Task CmdSetTime(DateTime time)
        {
            await RegisterNotification(ReadResult);

            var cmdBytes = new List<byte> { PREAMBLE, (byte)CommandType.PCMD_API_SET_TIME };
            cmdBytes.AddRange(BitConverter.GetBytes(time.ToUnixTime()));

            SendCommand(cmdBytes.ToArray());
        }

        /// <summary>
        /// Immediately restarts the PoolLab (BLE connection will fail)
        /// </summary>
        public static void CmdRestart()
        {
            SendCommand(PREAMBLE, (byte)CommandType.PCMD_API_RESET_DEVICE);
        }

        /// <summary>
        /// Set the device into sleep-mode/standby
        /// </summary>
        public static void CmdShutDown()
        {
            SendCommand(PREAMBLE, (byte)CommandType.PCMD_API_SLEEP_DEVICE);
        }

        /// <summary>
        /// Get measurements form PoolLab
        /// </summary>
        /// <param name="index">Startindex - first measurement ID</param>
        public static async Task CmdGetMeasurements(int index = 0)
        {
            await RegisterNotification(ReadMeasurements);

            byte cell = (byte)(index / 16);
            byte selector = (byte)(index / 8 % 2);
                       
            SendCommand(PREAMBLE, (byte)CommandType.PCMD_API_GET_MEASURES, 0x00, cell, 0x00, selector);
        }

        /// <summary>
        /// Delete all saved measurements from the PoolLab
        /// </summary>
        public static async Task CmdResetMeasurements()
        {
            await RegisterNotification(ReadResult);

            SendCommand(PREAMBLE, (byte)CommandType.PCMD_API_RESET_MEASURES);
        }

        #endregion Commands

        #region Write/Read
        public static async Task SendCommand(params byte[] cmd)
        {
            if (cmd.Length > COMMAND_LENGTH)
                throw new ArgumentOutOfRangeException("Invalide command length (" + cmd.Length + "), maximal allowed are " + COMMAND_LENGTH + " bytes.");

            //zero-padding
            var zeroPaddedcmd = new byte[COMMAND_LENGTH];
            Array.Copy(cmd, 0, zeroPaddedcmd, 0, cmd.Length);

            await SendRawCommand(cmdMosiCharacteristic, zeroPaddedcmd);
        }

        public static async Task SendCommand(IBuffer buffer)
        {
            await RegisterNotification();

            CryptographicBuffer.CopyToByteArray(buffer, out byte[] cmd);
            await SendCommand(cmd);
        }

        public static async Task<byte[]> ReadCmdMiso()
        {
            var result = await cmdMisoCharacteristic.ReadValueAsync(BluetoothCacheMode.Uncached);
            if (result.Status == GattCommunicationStatus.Success)
            {
                CryptographicBuffer.CopyToByteArray(result.Value, out byte[] data);

                Debug.WriteLine("CommandMISO value: " + BitConverter.ToString(data));

                return data;
            }
            else
            {
                Debug.WriteLine($"Error during read value from CommandMISO ({result.ProtocolError})");
                return null;
            }
        }

        #endregion Write/Read

        #endregion Services

        #region Internal services
        private static async void ReadResult(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            misoSigCharacteristic.ValueChanged -= ReadResult;

            var buffer = await ReadCmdMiso();

            if (buffer != null && buffer.Length > 0)
            {
                using (var reader = new BinaryReader(new MemoryStream(buffer)))
                {
                    var preamble = reader.ReadByte();

                    if (preamble != PREAMBLE)
                    {
                        Debug.WriteLine("invalide result data");
                    }
                    else
                    {
                        var res = (ResultCode)reader.ReadByte();
                        //TODO: Ergebnis verarbeiten...
                    }
                }
            }
            else
            {
                //TODO: No data...
            }
        }

        private static async void ReadMeasurements(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            misoSigCharacteristic.ValueChanged -= ReadMeasurements;

            var buffer = await ReadCmdMiso();

            if (buffer != null && buffer.Length > 0)
            {
                var measurements = new List<Measurement>();
                using (var reader = new BinaryReader(new MemoryStream(buffer)))
                {
                    var preamble = reader.ReadByte();

                    if (preamble != PREAMBLE)
                    {
                        Debug.WriteLine("invalide measurement data");
                    }
                    else
                    {
                        int d = (buffer.Length - 1) / 16;
                        for (int i = 0; i < (buffer.Length - 1) / 16; i++)
                        {
                            var meas = Measurement.FromBuffer(reader.ReadBytes(16));
                            if (meas.Type != 0)
                                measurements.Add(meas);
                        }
                    }
                }
                tcsMeasurements?.TrySetResult(measurements.ToArray());
            }
            else
            {
                //TODO: No data...
            }
        }

        private static async void ReadPoolLabInformation(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            misoSigCharacteristic.ValueChanged -= ReadPoolLabInformation;

            var buffer = await ReadCmdMiso();

            if (buffer != null && buffer.Length > 0)
            {
                //TODO: Buffer plausibilisieren
                var info = PoolLabInformation.FromBuffer(buffer, 1);

                tcsPoolLabInfo?.TrySetResult(info);
            }
            else
            {
                //TODO: No data...
            }
        }

        private static async void MisoSig_ValueChangedAsync(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            misoSigCharacteristic.ValueChanged -= MisoSig_ValueChangedAsync;

            GattReadResult result = await cmdMisoCharacteristic.ReadValueAsync(BluetoothCacheMode.Uncached);
            CryptographicBuffer.CopyToByteArray(result.Value, out byte[] data);

            Debug.WriteLine("MISO_Sig notification");
            Debug.WriteLine("CommandMISO value: " + BitConverter.ToString(data));
        }

        private static async Task SendRawCommand(GattCharacteristic characteristic, byte[] cmd)
        {
            if (cmd.Length > COMMAND_LENGTH)
                throw new OverflowException("Maximal " + COMMAND_LENGTH + " byte");

            if (cmd[0] != PREAMBLE) //undocumented commands not supported
                throw new ArgumentException("Command must be start with 0xAB");
            
            Debug.WriteLine("Sending command: " + BitConverter.ToString(cmd));

            var response = await characteristic.WriteValueAsync(cmd.AsBuffer(), GattWriteOption.WriteWithResponse);
            Debug.WriteLine("WriteResult: " + response);

            //Alternative Abfrage mit WriteValueWithResultAsync()
            //var response = await characteristic.WriteValueWithResultAsync(cmd.AsBuffer());
            //Debug.WriteLine("WriteResult: " + response.Status);
        }

        private static async Task RegisterNotification(TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs> receiver = null)
        {
            //MisoSig.ValueChanged -= MisoSig_ValueChangedAsync;

            try
            {
                GattCommunicationStatus notifyResult = GattCommunicationStatus.Unreachable;

                //Write the CCCD in order for server to send notifications.               
                notifyResult = await misoSigCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.None);

                if (notifyResult == GattCommunicationStatus.Success)
                {
                    notifyResult = await misoSigCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                    if (notifyResult == GattCommunicationStatus.Success)
                    {
                        Debug.WriteLine("Successfully registered for notifications on MISO_Signal");

                        if (receiver == null)
                            misoSigCharacteristic.ValueChanged += MisoSig_ValueChangedAsync;
                        else
                            misoSigCharacteristic.ValueChanged += receiver;

                    }
                    else
                    {
                        Debug.WriteLine("Error registering for notifications on MISO_Signal: " + notifyResult);
                    }
                }
                else
                {
                    Debug.WriteLine("Error deregistering notifications on MISO_Signal: " + notifyResult);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.Write(ex.Message);
            }

            await Task.Delay(TimeSpan.FromMilliseconds(250));
        }

        #endregion Internal services
    }
}
