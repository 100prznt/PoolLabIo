# Bugs in BLE Docu

Version 2 from 02/03/2022

## Commandlength
Kommandos mit einer Länge von 128 bit können erfolgreich an das PoolLAB gesendet werden, allerdings ist keine Reaktion feststellbar und es wird auch keine Antwort gesendet.
Kommandos mit einer Länge von 20 bit, werden entsprechend der Doku verarbeitet.
Ob es sich hier allerdings um ein Windows Problem handelt, kann ich nicht eindeutig sagen.


## Page 8:
Commandlist is incomplete.

### Missing commands:
        /// <summary>
        /// Increase PoolLab's LCD contrast by 1 level.
        /// </summary>
        PCMD_API_SET_CONTRAST_PLUS = 0x08,
        /// <summary>
        /// Decrease PoolLab®'s LCD contrast by 1 level.
        /// </summary>
        PCMD_API_SET_CONTRAST_MINUS = 0x09,
        /// <summary>
        /// Read device unit configuration.
        /// </summary>
        PCMD_API_GET_PPM_MGL = 0x0A,
        /// <summary>
        /// Set device unit configuration.
        /// </summary>
        PCMD_API_SET_PPM_MGL = 0x0B

## Page 20:
Wrong command name: "PCMD_API_GET_PPM_MGL"
Should be more like this: "PCMD_API_SET_PPM_MGL"

## Page 21 and 22
Inconsistent result units and ranges.

The unit specified here is always ppm. However, the PoolLAB can also be changed to mg/l. Does the unit then not equal the selected setting?

### Total Chlorine (id 1) and Total Chlorine (liquid) (id 2)
* Different units for same paramter? Cl2 vs. tCl
* Result range of Total Chlorine (id 1) only up to 0.6?

### Total Hardness (id 35) and Calcium (id 23)
* Same result unit for different parameters, CaCO3 (ppm)?