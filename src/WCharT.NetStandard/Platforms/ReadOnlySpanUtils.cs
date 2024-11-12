using System;
using System.Text;

namespace WCharT.Platforms
{

    public abstract class ReadOnlySpanUtils
    {
        private static ReadOnlySpanUtils _instance;

        public abstract ReadOnlySpan<byte> CreateData(int chars);
        public abstract ReadOnlySpan<byte> CreateData(string str);
        public abstract unsafe ReadOnlySpan<byte> CreateData(byte* p);
        public abstract string GetString(ReadOnlySpan<byte> data);

        public static ReadOnlySpanUtils Instance
        {
            get
            {
                if (_instance == null)
                {
                    if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                        _instance = new ReadOnlySpanUtilsWindows();
                    else if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
                        _instance = new ReadOnlySpanUtilsUnix();
                    else
                        throw new NotSupportedException("Platform not supported");
                }

                return _instance;
            }
        }
    }
}