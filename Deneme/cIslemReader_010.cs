using ADRcpLib;
using ADSioLib;
using log4net;
using System;
using System.Linq;

namespace Deneme
{
    public class cIslemReader_0010
    {
        static ILog _LogDosyasi = LogManager.GetLogger(typeof(cIslemReader_0010));

        public static SioBase SioBase = new SioNet();
        public static RcpBase RcpBase = new RcpBase();

        string _ReaderIp = "10.2.41.10";

        internal void fn_BaslaINSERT(object state)
        {
            #region Değişkenler

            #endregion

            try
            {


                SioBase.onStatus += SioBase_onStatus;
                SioBase.onReceived += SioBase_onReceived;

                RcpBase.RxRspParsed += RcpBase_RxRspParsed;
                RcpBase.TxRspParsed += RcpBase_TxRspParsed;

                SioBase.Connect(_ReaderIp, Convert.ToInt32("49152"));
            }
            catch (Exception ex)
            {
                _LogDosyasi.Error(ex.ToString());
            }
        }

        private void RcpBase_TxRspParsed(object sender, ProtocolEventArgs e)
        {
            
        }


        private void RcpBase_RxRspParsed(object sender, ProtocolEventArgs e)
        {
            try
            {
                string _Gelen = TagInfo.ByteArrayToHexString(e.Data);

                int _Sayi = _Gelen.IndexOf("A41");

                string _Epc = _Gelen.Substring(_Sayi,24);

                _LogDosyasi.Error(_ReaderIp + "= " + _Epc);
            }
            catch (Exception ex)
            {
                _LogDosyasi.Error(ex.ToString());
            }

            //_LogDosyasi.Error(TagInfo.ByteArrayToHexString(e.Data));
        }

        private void SioBase_onStatus(object sender, StatusEventArgs e)
        {
            try
            {
                switch ((StatusType)e.Status)
                {
                    case StatusType.CONNECT_OK:
                        try
                        {
                            int intVer = Convert.ToInt32(e.Msg);
                        }
                        catch { }
                       
                        break;
                    case StatusType.CONNECT_FAIL:
                        
                        break;
                    case StatusType.DISCONNECT_OK:
                       
                        break;
                    case StatusType.DISCONNECT_EXCEPT:
                       
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                _LogDosyasi.Error(ex.ToString());
            }
        }

        private void SioBase_onReceived(object sender, ReceivedEventArgs e)
        {
           // _LogDosyasi.Info("SioBase_onReceived");

            RcpBase.ReciveBytePkt(e.Data);
        }
    }
}
