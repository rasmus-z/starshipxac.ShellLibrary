using System;

namespace starshipxac.Windows.Shell.Media.Imaging.Interop
{
    /// <summary>
    /// 標準アイコンのIDを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762542(v=vs.85).aspx
    /// <c>ShellAPI.h</c>
    /// </remarks>
    internal enum SHSTOCKICONID
    {
        /// <summary>
        /// アプリケーションに関連づけられていないファイル。
        /// </summary>
        SIID_DOCNOASSOC = 0,

        /// <summary>
        /// アプリケーションに関連づけられているファイル。
        /// </summary>
        SIID_DOCASSOC = 1,

        /// <summary>
        /// 一般的なアプリケーションのアイコン。
        /// </summary>
        SIID_APPLICATION = 2,

        /// <summary>
        /// 閉じているフォルダー。
        /// </summary>
        SIID_FOLDER = 3,

        /// <summary>
        /// 開いているフォルダー。
        /// </summary>
        SIID_FOLDEROPEN = 4,

        /// <summary>
        /// 5.25インチフロッピーディスクドライブ。
        /// </summary>
        SIID_DRIVE525 = 5,

        /// <summary>
        /// 3.5インチフロッピーディスクドライブ。
        /// </summary>
        SIID_DRIVE35 = 6,

        /// <summary>
        /// リムーバブルドライブ。
        /// </summary>
        SIID_DRIVEREMOVE = 7,

        /// <summary>
        /// 固定ドライブ。
        /// </summary>
        SIID_DRIVEFIXED = 8,

        /// <summary>
        /// ネットワークドライブ。
        /// </summary>
        SIID_DRIVENET = 9,

        /// <summary>
        /// 切断されたネットワークドライブ。
        /// </summary>
        SIID_DRIVENETDISABLED = 10,

        /// <summary>
        /// CDドライブ。
        /// </summary>
        SIID_DRIVECD = 11,

        /// <summary>
        /// RAMドライブ。
        /// </summary>
        SIID_DRIVERAM = 12,

        /// <summary>
        /// ネットワーク。
        /// </summary>
        SIID_WORLD = 13,

        /// <summary>
        /// ネットワーク上のコンピューター。
        /// </summary>
        SIID_SERVER = 15,

        /// <summary>
        /// プリンター。
        /// </summary>
        SIID_PRINTER = 16,

        /// <summary>
        /// ネットワークフォルダー。
        /// </summary>
        SIID_MYNETWORK = 17,

        /// <summary>
        /// 検索。
        /// </summary>
        SIID_FIND = 22,

        /// <summary>
        /// ヘルプ。
        /// </summary>
        SIID_HELP = 23,

        /// <summary>
        /// 共有。
        /// </summary>
        SIID_SHARE = 28,

        /// <summary>
        /// ショートカット。
        /// </summary>
        SIID_LINK = 29,

        /// <summary>
        /// Slow Item.
        /// </summary>
        SIID_SLOWFILE = 30,

        /// <summary>
        /// 空のごみ箱。
        /// </summary>
        SIID_RECYCLER = 31,

        /// <summary>
        /// ごみ箱。
        /// </summary>
        SIID_RECYCLERFULL = 32,

        /// <summary>
        /// オーディオCD。
        /// </summary>
        SIID_MEDIACDAUDIO = 40,

        /// <summary>
        /// セキュリティーロック。
        /// </summary>
        SIID_LOCK = 47,

        /// <summary>
        /// Auto List.
        /// </summary>
        SIID_AUTOLIST = 49,

        /// <summary>
        /// ネットワークプリンター。
        /// </summary>
        SIID_PRINTERNET = 50,

        /// <summary>
        /// 共有ネットワークフォルダー。
        /// </summary>
        SIID_SERVERSHARE = 51,

        /// <summary>
        /// ファックス。
        /// </summary>
        SIID_PRINTERFAX = 52,

        /// <summary>
        /// ネットワークファックス。
        /// </summary>
        SIID_PRINTERFAXNET = 53,

        /// <summary>
        /// 印刷ファイル。
        /// </summary>
        SIID_PRINTERFILE = 54,

        /// <summary>
        /// スタック。
        /// </summary>
        SIID_STACK = 55,

        /// <summary>
        /// SVCDメディア。
        /// </summary>
        SIID_MEDIASVCD = 56,

        /// <summary>
        /// 他のアイテムを含むフォルダ。
        /// </summary>
        SIID_STUFFEDFOLDER = 57,

        /// <summary>
        /// 不明なドライブ。
        /// </summary>
        SIID_DRIVEUNKNOWN = 58,

        /// <summary>
        /// DVDドライブ。
        /// </summary>
        SIID_DRIVEDVD = 59,

        /// <summary>
        /// DVDメディア。
        /// </summary>
        SIID_MEDIADVD = 60,

        /// <summary>
        /// DVD-RAMメディア。
        /// </summary>
        SIID_MEDIADVDRAM = 61,

        /// <summary>
        /// DVD-RWメディア。
        /// </summary>
        SIID_MEDIADVDRW = 62,

        /// <summary>
        /// DVD-Rメディア。
        /// </summary>
        SIID_MEDIADVDR = 63,

        /// <summary>
        /// DVD-ROMメディア。
        /// </summary>
        SIID_MEDIADVDROM = 64,

        /// <summary>
        /// CD+(Enhanced CD)メディア。
        /// </summary>
        SIID_MEDIACDAUDIOPLUS = 65,

        /// <summary>
        /// CD-RWメディア。
        /// </summary>
        SIID_MEDIACDRW = 66,

        /// <summary>
        /// CD-Rメディア。
        /// </summary>
        SIID_MEDIACDR = 67,

        /// <summary>
        /// 書き込みCDメディア。
        /// </summary>
        SIID_MEDIACDBURN = 68,

        /// <summary>
        /// ブランクCDメディア。
        /// </summary>
        SIID_MEDIABLANKCD = 69,

        /// <summary>
        /// CD-ROMメディア。
        /// </summary>
        SIID_MEDIACDROM = 70,

        /// <summary>
        /// オーディオファイル。
        /// </summary>
        SIID_AUDIOFILES = 71,

        /// <summary>
        /// 画像ファイル。
        /// </summary>
        SIID_IMAGEFILES = 72,

        /// <summary>
        /// ビデオファイル。
        /// </summary>
        SIID_VIDEOFILES = 73,

        /// <summary>
        /// 混合ファイル。
        /// </summary>
        SIID_MIXEDFILES = 74,

        /// <summary>
        /// Folder Back.
        /// </summary>
        SIID_FOLDERBACK = 75,

        /// <summary>
        /// Folder Front.
        /// </summary>
        SIID_FOLDERFRONT = 76,

        /// <summary>
        /// UACで使用する盾アイコン。
        /// </summary>
        SIID_SHIELD = 77,

        /// <summary>
        /// 警告アイコン。
        /// </summary>
        SIID_WARNING = 78,

        /// <summary>
        /// 情報アイコン。
        /// </summary>
        SIID_INFO = 79,

        /// <summary>
        /// エラーアイコン。
        /// </summary>
        SIID_ERROR = 80,

        /// <summary>
        /// 鍵アイコン。
        /// </summary>
        SIID_KEY = 81,

        /// <summary>
        /// ソフトウェアアイコン。
        /// </summary>
        SIID_SOFTWARE = 82,

        /// <summary>
        /// 名前変更アイコン。
        /// </summary>
        SIID_RENAME = 83,

        /// <summary>
        /// 削除アイコン。
        /// </summary>
        SIID_DELETE = 84,

        /// <summary>
        /// オーディオDVDメディア。
        /// </summary>
        SIID_MEDIAAUDIODVD = 85,

        /// <summary>
        /// 映像DVDメディア。
        /// </summary>
        SIID_MEDIAMOVIEDVD = 86,

        /// <summary>
        /// エンハンスドCDメディア。
        /// </summary>
        SIID_MEDIAENHANCEDCD = 87,

        /// <summary>
        /// エンハンスドDVDメディア。
        /// </summary>
        SIID_MEDIAENHANCEDDVD = 88,

        /// <summary>
        /// HD-DVDメディア。
        /// </summary>
        SIID_MEDIAHDDVD = 89,

        /// <summary>
        /// BluRayメディア。
        /// </summary>
        SIID_MEDIABLURAY = 90,

        /// <summary>
        /// VCDメディア。
        /// </summary>
        SIID_MEDIAVCD = 91,

        /// <summary>
        /// DVD+Rメディア。
        /// </summary>
        SIID_MEDIADVDPLUSR = 92,

        /// <summary>
        /// DVD+RWメディア。
        /// </summary>
        SIID_MEDIADVDPLUSRW = 93,

        /// <summary>
        /// デスクトップコンピューター。
        /// </summary>
        SIID_DESKTOPPC = 94,

        /// <summary>
        /// モバイルコンピューター。
        /// </summary>
        SIID_MOBILEPC = 95,

        /// <summary>
        /// ユーザーアカウントコントロールパネル。
        /// </summary>
        SIID_USERS = 96,

        /// <summary>
        /// スマートメディア。
        /// </summary>
        SIID_MEDIASMARTMEDIA = 97,

        /// <summary>
        /// コンパクトフラッシュメディア。
        /// </summary>
        SIID_MEDIACOMPACTFLASH = 98,

        /// <summary>
        /// 携帯電話。
        /// </summary>
        SIID_DEVICECELLPHONE = 99,

        /// <summary>
        /// カメラ。
        /// </summary>
        SIID_DEVICECAMERA = 100,

        /// <summary>
        /// ビデオカメラ。
        /// </summary>
        SIID_DEVICEVIDEOCAMERA = 101,

        /// <summary>
        /// オーディオプレーヤー。
        /// </summary>
        SIID_DEVICEAUDIOPLAYER = 102,

        /// <summary>
        /// ネットワーク接続。
        /// </summary>
        SIID_NETWORKCONNECT = 103,

        /// <summary>
        /// ネットワークとインターネットコントロールパネル。
        /// </summary>
        SIID_INTERNET = 104,

        /// <summary>
        /// ZIPファイル。
        /// </summary>
        SIID_ZIPFILE = 105,

        /// <summary>
        /// 設定。
        /// </summary>
        SIID_SETTINGS = 106,

        /// <summary>
        /// HDDVDドライブ。
        /// </summary>
        SIID_DRIVEHDDVD = 132,

        /// <summary>
        /// BluRayドライブ。
        /// </summary>
        SIID_DRIVEBD = 133,

        /// <summary>
        /// HDDVD-ROMメディア。
        /// </summary>
        SIID_MEDIAHDDVDROM = 134,

        /// <summary>
        /// HDDVD-Rメディア。
        /// </summary>
        SIID_MEDIAHDDVDR = 135,

        /// <summary>
        /// HDDVD-RAMメディア。
        /// </summary>
        SIID_MEDIAHDDVDRAM = 136,

        /// <summary>
        /// BluRay ROMメディア。
        /// </summary>
        SIID_MEDIABDROM = 137,

        /// <summary>
        /// BluRay Rメディア。
        /// </summary>
        SIID_MEDIABDR = 138,

        /// <summary>
        /// BluRay REメディア。
        /// </summary>
        SIID_MEDIABDRE = 139,

        /// <summary>
        /// クラスター化されたディスク。
        /// </summary>
        SIID_CLUSTEREDDRIVE = 140,

        /// <summary>
        /// 列挙値の最大値。
        /// </summary>
        SIID_MAX_ICONS = 174,
    }
}