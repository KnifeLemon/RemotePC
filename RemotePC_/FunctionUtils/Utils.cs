using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RemotePC_.FunctionUtils
{
    public static class Utils
    {
        private static Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF); //랜덤 시드값

        /// <summary>
        /// 사용자의 아이디를 받아옵니다. 이 값은 고정입니다.
        /// </summary>
        /// <returns></returns>
        public static string GetUserID()
        {
            // 1. 외부아이피를 받아온다.
            // 2. IP를 INET_ATON 로 변환한다.
            // 3. 문자를 0000-0000-00 형식으로 만들어준다.
            string result = "";
            try
            {
                IPAddress ipaddress = GetAdapterWithInternetAccess();
                //IP를 INET_ATON 로 변환
                uint i = IPtoINET_ATON(ipaddress);

                result = string.Format("{0:####-####-##}", i);
            }
            catch { }

            return result;
        }

        /// <summary>
        /// IP를 INET_ATON 형식으로 변환
        /// </summary>
        public static uint IPtoINET_ATON(IPAddress ipAddress)
        {
            string ipstr = ipAddress.ToString();
            return ipstr.Split('.').Select(uint.Parse).Aggregate((a, b) => a * 256 + b);
        }

        /// <summary>
        /// INET_ATON 로 변경된 IP를 복원
        /// </summary>
        /// <param name="atonStr"></param>
        /// <returns></returns>
        public static IPAddress INET_ATONtoIP(uint atonStr)
        {
            long netorder_ip = IPAddress.HostToNetworkOrder(Convert.ToInt32(atonStr));
            return new IPAddress(netorder_ip);
        }

        /// <summary>
        /// BZip2 압축
        /// </summary>
        /// <param name="InData"></param>
        /// <returns></returns>
        public static byte[] CompressBZip2(byte[] InData)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bzip2.BZip2OutputStream input = new Bzip2.BZip2OutputStream(ms, false))
                {
                    input.Write(InData, 0, InData.Length);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// BZip2 압축해제
        /// </summary>
        /// <param name="InData"></param>
        /// <returns></returns>
        public static byte[] DecompressBZip2(byte[] InData)
        {
            using (MemoryStream ms = new MemoryStream(InData))
            {
                using (Bzip2.BZip2InputStream input = new Bzip2.BZip2InputStream(ms, false))
                {
                    using (MemoryStream decompressedData = new MemoryStream())
                    {
                        input.CopyTo(decompressedData);
                        return decompressedData.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// 이미지를 byte 배열로 변환
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
             MemoryStream ms = new MemoryStream();
             imageIn.Save(ms, ImageFormat.Jpeg);
             return  ms.ToArray();
        }

        /// <summary>
        /// byte 배열을 이미지로 변환
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] bytes)
        {
             MemoryStream ms = new MemoryStream(bytes);
             Image recImg = Image.FromStream(ms);
             return recImg ;
        }

        /// <summary>
        /// 사용자의 비밀번호를 받아옵니다.
        /// </summary>
        /// <returns></returns>
        public static string GetUserPW()
        {
            return RandomString(random.Next(8, 10));
        }

        /// <summary>
        /// 자리수만큼 랜덤 문자열 받아오기
        /// </summary>
        /// <param name="_nLength"></param>
        /// <returns></returns>
        private static string RandomString(int _nLength)
        {
            const string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var chars = Enumerable.Range(0, _nLength).Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }

        public static IPAddress GetAdapterWithInternetAccess()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_IP4RouteTable WHERE Destination=\"0.0.0.0\"");
            int interfaceIndex = -1;

            foreach (var item in searcher.Get())
                interfaceIndex = Convert.ToInt32(item["InterfaceIndex"]);

            searcher = new ManagementObjectSearcher("root\\CIMV2",
                string.Format("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE InterfaceIndex={0}", interfaceIndex));

            foreach (var item in searcher.Get())
            {
                string[] IPAddresses = (string[])item["IPAddress"];

                foreach (string IP in IPAddresses)
                    return IPAddress.Parse(IP);
            }

            return null;
        }

        public static Stream ToStream(this Image image, ImageCodecInfo enc = null, System.Drawing.Imaging.EncoderParameters pram = null)
        {
            var stream = new System.IO.MemoryStream();
            if (enc == null)
                image.Save(stream, ImageFormat.Jpeg);
            else
                image.Save(stream, enc, pram);
            stream.Position = 0;
            return stream;
        }
    }
}
