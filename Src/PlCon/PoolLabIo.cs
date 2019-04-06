using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace PlCon
{
    public static class PoolLabIo
    {
        public struct Uuids
        {
            public static Guid PoolLabSvc = new Guid("A7EE04A9-507B-4910-A528-B619D5501924");

            public static Guid CmdMosi = new Guid("91BFA536-3036-4901-8813-3635FCED7B90");
            public static Guid CmdMiso = new Guid("2FF18B59-195D-4EE1-B78C-0CBDE3EFF9C2");
            public static Guid MisoSig = new Guid("C2296C06-C7E0-4657-B42E-C8330826454C");
        }

        public const byte PREAMBLE = 0xAB;

        static GattCharacteristic CmdMiso = null;
        static GattCharacteristic CmdMosi = null;
        static GattCharacteristic MisoSig = null;

        static BluetoothLEDevice deviceReference;

        public static bool IsConnected { get; set; }

        public static bool IsPoolLabServiceDevice(DeviceInformation devInfo)
        {
            //TODO: Uuid struct verwenden
            return devInfo.Id.ToLower().Contains("a7ee04a9-507b-4910-a528-b619d5501924");
        }

        public static async Task Connect(string deviceId)
        {
            deviceReference = await BluetoothLEDevice.FromIdAsync(deviceId);

            GattDeviceServicesResult result = await deviceReference.GetGattServicesAsync();
            //Allways check result!
            if (result.Status == GattCommunicationStatus.Success)
            {
                //Put following two lines in try/catch to or check for null!!
                var characs = await result.Services.Single(s => s.Uuid == Uuids.PoolLabSvc).GetCharacteristicsAsync();

                CmdMiso = characs.Characteristics.Single(c => c.Uuid == Uuids.CmdMiso);
                CmdMosi = characs.Characteristics.Single(c => c.Uuid == Uuids.CmdMosi);
                MisoSig = characs.Characteristics.Single(c => c.Uuid == Uuids.MisoSig);

#if DEBUG
                if (BitConverter.IsLittleEndian)
                    Debug.WriteLine("LittleEndian");
                else
                    Debug.WriteLine("BigEndian");

                if (CmdMiso.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                    Debug.WriteLine("CommandMISO characteristic supports reading from it.");
                if (CmdMiso.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                    Debug.WriteLine("CommandMISO characteristic supports writing.");
                if (CmdMiso.CharacteristicProperties.HasFlag(GattCharacteristicProperties.ReliableWrites))
                    Debug.WriteLine("MISO_Signal characteristic supports reliable writing.");
                if (CmdMiso.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                    Debug.WriteLine("MISO_Signal characteristic supports writing without response.");
                if (CmdMiso.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                    Debug.WriteLine("CommandMISO characteristic supports subscribing to notifications.");

                if (CmdMosi.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                    Debug.WriteLine("CommandMOSI characteristic supports reading from it.");
                if (CmdMosi.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                    Debug.WriteLine("CommandMOSI characteristic supports writing.");
                if (CmdMosi.CharacteristicProperties.HasFlag(GattCharacteristicProperties.ReliableWrites))
                    Debug.WriteLine("MISO_Signal characteristic supports reliable writing.");
                if (CmdMosi.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                    Debug.WriteLine("MISO_Signal characteristic supports writing without response.");
                if (CmdMosi.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                    Debug.WriteLine("CommandMOSI characteristic supports subscribing to notifications.");

                if (MisoSig.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                    Debug.WriteLine("MISO_Signal characteristic supports reading from it.");
                if (MisoSig.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Write))
                    Debug.WriteLine("MISO_Signal characteristic supports writing.");
                if (MisoSig.CharacteristicProperties.HasFlag(GattCharacteristicProperties.ReliableWrites))
                    Debug.WriteLine("MISO_Signal characteristic supports reliable writing.");
                if (MisoSig.CharacteristicProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                    Debug.WriteLine("MISO_Signal characteristic supports writing without response.");
                if (MisoSig.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                    Debug.WriteLine("MISO_Signal characteristic supports subscribing to notifications.");
                if (MisoSig.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Indicate))
                    Debug.WriteLine("MISO_Signal characteristic supports indicate.");
#endif

                

                IsConnected = true;
            }
            else
            {
                Debug.WriteLine("No services found");
            }
        }

        //public delegate void CommandMISOReceiverHandler(GattCharacteristic sender, GattValueChangedEventArgs args);

        private static async Task RegisterNotification(TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs> receiver = null)
        {
            //MisoSig.ValueChanged -= MisoSig_ValueChangedAsync;

            try
            {
                GattCommunicationStatus notifyResult = GattCommunicationStatus.Unreachable;

                //Write the CCCD in order for server to send notifications.               
                notifyResult = await MisoSig.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.None);

                if (notifyResult == GattCommunicationStatus.Success)
                {
                    notifyResult = await MisoSig.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                    if (notifyResult == GattCommunicationStatus.Success)
                    {
                        Debug.WriteLine("Successfully registered for notifications on MISO_Signal");

                        if (receiver == null)
                            MisoSig.ValueChanged += MisoSig_ValueChangedAsync;
                        else
                            MisoSig.ValueChanged += receiver;

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
        }

        

        public static async Task SendCommand(params byte[] cmd)
        {
            await SendRawCommand(CmdMosi, cmd);
        }

        public static async Task SendCommand(IBuffer buffer)
        {
            await RegisterNotification();

            CryptographicBuffer.CopyToByteArray(buffer, out byte[] cmd);
            await SendCommand(cmd);
        }

        private static async Task SendRawCommand(GattCharacteristic characteristic, byte[] cmd)
        {
            if (cmd.Length > 128)
                throw new OverflowException("Maximal 128 byte");

            if (cmd[0] != 0xAB)
                throw new ArgumentException("Command must be start with 0xAB");

            //await RegisterNotification();

            Debug.WriteLine("Sending command: " + BitConverter.ToString(cmd));
            
            var response = await characteristic.WriteValueWithResultAsync(cmd.AsBuffer());

            Debug.WriteLine("WriteResult: " + response.Status);
        }

        public static async void Restart()
        {
            var cmd = new byte[] { 0xAB, 0x03 };

            Debug.WriteLine("Sending: " + BitConverter.ToString(cmd));

            var res = await CmdMosi.WriteValueWithResultAsync(cmd.AsBuffer());

            Debug.WriteLine("Restart Result: " + res.Status.ToString());
        }

        public static async void ShutDown()
        {
            var cmd = new byte[] { 0xAB, 0x04 };

            Debug.WriteLine("Sending: " + BitConverter.ToString(cmd));
            
            var res = await CmdMosi.WriteValueWithResultAsync(cmd.AsBuffer());

            Debug.WriteLine("Restart Result: " + res.Status.ToString());
        }

        public static async void GetMeasurements()
        {
            await RegisterNotification(ReadMeasurements);

            SendCommand(PREAMBLE, 0x05);
        }

        public static async void SetTime()
        {
            await RegisterNotification();

            //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var cmdBytes = new List<byte> { PREAMBLE, 0x02, 0x00 };

            cmdBytes.AddRange(BitConverter.GetBytes(DateTime.Now.ToUnixTime()));

            var cmd = cmdBytes.ToArray();

            Debug.WriteLine("Sending: " + BitConverter.ToString(cmd));

            var res = await CmdMosi.WriteValueWithResultAsync(cmd.AsBuffer());

            Debug.WriteLine("Set Time Result: " + res.Status.ToString());
        }

        private static async void MisoSig_ValueChangedAsync(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            MisoSig.ValueChanged -= MisoSig_ValueChangedAsync;

            GattReadResult result = await CmdMiso.ReadValueAsync(BluetoothCacheMode.Uncached);
            CryptographicBuffer.CopyToByteArray(result.Value, out byte[] data);

            Debug.WriteLine("MISO_Sig notification");
            Debug.WriteLine("CommandMISO value: " + BitConverter.ToString(data));
        }

        public static async Task<byte[]> ReadCmdMiso()
        {
            var result = await CmdMiso.ReadValueAsync(BluetoothCacheMode.Uncached);
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

        [Obsolete]
        public static async Task<byte[]> ReadCmdMisoOld()
        {
            GattReadResult result = await CmdMiso.ReadValueAsync(BluetoothCacheMode.Uncached);
            if (result.Status == GattCommunicationStatus.Success)
            {
                var reader = DataReader.FromBuffer(result.Value);
                byte[] data = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(data);
                Debug.WriteLine("Manual reading");
                Debug.WriteLine("CommandMISO value: " + BitConverter.ToString(data));
                return data;
            }

            return null;
        }

        private static async void ReadMeasurements(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            MisoSig.ValueChanged -= ReadMeasurements;

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
                            measurements.Add(Measurement.FromBuffer(reader.ReadBytes(16)));
                    }
                }
            }
        }
    }
}
