using System;
using System.ComponentModel;
using WCharT.Platforms;

namespace WCharT
{
    /// <summary>
    /// A readonly data structure to send or receive wchar_t data to / from native code.
    /// </summary>
    public readonly ref struct WCharTString
    {
        private readonly ReadOnlySpan<byte> data;

        /// <summary>
        /// Creates an instance from a given pointer to be able to read the data later.
        /// </summary>
        /// <param name="p">Pointer to a null terminated wchar_t string.</param>
        public unsafe WCharTString(byte* p)
        {
            data = ReadOnlySpanUtils.Instance.CreateData(p);
        }

        /// <summary>
        /// Creates an instance from a given string to be able to pass on the data later.
        /// </summary>
        /// <param name="str">A string which should be encoded into a wchar_t string.</param>
        /// <remarks>The internal representation of the string is null terminated. Meaning it is possible to simply pass the pointer of the <see cref="ReadOnlySpan{T}"/> to native code without any length information. At the same time the length parameter of the <see cref="ReadOnlySpan{T}"/> does not include the null termination character. Meaning if an API requires the size of the data in bytes without the null termination character the length value of the <see cref="ReadOnlySpan{T}"/>can be used.</remarks>
        public WCharTString(string str)
        {
            data = ReadOnlySpanUtils.Instance.CreateData(str);
        }

        /// <summary>
        /// Creates an instance for a given amount of wchar_t characters.
        /// </summary>
        /// <param name="length">The number of wchar_t characters which should be able to put into the instance.</param>
        /// <remarks>This does not automatically add a character in case a null termination character is needed.</remarks>
        public WCharTString(int length)
        {
            data = ReadOnlySpanUtils.Instance.CreateData(length);
        }

        /// <summary>
        /// Reads the current data and returns the contained string.
        /// </summary>
        /// <returns>The decoded wchar_t string from the data.</returns>
        /// <remarks>The string has the size of the internal data block which can include null termination characters. If not needed those can be trimmed.</remarks>
        public string GetString()
        {
            return ReadOnlySpanUtils.Instance.GetString(data);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ref readonly byte GetPinnableReference() => ref data.GetPinnableReference();

        public static implicit operator ReadOnlySpan<byte>(WCharTString obj)
        {
            return obj.data;
        }
    }
}