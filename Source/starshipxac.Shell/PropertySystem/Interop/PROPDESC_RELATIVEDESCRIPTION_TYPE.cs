﻿using System;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762526(v=vs.85).aspx
    /// </remarks>
    internal enum PROPDESC_RELATIVEDESCRIPTION_TYPE
    {
        PDRDT_GENERAL = 0,
        PDRDT_DATE = 1,
        PDRDT_SIZE = 2,
        PDRDT_COUNT = 3,
        PDRDT_REVISION = 4,
        PDRDT_LENGTH = 5,
        PDRDT_DURATION = 6,
        PDRDT_SPEED = 7,
        PDRDT_RATE = 8,
        PDRDT_RATING = 9,
        PDRDT_PRIORITY = 10
    }
}