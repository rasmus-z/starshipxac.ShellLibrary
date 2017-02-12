using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Media.Imaging.Interop
{
    /// <summary>
    ///     Define stock icon ID.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762542(v=vs.85).aspx
    ///     <c>ShellAPI.h</c>
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum SHSTOCKICONID
    {
        /// <summary>
        ///     <para>
        ///         Generic application with no custom icon.
        ///     </para>
        ///     <para>
        ///         アプリケーションに関連づけられていないファイル。
        ///     </para>
        /// </summary>
        SIID_DOCNOASSOC = 0,

        /// <summary>
        ///     <para>
        ///         Document of a type with an associated application.
        ///     </para>
        ///     <para>
        ///         アプリケーションに関連づけられているファイル。
        ///     </para>
        /// </summary>
        SIID_DOCASSOC = 1,

        /// <summary>
        ///     <para>
        ///         Generic application with no custom icon.
        ///     </para>
        ///     <para>
        ///         一般的なアプリケーションのアイコン。
        ///     </para>
        /// </summary>
        SIID_APPLICATION = 2,

        /// <summary>
        ///     <para>
        ///         Folder (generic, unspecified state).
        ///     </para>
        ///     <para>
        ///         フォルダー。
        ///     </para>
        /// </summary>
        SIID_FOLDER = 3,

        /// <summary>
        ///     <para>
        ///         Folder (open).
        ///     </para>
        ///     <para>
        ///         開いているフォルダー。
        ///     </para>
        /// </summary>
        SIID_FOLDEROPEN = 4,

        /// <summary>
        ///     <para>
        ///         5.25-inch disk drive.
        ///     </para>
        ///     <para>
        ///         5.25インチフロッピーディスクドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVE525 = 5,

        /// <summary>
        ///     <para>
        ///         3.5-inch disk drive.
        ///     </para>
        ///     <para>
        ///         3.5インチフロッピーディスクドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVE35 = 6,

        /// <summary>
        ///     <para>
        ///         Removable drive.
        ///     </para>
        ///     <para>
        ///         リムーバブルドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVEREMOVE = 7,

        /// <summary>
        ///     <para>
        ///         Fixed drive (hard disk).
        ///     </para>
        ///     <para>
        ///         固定ドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVEFIXED = 8,

        /// <summary>
        ///     <para>
        ///         Network drive (connected).
        ///     </para>
        ///     <para>
        ///         ネットワークドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVENET = 9,

        /// <summary>
        ///     <para>
        ///         Network drive (disconnected).
        ///     </para>
        ///     <para>
        ///         切断されたネットワークドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVENETDISABLED = 10,

        /// <summary>
        ///     <para>
        ///         CD drive.
        ///     </para>
        ///     <para>
        ///         CDドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVECD = 11,

        /// <summary>
        ///     <para>
        ///         RAM disk drive.
        ///     </para>
        ///     <para>
        ///         RAMドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVERAM = 12,

        /// <summary>
        ///     <para>
        ///         The entire network.
        ///     </para>
        ///     <para>
        ///         ネットワーク。
        ///     </para>
        /// </summary>
        SIID_WORLD = 13,

        /// <summary>
        ///     <para>
        ///         A computer on the network.
        ///     </para>
        ///     <para>
        ///         ネットワーク上のコンピューター。
        ///     </para>
        /// </summary>
        SIID_SERVER = 15,

        /// <summary>
        ///     <para>
        ///         A local printer or print destination.
        ///     </para>
        ///     <para>
        ///         プリンター。
        ///     </para>
        /// </summary>
        SIID_PRINTER = 16,

        /// <summary>
        ///     <para>
        ///         The Network virtual folder (FOLDERID_NetworkFolder/CSIDL_NETWORK).
        ///     </para>
        ///     <para>
        ///         ネットワークフォルダー。
        ///     </para>
        /// </summary>
        SIID_MYNETWORK = 17,

        /// <summary>
        ///     <para>
        ///         The Search feature.
        ///     </para>
        ///     <para>
        ///         検索。
        ///     </para>
        /// </summary>
        SIID_FIND = 22,

        /// <summary>
        ///     <para>
        ///         The Help and Support feature.
        ///     </para>
        ///     <para>
        ///         ヘルプ。
        ///     </para>
        /// </summary>
        SIID_HELP = 23,

        /// <summary>
        ///     <para>
        ///         Overlay for a shared item.
        ///     </para>
        ///     <para>
        ///         共有。
        ///     </para>
        /// </summary>
        SIID_SHARE = 28,

        /// <summary>
        ///     <para>
        ///         Overlay for a shortcut.
        ///     </para>
        ///     <para>
        ///         ショートカット。
        ///     </para>
        /// </summary>
        SIID_LINK = 29,

        /// <summary>
        ///     <para>
        ///         Overlay for items that are expected to be slow to access.
        ///     </para>
        /// </summary>
        SIID_SLOWFILE = 30,

        /// <summary>
        ///     <para>
        ///         The Recycle Bin (empty).
        ///     </para>
        ///     <para>
        ///         空のごみ箱。
        ///     </para>
        /// </summary>
        SIID_RECYCLER = 31,

        /// <summary>
        ///     <para>
        ///         The Recycle Bin (not empty).
        ///     </para>
        ///     <para>
        ///         ごみ箱。
        ///     </para>
        /// </summary>
        SIID_RECYCLERFULL = 32,

        /// <summary>
        ///     <para>
        ///         Audio CD media.
        ///     </para>
        ///     <para>
        ///         オーディオCD。
        ///     </para>
        /// </summary>
        SIID_MEDIACDAUDIO = 40,

        /// <summary>
        ///     <para>
        ///         Security lock.
        ///     </para>
        ///     <para>
        ///         セキュリティーロック。
        ///     </para>
        /// </summary>
        SIID_LOCK = 47,

        /// <summary>
        ///     <para>
        ///         A virtual folder that contains the results of a search.
        ///     </para>
        ///     <para>
        ///         Auto List.
        ///     </para>
        /// </summary>
        SIID_AUTOLIST = 49,

        /// <summary>
        ///     <para>
        ///         A network printer.
        ///     </para>
        ///     <para>
        ///         ネットワークプリンター。
        ///     </para>
        /// </summary>
        SIID_PRINTERNET = 50,

        /// <summary>
        ///     <para>
        ///         A server shared on a network.
        ///     </para>
        ///     <para>
        ///         共有ネットワークフォルダー。
        ///     </para>
        /// </summary>
        SIID_SERVERSHARE = 51,

        /// <summary>
        ///     <para>
        ///         A local fax printer.
        ///     </para>
        ///     <para>
        ///         ファックス。
        ///     </para>
        /// </summary>
        SIID_PRINTERFAX = 52,

        /// <summary>
        ///     <para>
        ///         A network fax printer.
        ///     </para>
        ///     <para>
        ///         ネットワークファックス。
        ///     </para>
        /// </summary>
        SIID_PRINTERFAXNET = 53,

        /// <summary>
        ///     <para>
        ///         A file that receives the output of a Print to file operation.
        ///     </para>
        ///     <para>
        ///         印刷ファイル。
        ///     </para>
        /// </summary>
        SIID_PRINTERFILE = 54,

        /// <summary>
        ///     <para>
        ///         A category that results from a Stack by command to organize the contents of a folder.
        ///     </para>
        ///     <para>
        ///         スタック。
        ///     </para>
        /// </summary>
        SIID_STACK = 55,

        /// <summary>
        ///     <para>
        ///         Super Video CD (SVCD) media.
        ///     </para>
        ///     <para>
        ///         SVCDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIASVCD = 56,

        /// <summary>
        ///     <para>
        ///         A folder that contains only subfolders as child items.
        ///     </para>
        ///     <para>
        ///         他のアイテムを含むフォルダ。
        ///     </para>
        /// </summary>
        SIID_STUFFEDFOLDER = 57,

        /// <summary>
        ///     <para>
        ///         Unknown drive type.
        ///     </para>
        ///     <para>
        ///         不明なドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVEUNKNOWN = 58,

        /// <summary>
        ///     <para>
        ///         DVD drive.
        ///     </para>
        ///     <para>
        ///         DVDドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVEDVD = 59,

        /// <summary>
        ///     <para>
        ///         DVD media.
        ///     </para>
        ///     <para>
        ///         DVDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIADVD = 60,

        /// <summary>
        ///     <para>
        ///         DVD-RAM media.
        ///     </para>
        ///     <para>
        ///         DVD-RAMメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIADVDRAM = 61,

        /// <summary>
        ///     <para>
        ///         DVD-RW media.
        ///     </para>
        ///     <para>
        ///         DVD-RWメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIADVDRW = 62,

        /// <summary>
        ///     <para>
        ///         DVD-R media.
        ///     </para>
        ///     <para>
        ///         DVD-Rメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIADVDR = 63,

        /// <summary>
        ///     <para>
        ///         DVD-ROM media.
        ///     </para>
        ///     <para>
        ///         DVD-ROMメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIADVDROM = 64,

        /// <summary>
        ///     <para>
        ///         CD+ (enhanced audio CD) media.
        ///     </para>
        ///     <para>
        ///         CD+(Enhanced CD)メディア。
        ///     </para>
        /// </summary>
        SIID_MEDIACDAUDIOPLUS = 65,

        /// <summary>
        ///     <para>
        ///         CD-RW media.
        ///     </para>
        ///     <para>
        ///         CD-RWメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIACDRW = 66,

        /// <summary>
        ///     <para>
        ///         CD-R media.
        ///     </para>
        ///     <para>
        ///         CD-Rメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIACDR = 67,

        /// <summary>
        ///     <para>
        ///         A writeable CD in the process of being burned.
        ///     </para>
        ///     <para>
        ///         書き込みCDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIACDBURN = 68,

        /// <summary>
        ///     <para>
        ///         Blank writable CD media.
        ///     </para>
        ///     <para>
        ///         ブランクCDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIABLANKCD = 69,

        /// <summary>
        ///     <para>
        ///         CD-ROM media.
        ///     </para>
        ///     <para>
        ///         CD-ROMメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIACDROM = 70,

        /// <summary>
        ///     <para>
        ///         An audio file.
        ///     </para>
        ///     <para>
        ///         オーディオファイル。
        ///     </para>
        /// </summary>
        SIID_AUDIOFILES = 71,

        /// <summary>
        ///     <para>
        ///         An image file.
        ///     </para>
        ///     <para>
        ///         画像ファイル。
        ///     </para>
        /// </summary>
        SIID_IMAGEFILES = 72,

        /// <summary>
        ///     <para>
        ///         A video file.
        ///     </para>
        ///     <para>
        ///         ビデオファイル。
        ///     </para>
        /// </summary>
        SIID_VIDEOFILES = 73,

        /// <summary>
        ///     <para>
        ///         A mixed file.
        ///     </para>
        ///     <para>
        ///         混合ファイル。
        ///     </para>
        /// </summary>
        SIID_MIXEDFILES = 74,

        /// <summary>
        ///     <para>
        ///         Folder back.
        ///     </para>
        ///     <para>
        ///         Folder Back.
        ///     </para>
        /// </summary>
        SIID_FOLDERBACK = 75,

        /// <summary>
        ///     <para>
        ///         Folder front.
        ///     </para>
        ///     <para>
        ///         Folder Front.
        ///     </para>
        /// </summary>
        SIID_FOLDERFRONT = 76,

        /// <summary>
        ///     <para>
        ///         Security shield. Use for UAC prompts only.
        ///     </para>
        ///     <para>
        ///         UACで使用する盾アイコン。
        ///     </para>
        /// </summary>
        SIID_SHIELD = 77,

        /// <summary>
        ///     <para>
        ///         Warning.
        ///     </para>
        ///     <para>
        ///         警告アイコン。
        ///     </para>
        /// </summary>
        SIID_WARNING = 78,

        /// <summary>
        ///     <para>
        ///         Informational.
        ///     </para>
        ///     <para>
        ///         情報アイコン。
        ///     </para>
        /// </summary>
        SIID_INFO = 79,

        /// <summary>
        ///     <para>
        ///         Error.
        ///     </para>
        ///     <para>
        ///         エラーアイコン。
        ///     </para>
        /// </summary>
        SIID_ERROR = 80,

        /// <summary>
        ///     <para>
        ///         Key.
        ///     </para>
        ///     <para>
        ///         鍵アイコン。
        ///     </para>
        /// </summary>
        SIID_KEY = 81,

        /// <summary>
        ///     <para>
        ///         Software.
        ///     </para>
        ///     <para>
        ///         ソフトウェアアイコン。
        ///     </para>
        /// </summary>
        SIID_SOFTWARE = 82,

        /// <summary>
        ///     <para>
        ///         A UI item, such as a button, that issues a rename command.
        ///     </para>
        ///     <para>
        ///         名前変更アイコン。
        ///     </para>
        /// </summary>
        SIID_RENAME = 83,

        /// <summary>
        ///     <para>
        ///         A UI item, such as a button, that issues a delete command.
        ///     </para>
        ///     <para>
        ///         削除アイコン。
        ///     </para>
        /// </summary>
        SIID_DELETE = 84,

        /// <summary>
        ///     <para>
        ///         Audio DVD media.
        ///     </para>
        ///     <para>
        ///         オーディオDVDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAAUDIODVD = 85,

        /// <summary>
        ///     <para>
        ///         Movie DVD media.
        ///     </para>
        ///     <para>
        ///         映像DVDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAMOVIEDVD = 86,

        /// <summary>
        ///     <para>
        ///         Enhanced CD media.
        ///     </para>
        ///     <para>
        ///         エンハンスドCDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAENHANCEDCD = 87,

        /// <summary>
        ///     <para>
        ///         Enhanced DVD media.
        ///     </para>
        ///     <para>
        ///         エンハンスドDVDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAENHANCEDDVD = 88,

        /// <summary>
        ///     <para>
        ///         High definition DVD media in the HD DVD format.
        ///     </para>
        ///     <para>
        ///         HD-DVDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAHDDVD = 89,

        /// <summary>
        ///     <para>
        ///         High definition DVD media in the Blu-ray Disc™ format.
        ///     </para>
        ///     <para>
        ///         BluRayメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIABLURAY = 90,

        /// <summary>
        ///     <para>
        ///         Video CD (VCD) media.
        ///     </para>
        ///     <para>
        ///         VCDメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAVCD = 91,

        /// <summary>
        ///     <para>
        ///         DVD+R media.
        ///     </para>
        ///     <para>
        ///         DVD+Rメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIADVDPLUSR = 92,

        /// <summary>
        ///     <para>
        ///         DVD+RW media.
        ///     </para>
        ///     <para>
        ///         DVD+RWメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIADVDPLUSRW = 93,

        /// <summary>
        ///     <para>
        ///         A desktop computer.
        ///     </para>
        ///     <para>
        ///         デスクトップコンピューター。
        ///     </para>
        /// </summary>
        SIID_DESKTOPPC = 94,

        /// <summary>
        ///     <para>
        ///         A mobile computer (laptop).
        ///     </para>
        ///     <para>
        ///         モバイルコンピューター。
        ///     </para>
        /// </summary>
        SIID_MOBILEPC = 95,

        /// <summary>
        ///     <para>
        ///         The User Accounts Control Panel item.
        ///     </para>
        ///     <para>
        ///         ユーザーアカウントコントロールパネル。
        ///     </para>
        /// </summary>
        SIID_USERS = 96,

        /// <summary>
        ///     <para>
        ///         Smart media.
        ///     </para>
        ///     <para>
        ///         スマートメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIASMARTMEDIA = 97,

        /// <summary>
        ///     <para>
        ///         CompactFlash media.
        ///     </para>
        ///     <para>
        ///         コンパクトフラッシュメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIACOMPACTFLASH = 98,

        /// <summary>
        ///     <para>
        ///         A cell phone.
        ///     </para>
        ///     <para>
        ///         携帯電話。
        ///     </para>
        /// </summary>
        SIID_DEVICECELLPHONE = 99,

        /// <summary>
        ///     <para>
        ///         A digital camera.
        ///     </para>
        ///     <para>
        ///         カメラ。
        ///     </para>
        /// </summary>
        SIID_DEVICECAMERA = 100,

        /// <summary>
        ///     <para>
        ///         A digital video camera.
        ///     </para>
        ///     <para>
        ///         ビデオカメラ。
        ///     </para>
        /// </summary>
        SIID_DEVICEVIDEOCAMERA = 101,

        /// <summary>
        ///     <para>
        ///         An audio player.
        ///     </para>
        ///     <para>
        ///         オーディオプレーヤー。
        ///     </para>
        /// </summary>
        SIID_DEVICEAUDIOPLAYER = 102,

        /// <summary>
        ///     <para>
        ///         Connect to network.
        ///     </para>
        ///     <para>
        ///         ネットワーク接続。
        ///     </para>
        /// </summary>
        SIID_NETWORKCONNECT = 103,

        /// <summary>
        ///     <para>
        ///         The Network and Internet Control Panel item.
        ///     </para>
        ///     <para>
        ///         ネットワークとインターネットコントロールパネル。
        ///     </para>
        /// </summary>
        SIID_INTERNET = 104,

        /// <summary>
        ///     <para>
        ///         A compressed file with a .zip file name extension.
        ///     </para>
        ///     <para>
        ///         ZIPファイル。
        ///     </para>
        /// </summary>
        SIID_ZIPFILE = 105,

        /// <summary>
        ///     <para>
        ///         The Additional Options Control Panel item.
        ///     </para>
        ///     <para>
        ///         設定。
        ///     </para>
        /// </summary>
        SIID_SETTINGS = 106,

        /// <summary>
        ///     <para>
        ///         High definition DVD drive (any type - HD DVD-ROM, HD DVD-R, HD-DVD-RAM) that uses the HD DVD format.
        ///     </para>
        ///     <para>
        ///         HDDVDドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVEHDDVD = 132,

        /// <summary>
        ///     <para>
        ///         High definition DVD drive (any type - BD-ROM, BD-R, BD-RE) that uses the Blu-ray Disc format.
        ///     </para>
        ///     <para>
        ///         BluRayドライブ。
        ///     </para>
        /// </summary>
        SIID_DRIVEBD = 133,

        /// <summary>
        ///     <para>
        ///         High definition DVD-ROM media in the HD DVD-ROM format.
        ///     </para>
        ///     <para>
        ///         HDDVD-ROMメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAHDDVDROM = 134,

        /// <summary>
        ///     <para>
        ///         High definition DVD-R media in the HD DVD-R format.
        ///     </para>
        ///     <para>
        ///         HDDVD-Rメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAHDDVDR = 135,

        /// <summary>
        ///     <para>
        ///         High definition DVD-RAM media in the HD DVD-RAM format.
        ///     </para>
        ///     <para>
        ///         HDDVD-RAMメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIAHDDVDRAM = 136,

        /// <summary>
        ///     <para>
        ///         High definition DVD-ROM media in the Blu-ray Disc BD-ROM format.
        ///     </para>
        ///     <para>
        ///         BluRay ROMメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIABDROM = 137,

        /// <summary>
        ///     <para>
        ///         High definition write-once media in the Blu-ray Disc BD-R format.
        ///     </para>
        ///     <para>
        ///         BluRay Rメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIABDR = 138,

        /// <summary>
        ///     <para>
        ///         High definition read/write media in the Blu-ray Disc BD-RE format.
        ///     </para>
        ///     <para>
        ///         BluRay REメディア。
        ///     </para>
        /// </summary>
        SIID_MEDIABDRE = 139,

        /// <summary>
        ///     <para>
        ///         A cluster disk array.
        ///     </para>
        ///     <para>
        ///         クラスター化されたディスク。
        ///     </para>
        /// </summary>
        SIID_CLUSTEREDDRIVE = 140,

        /// <summary>
        ///     <para>
        ///         The highest valid value in the enumeration.
        ///     </para>
        ///     <para>
        ///         列挙値の最大値。
        ///     </para>
        /// </summary>
        SIID_MAX_ICONS = 174,
    }
}