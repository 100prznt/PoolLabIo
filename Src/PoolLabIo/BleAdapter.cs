using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.UI.Core;

namespace Rca.PoolLabIo
{
    public class BleAdapter
    {
        #region Properties
        public List<DeviceInformation> DeviceInformations { get; set; } = null;

        #endregion Properties

        #region Member
        private DeviceWatcher m_DeviceWatcher = null;
        private TypedEventHandler<DeviceWatcher, DeviceInformation> m_HandlerAdded = null;
        private TypedEventHandler<DeviceWatcher, DeviceInformationUpdate> m_HandlerUpdated = null;
        private TypedEventHandler<DeviceWatcher, DeviceInformationUpdate> m_HandlerRemoved = null;
        private TypedEventHandler<DeviceWatcher, object> m_HandlerEnumCompleted = null;
        private TypedEventHandler<DeviceWatcher, object> m_HandlerStopped = null;

        #endregion Member

        #region Constructor
        public BleAdapter()
        {
            DeviceInformations = new List<DeviceInformation>();
        }

        #endregion Constructor

        #region Services
        /// <summary>
        /// Start the watcher, to search for BLE devices
        /// </summary>
        public void StartWatcher()
        {
            DeviceInformations.Clear();

            // Create all the event handlers
            m_HandlerAdded = new TypedEventHandler<DeviceWatcher, DeviceInformation>((watcher, deviceInfo) =>
            {
                DeviceInformations.Add(deviceInfo);
                Debug.WriteLine($"Device added. ID: {deviceInfo.Id} Name: {deviceInfo.Name}");

                DeviceAddedEvent(deviceInfo);
            });

            m_HandlerUpdated = new TypedEventHandler<DeviceWatcher, DeviceInformationUpdate>((watcher, deviceInfoUpdate) =>
            {
                foreach (DeviceInformation deviceInfo in DeviceInformations)
                {
                    if (deviceInfo.Id == deviceInfoUpdate.Id)
                    {
                        deviceInfo.Update(deviceInfoUpdate);
                        Debug.WriteLine($"Device updated: {deviceInfo.Name}");
                        break;
                    }
                }
            });

            m_HandlerRemoved = new TypedEventHandler<DeviceWatcher, DeviceInformationUpdate>((watcher, deviceInfoUpdate) =>
            {
                // Find the corresponding DeviceInformation in the collection and remove it
                foreach (DeviceInformation deviceInfo in DeviceInformations)
                {
                    if (deviceInfo.Id == deviceInfoUpdate.Id)
                    {
                        DeviceInformations.Remove(deviceInfo);
                        Debug.WriteLine($"Device removed: {deviceInfo.Name}");
                        break;
                    }
                }
            });

            m_HandlerEnumCompleted = new TypedEventHandler<DeviceWatcher, Object>((watcher, obj) =>
            {
                Debug.WriteLine($"Enum completed, found {DeviceInformations.Count} devices.");

                EnumCompletedEvent?.Invoke();
            });

            m_HandlerStopped = new TypedEventHandler<DeviceWatcher, Object>((watcher, obj) =>
            {
                Debug.WriteLine("Enum stopped.");
            });


            m_DeviceWatcher = DeviceInformation.CreateWatcher(
                    "", // AQS Filter string
                    null, // requested properties
                    DeviceInformationKind.AssociationEndpointService);



            //Hook up handlers for the watcher events before starting the watcher
            m_DeviceWatcher.Added += m_HandlerAdded;
            m_DeviceWatcher.Updated += m_HandlerUpdated;
            m_DeviceWatcher.Removed += m_HandlerRemoved;
            m_DeviceWatcher.EnumerationCompleted += m_HandlerEnumCompleted;
            m_DeviceWatcher.Stopped += m_HandlerStopped;

            m_DeviceWatcher.Start();

        }

        /// <summary>
        /// Stops the watcher
        /// </summary>
        public void StopWatcher()
        {
            // First unhook all event handlers except the stopped handler. This ensures our
            // event handlers don't get called after stop, as stop won't block for any "in flight" 
            // event handler calls.  We leave the stopped handler as it's guaranteed to only be called
            // once and we'll use it to know when the query is completely stopped. 
            m_DeviceWatcher.Added -= m_HandlerAdded;
            m_DeviceWatcher.Updated -= m_HandlerUpdated;
            m_DeviceWatcher.Removed -= m_HandlerRemoved;
            m_DeviceWatcher.EnumerationCompleted -= m_HandlerEnumCompleted;

            if (DeviceWatcherStatus.Started == m_DeviceWatcher.Status || DeviceWatcherStatus.EnumerationCompleted == m_DeviceWatcher.Status)
            {
                m_DeviceWatcher.Stop();
            }
        }

        #endregion Services

        #region Events
        public delegate void EnumCompletedEventHandler();
        public event EnumCompletedEventHandler EnumCompletedEvent;

        public delegate void DeviceAddedEventHandler(DeviceInformation devInfo);
        public event DeviceAddedEventHandler DeviceAddedEvent;

        #endregion Services
    }
}
