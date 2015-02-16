using System;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    /// シェルイメージサイズを保持します。
    /// </summary>
    [Serializable]
    public struct ShellImageSize
    {
        private static readonly ShellImageSize empty = new ShellImageSize(0.0, 0.0);

        public ShellImageSize(double width, double height)
            : this()
        {
            this.Width = width;
            this.Height = height;
        }

        public static ShellImageSize Empty
        {
            get
            {
                return empty;
            }
        }

        /// <summary>
        /// イメージの幅を取得します。
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// イメージの高さを取得します。
        /// </summary>
        public double Height { get; set; }

        public bool IsEmpty
        {
            get
            {
                return this == Empty;
            }
        }

        public static bool operator ==(ShellImageSize x, ShellImageSize y)
        {
            return (x.Width == y.Width) && (x.Height == y.Height);
        }

        public static bool operator !=(ShellImageSize x, ShellImageSize y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is ShellImageSize)
                {
                    return Equals(this, (ShellImageSize)obj);
                }
            }
            return false;
        }

        public bool Equals(ShellImageSize obj)
        {
            return Equals(this, obj);
        }

        public static bool Equals(ShellImageSize x, ShellImageSize y)
        {
            return x == y;
        }

        public override int GetHashCode()
        {
            return this.Width.GetHashCode() ^ this.Height.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}", this.Width, this.Height);
        }
    }
}