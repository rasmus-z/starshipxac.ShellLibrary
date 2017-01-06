using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Dialogs.Interop
{
    /// <summary>
    ///     ウィンドウメッセージを定義します。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class WindowMessages
    {
        public const UInt32 WM_NULL = 0x00;

        public const UInt32 WM_CREATE = 0x01;
        public const UInt32 WM_DESTROY = 0x02;
        public const UInt32 WM_MOVE = 0x03;
        public const UInt32 WM_SIZE = 0x05;
        public const UInt32 WM_ACTIVATE = 0x06;
        public const UInt32 WM_SETFOCUS = 0x07;
        public const UInt32 WM_KILLFOCUS = 0x08;
        public const UInt32 WM_ENABLE = 0x0A;
        public const UInt32 WM_SETREDRAW = 0x0B;
        public const UInt32 WM_SETTEXT = 0x0C;
        public const UInt32 WM_GETTEXT = 0x0D;
        public const UInt32 WM_GETTEXTLENGTH = 0x0E;
        public const UInt32 WM_PAINT = 0x0F;
        public const UInt32 WM_CLOSE = 0x10;
        public const UInt32 WM_QUERYENDSESSION = 0x11;
        public const UInt32 WM_QUIT = 0x12;
        public const UInt32 WM_QUERYOPEN = 0x13;
        public const UInt32 WM_ERASEBACKGROUND = 0x14;
        public const UInt32 WM_SYSTEMCOLORCHANGE = 0x15;
        public const UInt32 WM_ENDSESSION = 0x16;
        public const UInt32 WM_SYSTEMERROR = 0x17;
        public const UInt32 WM_SHOWWINDOW = 0x18;
        public const UInt32 WM_CONTROLCOLOR = 0x19;
        public const UInt32 WM_WININICHANGE = 0x1A;
        public const UInt32 WM_SETTINGCHANGE = 0x1A;
        public const UInt32 WM_DEVMODECHANGE = 0x1B;
        public const UInt32 WM_ACTIVATEAPPLICATION = 0x1C;
        public const UInt32 WM_FONTCHANGE = 0x1D;
        public const UInt32 WM_TIMECHANGE = 0x1E;
        public const UInt32 WM_CANCELMODE = 0x1F;
        public const UInt32 WM_SETCURSOR = 0x20;
        public const UInt32 WM_MOUSEACTIVATE = 0x21;
        public const UInt32 WM_CHILDACTIVATE = 0x22;
        public const UInt32 WM_QUEUESYNC = 0x23;
        public const UInt32 WM_GETMINMAXINFO = 0x24;
        public const UInt32 WM_PAINTICON = 0x26;
        public const UInt32 WM_ICONERASEBACKGROUND = 0x27;
        public const UInt32 WM_NEXTDIALOGCONTROL = 0x28;
        public const UInt32 WM_SPOOLERSTATUS = 0x2A;
        public const UInt32 WM_DRAWITEM = 0x2B;
        public const UInt32 WM_MEASUREITEM = 0x2C;
        public const UInt32 WM_DELETEITEM = 0x2D;
        public const UInt32 WM_VKEYTOITEM = 0x2E;
        public const UInt32 WM_CHARTOITEM = 0x2F;

        public const UInt32 WM_SETFONT = 0x30;
        public const UInt32 WM_GETFONT = 0x31;
        public const UInt32 WM_SETHOTKEY = 0x32;
        public const UInt32 WM_GETHOTKEY = 0x33;
        public const UInt32 WM_QUERYDRAGICON = 0x37;
        public const UInt32 WM_COMPAREITEM = 0x39;
        public const UInt32 WM_COMPACTING = 0x41;
        public const UInt32 WM_WINDOWPOSITIONCHANGING = 0x46;
        public const UInt32 WM_WINDOWPOSITIONCHANGED = 0x47;
        public const UInt32 WM_POWER = 0x48;
        public const UInt32 WM_COPYDATA = 0x4A;
        public const UInt32 WM_CANCELJOURNAL = 0x4B;
        public const UInt32 WM_NOTIFY = 0x4E;
        public const UInt32 WM_INPUTLANGUAGECHANGEREQUEST = 0x50;
        public const UInt32 WM_INPUTLANGUAGECHANGE = 0x51;
        public const UInt32 WM_TCARD = 0x52;
        public const UInt32 WM_HELP = 0x53;
        public const UInt32 WM_USERCHANGED = 0x54;
        public const UInt32 WM_NOTIFYFORMAT = 0x55;
        public const UInt32 WM_CONTEXTMENU = 0x7B;
        public const UInt32 WM_STYLECHANGING = 0x7C;
        public const UInt32 WM_STYLECHANGED = 0x7D;
        public const UInt32 WM_DISPLAYCHANGE = 0x7E;
        public const UInt32 WM_GETICON = 0x7F;
        public const UInt32 WM_SETICON = 0x80;

        public const UInt32 WM_NCCREATE = 0x81;
        public const UInt32 WM_NCDESTROY = 0x82;
        public const UInt32 WM_NCCALCULATESIZE = 0x83;
        public const UInt32 WM_NCHITTEST = 0x84;
        public const UInt32 WM_NCPAINT = 0x85;
        public const UInt32 WM_NCACTIVATE = 0x86;
        public const UInt32 WM_GETDIALOGCODE = 0x87;
        public const UInt32 WM_NCMOUSEMOVE = 0xA0;
        public const UInt32 WM_NCLEFTBUTTONDOWN = 0xA1;
        public const UInt32 WM_NCLEFTBUTTONUP = 0xA2;
        public const UInt32 WM_NCLEFTBUTTONDOUBLECLICK = 0xA3;
        public const UInt32 WM_NCRIGHTBUTTONDOWN = 0xA4;
        public const UInt32 WM_NCRIGHTBUTTONUP = 0xA5;
        public const UInt32 WM_NCRIGHTBUTTONDOUBLECLICK = 0xA6;
        public const UInt32 WM_NCMIDDLEBUTTONDOWN = 0xA7;
        public const UInt32 WM_NCMIDDLEBUTTONUP = 0xA8;
        public const UInt32 WM_NCMIDDLEBUTTONDOUBLECLICK = 0xA9;

        public const UInt32 WM_KEYFIRST = 0x100;
        public const UInt32 WM_KEYDOWN = 0x100;
        public const UInt32 WM_KEYUP = 0x101;
        public const UInt32 WM_CHAR = 0x102;
        public const UInt32 WM_DEADCHAR = 0x103;
        public const UInt32 WM_SYSTEMKEYDOWN = 0x104;
        public const UInt32 WM_SYSTEMKEYUP = 0x105;
        public const UInt32 WM_SYSTEMCHAR = 0x106;
        public const UInt32 WM_SYSTEMDEADCHAR = 0x107;
        public const UInt32 WM_KEYLAST = 0x108;

        public const UInt32 WM_IMESTARTCOMPOSITION = 0x10D;
        public const UInt32 WM_IMEENDCOMPOSITION = 0x10E;
        public const UInt32 WM_IMECOMPOSITION = 0x10F;
        public const UInt32 WM_IMEKEYLAST = 0x10F;

        public const UInt32 WM_INITIALIZEDIALOG = 0x110;
        public const UInt32 WM_COMMAND = 0x111;
        public const UInt32 WM_SYSTEMCOMMAND = 0x112;
        public const UInt32 WM_TIMER = 0x113;
        public const UInt32 WM_HORIZONTALSCROLL = 0x114;
        public const UInt32 WM_VERTICALSCROLL = 0x115;
        public const UInt32 WM_INITIALIZEMENU = 0x116;
        public const UInt32 WM_INITIALIZEMENUPOPUP = 0x117;
        public const UInt32 WM_MENUSELECT = 0x11F;
        public const UInt32 WM_MENUCHAR = 0x120;
        public const UInt32 WM_ENTERIDLE = 0x121;

        public const UInt32 WM_CTLCOLORMESSAGEBOX = 0x132;
        public const UInt32 WM_CTLCOLOREDIT = 0x133;
        public const UInt32 WM_CTLCOLORLISTBOX = 0x134;
        public const UInt32 WM_CTLCOLORBUTTON = 0x135;
        public const UInt32 WM_CTLCOLORDIALOG = 0x136;
        public const UInt32 WM_CTLCOLORSCROLLBAR = 0x137;
        public const UInt32 WM_CTLCOLORSTATIC = 0x138;

        public const UInt32 WM_MOUSEFIRST = 0x200;
        public const UInt32 WM_MOUSEMOVE = 0x200;
        public const UInt32 WM_LEFTBUTTONDOWN = 0x201;
        public const UInt32 WM_LEFTBUTTONUP = 0x202;
        public const UInt32 WM_LEFTBUTTONDOUBLECLICK = 0x203;
        public const UInt32 WM_RIGHTBUTTONDOWN = 0x204;
        public const UInt32 WM_RIGHTBUTTONUP = 0x205;
        public const UInt32 WM_RIGHTBUTTONDOUBLECLICK = 0x206;
        public const UInt32 WM_MIDDLEBUTTONDOWN = 0x207;
        public const UInt32 WM_MIDDLEBUTTONUP = 0x208;
        public const UInt32 WM_MIDDLEBUTTONDOUBLECLICK = 0x209;
        public const UInt32 WM_MOUSEWHEEL = 0x20A;
        public const UInt32 WM_MOUSEHORIZONTALWHEEL = 0x20E;

        public const UInt32 WM_PARENTNOTIFY = 0x210;
        public const UInt32 WM_ENTERMENULOOP = 0x211;
        public const UInt32 WM_EXITMENULOOP = 0x212;
        public const UInt32 WM_NEXTMENU = 0x213;
        public const UInt32 WM_SIZING = 0x214;
        public const UInt32 WM_CAPTURECHANGED = 0x215;
        public const UInt32 WM_MOVING = 0x216;
        public const UInt32 WM_POWERBROADCAST = 0x218;
        public const UInt32 WM_DEVICECHANGE = 0x219;

        public const UInt32 WM_MDICREATE = 0x220;
        public const UInt32 WM_MDIDESTROY = 0x221;
        public const UInt32 WM_MDIACTIVATE = 0x222;
        public const UInt32 WM_MDIRESTORE = 0x223;
        public const UInt32 WM_MDINEXT = 0x224;
        public const UInt32 WM_MDIMAXIMIZE = 0x225;
        public const UInt32 WM_MDITILE = 0x226;
        public const UInt32 WM_MDICASCADE = 0x227;
        public const UInt32 WM_MDIICONARRANGE = 0x228;
        public const UInt32 WM_MDIGETACTIVE = 0x229;
        public const UInt32 WM_MDISETMENU = 0x230;
        public const UInt32 WM_ENTERSIZEMOVE = 0x231;
        public const UInt32 WM_EXITSIZEMOVE = 0x232;
        public const UInt32 WM_DROPFILES = 0x233;
        public const UInt32 WM_MDIREFRESHMENU = 0x234;

        public const UInt32 WM_IMESETCONTEXT = 0x281;
        public const UInt32 WM_IMENOTIFY = 0x282;
        public const UInt32 WM_IMECONTROL = 0x283;
        public const UInt32 WM_IMECOMPOSITIONFULL = 0x284;
        public const UInt32 WM_IMESELECT = 0x285;
        public const UInt32 WM_IMECHAR = 0x286;
        public const UInt32 WM_IMEKEYDOWN = 0x290;
        public const UInt32 WM_IMEKEYUP = 0x291;

        public const UInt32 WM_MOUSEHOVER = 0x2A1;
        public const UInt32 WM_NCMOUSELEAVE = 0x2A2;
        public const UInt32 WM_MOUSELEAVE = 0x2A3;

        public const UInt32 WM_CUT = 0x300;
        public const UInt32 WM_COPY = 0x301;
        public const UInt32 WM_PASTE = 0x302;
        public const UInt32 WM_CLEAR = 0x303;
        public const UInt32 WM_UNDO = 0x304;

        public const UInt32 WM_RENDERFORMAT = 0x305;
        public const UInt32 WM_RENDERALLFORMATS = 0x306;
        public const UInt32 WM_DESTROYCLIPBOARD = 0x307;
        public const UInt32 WM_DRAWCLIPBARD = 0x308;
        public const UInt32 WM_PAINTCLIPBARD = 0x309;
        public const UInt32 WM_VERTICALSCROLLCLIPBOARD = 0x30A;
        public const UInt32 WM_SIZECLIPBARD = 0x30B;
        public const UInt32 WM_ASKCLIPBOARDFORMATNAME = 0x30C;
        public const UInt32 WM_CHANGECLIPBOARDCHAIN = 0x30D;
        public const UInt32 WM_HORIZONTALSCROLLCLIPBOARD = 0x30E;
        public const UInt32 WM_QUERYNEWPALETTE = 0x30F;
        public const UInt32 WM_PALETTEISCHANGING = 0x310;
        public const UInt32 WM_PALETTECHANGED = 0x311;

        public const UInt32 WM_HOTKEY = 0x312;
        public const UInt32 WM_PRINT = 0x317;
        public const UInt32 WM_PRINTCLIENT = 0x318;

        public const UInt32 WM_HANDHELDFIRST = 0x358;
        public const UInt32 WM_HANDHELDLAST = 0x35F;
        public const UInt32 WM_PENWINFIRST = 0x380;
        public const UInt32 WM_PENWINLAST = 0x38F;
        public const UInt32 WM_COALESCEFIRST = 0x390;
        public const UInt32 WM_COALESCELAST = 0x39F;
        public const UInt32 WM_DDE_FIRST = 0x3E0;
        public const UInt32 WM_DDE_INITIATE = 0x3E0;
        public const UInt32 WM_DDE_TERMINATE = 0x3E1;
        public const UInt32 WM_DDE_ADVISE = 0x3E2;
        public const UInt32 WM_DDE_UNADVISE = 0x3E3;
        public const UInt32 WM_DDE_ACK = 0x3E4;
        public const UInt32 WM_DDE_DATA = 0x3E5;
        public const UInt32 WM_DDE_REQUEST = 0x3E6;
        public const UInt32 WM_DDE_POKE = 0x3E7;
        public const UInt32 WM_DDE_EXECUTE = 0x3E8;
        public const UInt32 WM_DDE_LAST = 0x3E8;

        public const UInt32 WM_USER = 0x400;
        public const UInt32 WM_APP = 0x8000;

        public const UInt32 MaxAppMessage = 0xBFFF;

        // 0xC000 - 0xFFFF: RegisterWindowMessageで使用
        // 0x10000以上    : システムで予約
    }
}