namespace DaprTool.BuildingBlocks.Utils;

#region 校验相关助手类

/// <summary>
///     校验相关助手类
/// </summary>
public class CheckSumHelper
{
    #region CRC16_CCITT校验

    /// <summary>
    ///     CRC16_CCITT校验,多项式x16+x12+x5+1（0x1021），初始值0x0000，低位在前，高位在后，结果与0x0000异或
    ///     0x8408是0x1021按位颠倒后的结果。
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_CCITT(byte[] buffer)
    {
        var wCRCin = 0x0000;
        var wCPoly = 0x8408;
        foreach (var item in buffer)
        {
            wCRCin ^= item & 0x00ff;
            for (var j = 0; j < 8; j++)
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= wCPoly;
                }
                else
                {
                    wCRCin >>= 1;
                }
        }

        return wCRCin;
    }

    #endregion

    #region CRC16/CCITT-FALSE校验

    /// <summary>
    ///     CRC16/CCITT-FALSE校验,多项式x16+x12+x5+1（0x1021），初始值0xFFFF，低位在后，高位在前，结果与0x0000异或
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_CCITT_FALSE(byte[] buffer)
    {
        var wCRCin = 0xffff;
        var wCPoly = 0x1021;
        foreach (var item in buffer)
            for (var i = 0; i < 8; i++)
            {
                var bit = ((item >> (7 - i)) & 1) == 1;
                var c15 = ((wCRCin >> 15) & 1) == 1;
                wCRCin <<= 1;
                if (c15 ^ bit)
                    wCRCin ^= wCPoly;
            }

        wCRCin &= 0xffff;
        return wCRCin ^= 0x0000;
    }

    #endregion

    #region CRC16/XMODEM校验

    /// <summary>
    ///     CRC16/XMODEM校验,多项式x16+x12+x5+1（0x1021），初始值0x0000，低位在后，高位在前，结果与0x0000异或
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_XMODEM(byte[] buffer)
    {
        var wCRCin = 0x0000; // initial value 65535
        var wCPoly = 0x1021; // 0001 0000 0010 0001 (0, 5, 12)
        foreach (var item in buffer)
            for (var i = 0; i < 8; i++)
            {
                var bit = ((item >> (7 - i)) & 1) == 1;
                var c15 = ((wCRCin >> 15) & 1) == 1;
                wCRCin <<= 1;
                if (c15 ^ bit)
                    wCRCin ^= wCPoly;
            }

        wCRCin &= 0xffff;
        return wCRCin ^= 0x0000;
    }

    #endregion

    #region CRC16/X25校验

    /// <summary>
    ///     CRC16/X25校验,多项式x16+x12+x5+1（0x1021），初始值0xffff，低位在前，高位在后，结果与0xFFFF异或,0x8408是0x1021按位颠倒后的结果。
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_X25(byte[] buffer)
    {
        var wCRCin = 0xffff;
        var wCPoly = 0x8408;
        foreach (var item in buffer)
        {
            wCRCin ^= item & 0x00ff;
            for (var j = 0; j < 8; j++)
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= wCPoly;
                }
                else
                {
                    wCRCin >>= 1;
                }
        }

        return wCRCin ^= 0xffff;
    }

    #endregion

    #region CRC16/MODBUS校验

    /// <summary>
    ///     CRC16/MODBUS校验,多项式x16+x15+x2+1（0x8005），初始值0xFFFF，低位在前，高位在后，结果与0x0000异或，0xA001是0x8005按位颠倒后的结果
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_MODBUS(byte[] buffer)
    {
        var wCRCin = 0xffff;
        var POLYNOMIAL = 0xa001;
        foreach (var item in buffer)
        {
            wCRCin ^= item & 0x00ff;
            for (var j = 0; j < 8; j++)
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= POLYNOMIAL;
                }
                else
                {
                    wCRCin >>= 1;
                }
        }

        return wCRCin ^= 0x0000;
    }

    #endregion

    #region CRC16/IBM校验

    /// <summary>
    ///     CRC16/IBM校验,多项式x16+x15+x2+1（0x8005），初始值0x0000，低位在前，高位在后，结果与0x0000异或，0xA001是0x8005按位颠倒后的结果
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_IBM(byte[] buffer)
    {
        var wCRCin = 0x0000;
        var wCPoly = 0xa001;
        foreach (var item in buffer)
        {
            wCRCin ^= item & 0x00ff;
            for (var j = 0; j < 8; j++)
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= wCPoly;
                }
                else
                {
                    wCRCin >>= 1;
                }
        }

        return wCRCin ^= 0x0000;
    }

    #endregion

    #region CRC16/MAXIM校验

    /// <summary>
    ///     CRC16/MAXIM校验,多项式x16+x15+x2+1（0x8005），初始值0x0000，低位在前，高位在后，结果与0xFFFF异或,0xA001是0x8005按位颠倒后的结果
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_MAXIM(byte[] buffer)
    {
        var wCRCin = 0x0000;
        var wCPoly = 0xa001;
        foreach (var item in buffer)
        {
            wCRCin ^= item & 0x00ff;
            for (var j = 0; j < 8; j++)
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= wCPoly;
                }
                else
                {
                    wCRCin >>= 1;
                }
        }

        return wCRCin ^= 0xffff;
    }

    #endregion

    #region CRC16/USB校验

    /// <summary>
    ///     CRC16/USB校验,多项式x16+x15+x2+1（0x8005），初始值0xFFFF，低位在前，高位在后，结果与0xFFFF异或，0xA001是0x8005按位颠倒后的结果
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_USB(byte[] buffer)
    {
        var wCRCin = 0xFFFF;
        var wCPoly = 0xa001;
        foreach (var item in buffer)
        {
            wCRCin ^= item & 0x00ff;
            for (var j = 0; j < 8; j++)
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= wCPoly;
                }
                else
                {
                    wCRCin >>= 1;
                }
        }

        return wCRCin ^= 0xffff;
    }

    #endregion

    #region CRC16/DNP校验

    /// <summary>
    ///     CRC16/DNP校验,多项式x16+x13+x12+x11+x10+x8+x6+x5+x2+1（0x3D65），初始值0x0000，低位在前，高位在后，结果与0xFFFF异或,0xA6BC是0x3D65按位颠倒后的结果
    /// </summary>
    /// <param name="buffer">校验的数组</param>
    /// <returns></returns>
    public static int Crc16_DNP(byte[] buffer)
    {
        var wCRCin = 0x0000;
        var wCPoly = 0xA6BC;
        foreach (var item in buffer)
        {
            wCRCin ^= item & 0x00ff;
            for (var j = 0; j < 8; j++)
                if ((wCRCin & 0x0001) != 0)
                {
                    wCRCin >>= 1;
                    wCRCin ^= wCPoly;
                }
                else
                {
                    wCRCin >>= 1;
                }
        }

        return wCRCin ^= 0xffff;
    }

    #endregion
}

#endregion