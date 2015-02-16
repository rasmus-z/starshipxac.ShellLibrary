using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    /// シェルイメージサイズオプションを定義します。
    /// </summary>
    public enum ShellItemImageSizeOptions
    {
        ResizeToFit = SIIGBF.SIIGBF_RESIZETOFIT,

        BiggerSizeOk = SIIGBF.SIIGBF_BIGGERSIZEOK
    }
}