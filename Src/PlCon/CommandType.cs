using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlCon
{
    /// <summary>
    /// Available commands and their IDs
    /// </summary>
    public enum CommandType : byte
    {
        PCMD_API_GET_INFO = 0x1,
        PCMD_API_SET_TIME,
        PCMD_API_RESET_DEVICE,
        PCMD_API_SLEEP_DEVICE,
        PCMD_API_GET_MEASURES,
        PCMD_API_RESET_MEASURES
    }
}
