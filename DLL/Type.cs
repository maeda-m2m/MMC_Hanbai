using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLL
{
    public enum EnumUserKubun
    {
        Honsya = 0,
        Yodokou = 1,
        Tokuisaki = 2,
        Shiiresaki = 3
    }

    /*
    public enum EnumEigyousyo
    {
        Honsya = 0,
        Hanshin = 1,
        Kyoto = 2,
        Osaka = 3,
        Himeji = 4,
        Kouji = 5
    }
    */

    public enum EnumNyukinJoutai
    {
        Minyukin = 1,
        Bunkatsu = 2,
        Kanryou = 3
    }

    public enum EnumYesNo
    {
        None = 0,
        Yes = 1,
        No = 0
    }

    public enum EnumTorihikiKuza
    {
        Futsu = 1,
        Sougou = 2,
        Touza = 3
    }

    public enum EnumRenrakuHouhou
    {
        None = 0,
        Denwa = 1,
        FAX = 2,
        Email = 3,
        WEB = 4
    }

    public enum EnumSyukin
    {
        Tougetsu =0,
        YokuGetsu = 1,
        YokuYokuGetsu = 2,
    }

    public enum EnumNouhinKubun
    {
        Yodokou = 0,
        Tokuisaki = 1,
        //Nouhinsaki = 2,//変更＆追加2,3,4 2012/05
        Hanshin = 2,
        Oosaka = 3,
        Kyouto = 4,
        Tourokuzumi = 5
    }

    public class NyukinKubun
    {
        public const int IKKATU = 0;
        public const int JYUCHU_TANI = 1;
        public const int KOBETU_TANI = 2;
    }

    public class TorihikisakiType
    {
        public const int TOKUISAKI = 2;
        public const int SHIRESAKI = 3;
    }

    public class ZeiKubun
    {
        public const byte SHISHA_GONYU = 0;
        public const byte KIRI_AGE = 1;
        public const byte KIRI_SUTE = 2;
    }

    public class Eigyousho
    {
        public const string HONSHA = "0";
        public const string HANSHIN = "1";
        public const string OSAKA = "2";
        public const string KYOTO = "3";
        public const string KOUJI = "5";
    }
}
