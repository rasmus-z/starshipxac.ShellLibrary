using System;
using Xunit;

namespace starshipxac.Shell.PropertySystem.Interop
{
    public class PropVariantTest
    {
        [Fact]
        public void FromStringTest()
        {
            var prop = PropVariant.FromString("abcdefg");
            try
            {
                var actual = prop.GetStringValue();
                actual.IsNotNull();
                actual.Is("abcdefg");
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromStringArrayTest()
        {
            var prop = PropVariant.FromStringArray(new[] { "abcd", "efg", "hijk", "lmn", "opqr", "stu", "vw", "xyz" });
            try
            {
                var actual = prop.GetStringArrayValue();
                actual.IsNotNull();
                actual.Is("abcd", "efg", "hijk", "lmn", "opqr", "stu", "vw", "xyz");
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromStringAsTest()
        {
            var prop = PropVariant.FromStringAs("abcd;efg;hijk;lmn;opqr;stu;vw;xyz");
            try
            {
                var actual = prop.GetStringArrayValue();
                actual.IsNotNull();
                actual.Is("abcd", "efg", "hijk", "lmn", "opqr", "stu", "vw", "xyz");
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromInt8Test()
        {
            var prop = PropVariant.FromInt8(123);
            try
            {
                var actual = prop.GetInt8Value();
                actual.Is<sbyte>(123);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromUInt8Test()
        {
            var prop = PropVariant.FromUInt8(123);
            try
            {
                var actual = prop.GetUInt8Value();
                actual.Is<byte>(123);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromInt16Test()
        {
            var prop = PropVariant.FromInt16(-1234);
            try
            {
                var actual = prop.GetInt16Value();
                actual.Is<Int16>(-1234);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromInt16ArrayTest()
        {
            var prop = PropVariant.FromInt16Array(new Int16[] { -1, -2, -3, -4, -5 });
            try
            {
                var actual = prop.GetInt16ArrayValue();
                actual.Is<Int16>(-1, -2, -3, -4, -5);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromUInt16Test()
        {
            var prop = PropVariant.FromUInt16(1234);
            try
            {
                var actual = prop.GetUInt16Value();
                actual.Is<UInt16>(1234);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromUInt16ArrayTest()
        {
            var prop = PropVariant.FromUInt16Array(new UInt16[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            try
            {
                var actual = prop.GetUInt16ArrayValue();
                actual.IsNotNull();
                actual.Is<UInt16>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromInt32Test()
        {
            var prop = PropVariant.FromInt32(1234);
            try
            {
                var actual = prop.GetInt32Value();
                actual.Is(1234);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromInt32ArrayTest()
        {
            var prop = PropVariant.FromInt32Array(new[] { -1234, -123, -12, -1, 0, 1, 12, 123, 1234 });
            try
            {
                var actual = prop.GetInt32ArrayValue();
                actual.IsNotNull();
                actual.Is(-1234, -123, -12, -1, 0, 1, 12, 123, 1234);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromUInt32Test()
        {
            var prop = PropVariant.FromUInt32(12345);
            try
            {
                var actual = prop.GetUInt32Value();
                actual.Is<UInt32>(12345);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromUInt32ArrayTest()
        {
            var prop = PropVariant.FromUInt32Array(new UInt32[] { 1, 10, 100, 1000, 10000 });
            try
            {
                var actual = prop.GetUInt32ArrayValue();
                actual.Is<UInt32>(1, 10, 100, 1000, 10000);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromInt64Test()
        {
            var prop = PropVariant.FromInt64(12345678);
            try
            {
                var actual = prop.GetInt64Value();
                actual.Is(12345678);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromInt64ArrayTest()
        {
            var prop = PropVariant.FromInt64Array(new Int64[] { 1234, 12345, 123456, 1234567, 12345678 });
            try
            {
                var actual = prop.GetInt64ArrayValue();
                actual.Is(1234, 12345, 123456, 1234567, 12345678);
            }
            finally
            {
                prop.Clear();
            }
        }

        [Fact]
        public void FromDateTimeTest()
        {
            var prop = PropVariant.FromDateTime(new DateTime(2017, 2, 27, 10, 10, 10));
            try
            {
                var actual = prop.GetDateTimeValue();
                actual.Is(new DateTime(2017, 2, 27, 10, 10, 10));
            }
            finally
            {
                prop.Clear();
            }
        }
    }
}