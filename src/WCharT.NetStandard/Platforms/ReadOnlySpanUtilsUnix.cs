using System;
using System.Text;

namespace WCharT.Platforms
{

    public class ReadOnlySpanUtilsUnix:ReadOnlySpanUtils
    {
        public override ReadOnlySpan<byte> CreateData(int chars)
        {
            var length = chars * sizeof(uint);
            var data = new byte[length];
            return new ReadOnlySpan<byte>(data, 0, length);
        }

        public override ReadOnlySpan<byte> CreateData(string str)
        {
            var src = Encoding.UTF32.GetBytes(str);
            var dest = new byte[src.Length + sizeof(uint)];
            Array.Copy(src, dest, src.Length);

            //Remove null terminator from span length as the span does
            //contain this information itself. It is only there for
            //null terminated interop scenarios.
            return new ReadOnlySpan<byte>(dest, 0, src.Length);
        }

        public override unsafe ReadOnlySpan<byte> CreateData(byte* p)
        {
            return (IntPtr) p == IntPtr.Zero
                ? ReadOnlySpan<byte>.Empty
                : new ReadOnlySpan<byte>(p, GetLength(p));
        }

        public override string GetString(ReadOnlySpan<byte> data)
        {
            return Encoding.UTF32.GetString(data.ToArray());
        }
        
        private unsafe int GetLength(byte* ptr)
        {
            //check code to throw exception in case of arithmethic overflow
            checked
            {
                var current = (uint*) ptr;
                while (*current != 0)
                {
                    current += 1; //Jump to next UTF32 char
                }

                return (int) ((byte*) current - ptr);
            }
        }
    }
}