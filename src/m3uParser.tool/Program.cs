using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using m3uParser.Model;
using Sprache;

namespace m3uParser.tool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customFile = Path.Combine(@"C:\TEMP\xenu", "samsung.m3u8");
            var simplem3u = M3U.ParseFromFile(customFile);
            
            //var simpleVodM3u = M3U.Parse(simpleVod);
            //var headerParameterM3u = M3U.Parse(headerParameter);

            //var simplem3u = M3U.Parse(problemList);

            Console.WriteLine("Warnings: " + simplem3u.Warnings.Count());
        }

        //Every # symbol is being interpreted as a comment inside the list and is breaking the line

        static readonly string samsungPlaylist =
@"#EXTM3U x-tvg-url=""https://i.mjh.nz/SamsungTVPlus/all.xml""
#EXTINF:-1 channel-id=""samsung-ATBB3100001RH"" tvg-id=""ATBB3100001RH"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATBB3100001RH_20230913T030447SQUARE.png_20230913030447.png"" tvg-chno=""4001"" group-title=""Austria"" , Crime Mix
https://i.mjh.nz/SamsungTVPlus/ATBB3100001RH.m3u8

#EXTINF:-1 channel-id=""samsung-ATBA1000004XC"" tvg-id=""ATBA1000004XC"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATBA1000004XC_20230222T012142SQUARE.png_20230222012142.png"" tvg-chno=""4003"" group-title=""Austria"" , Euronews Live
https://i.mjh.nz/SamsungTVPlus/ATBA1000004XC.m3u8

#EXTINF:-1 channel-id=""samsung-ATAJ35000024Y"" tvg-id=""ATAJ35000024Y"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAJ35000024Y_20230926T032230SQUARE.png_20230926032231.png"" tvg-chno=""4004"" group-title=""Austria"" , Komödien - Rakuten TV
https://i.mjh.nz/SamsungTVPlus/ATAJ35000024Y.m3u8

#EXTINF:-1 channel-id=""samsung-ATBD800001VA"" tvg-id=""ATBD800001VA"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATBD800001VA_20230308T021713SQUARE.png_20230308021714.png"" tvg-chno=""4005"" group-title=""Austria"" , CNN
https://i.mjh.nz/SamsungTVPlus/ATBD800001VA.m3u8

#EXTINF:-1 channel-id=""samsung-ATAJ0500427A"" tvg-id=""ATAJ0500427A"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAJ0500427A_20230913T030438SQUARE.png_20230913030438.png"" tvg-chno=""4006"" group-title=""Austria"" , Tierwelt
https://i.mjh.nz/SamsungTVPlus/ATAJ0500427A.m3u8

#EXTINF:-1 channel-id=""samsung-ATBD1500001X4"" tvg-id=""ATBD1500001X4"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATBD1500001X4_20230510T043844SQUARE.png_20230510043845.png"" tvg-chno=""4009"" group-title=""Austria"" , Lagerfeuer
https://i.mjh.nz/SamsungTVPlus/ATBD1500001X4.m3u8

#EXTINF:-1 channel-id=""samsung-ATAK3504517A"" tvg-id=""ATAK3504517A"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAK3504517A_20221026T004148SQUARE.png_20221026004148.png"" tvg-chno=""4110"" group-title=""Austria"" , MTV Catfish
https://i.mjh.nz/SamsungTVPlus/ATAK3504517A.m3u8

#EXTINF:-1 channel-id=""samsung-ATBD2600001QK"" tvg-id=""ATBD2600001QK"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATBD2600001QK_20230809T053624SQUARE.png_20230809053625.png"" tvg-chno=""4112"" group-title=""Austria"" , Hell's Kitchen
https://i.mjh.nz/SamsungTVPlus/ATBD2600001QK.m3u8

#EXTINF:-1 channel-id=""samsung-ATAK3504505A"" tvg-id=""ATAK3504505A"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAK3504505A_20230809T053604SQUARE.png_20230809053605.png"" tvg-chno=""4114"" group-title=""Austria"" , Pluto TV Sitcoms
https://i.mjh.nz/SamsungTVPlus/ATAK3504505A.m3u8

#EXTINF:-1 channel-id=""samsung-ATAJ32000027V"" tvg-id=""ATAJ32000027V"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAJ32000027V_20221026T004149SQUARE.png_20221026004149.png"" tvg-chno=""4118"" group-title=""Austria"" , FailArmy
https://i.mjh.nz/SamsungTVPlus/ATAJ32000027V.m3u8

#EXTINF:-1 channel-id=""samsung-ATBA33000270C"" tvg-id=""ATBA33000270C"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATBA33000270C_20230426T015535SQUARE.png_20230426015536.png"" tvg-chno=""4120"" group-title=""Austria"" , Craction TV
https://i.mjh.nz/SamsungTVPlus/ATBA33000270C.m3u8

#EXTINF:-1 channel-id=""samsung-ATAJ3200003W3"" tvg-id=""ATAJ3200003W3"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAJ3200003W3_20230222T012140SQUARE.png_20230222012141.png"" tvg-chno=""4126"" group-title=""Austria"" , The Pet Collective
https://i.mjh.nz/SamsungTVPlus/ATAJ3200003W3.m3u8

#EXTINF:-1 channel-id=""samsung-ATAK3504515A"" tvg-id=""ATAK3504515A"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAK3504515A_20221026T004146SQUARE.png_20221026004146.png"" tvg-chno=""4128"" group-title=""Austria"" , Comedy Central Made in Germany
https://i.mjh.nz/SamsungTVPlus/ATAK3504515A.m3u8

#EXTINF:-1 channel-id=""samsung-ATAJ7000105F"" tvg-id=""ATAJ7000105F"" tvg-logo=""https://tvpnlogopeu.samsungcloud.tv/platform/image/sourcelogo/vc/00/02/34/ATAJ7000105F_20220323T072705SQUARE.png_20220323072708.png"" tvg-chno=""4130"" group-title=""Austria"" , MTV Teen Mom
https://i.mjh.nz/SamsungTVPlus/ATAJ7000105F.m3u8";

        static readonly string problemList =
 @"#EXTM3U
#EXTINF:-1 tvg-id=""ESNEWS.us"" group-title=""Sports News"" tvg-logo=""https://192.168.1.50/uploads/1518524153.png"" tvg-name=""ESNews"" tvg-num=""1"", ESNews
https://192.168.1.50/viewsa/ch01q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzI
#EXTINF:-1 tvg-id=""US0"" group-title=""EVENTS"" tvg-logo=""https://192.168.1.50/uploads/USsmall.png"" tvg-name=""US Event Channel #0"" tvg-num=""90"", US Event Channel #0
https://192.168.1.50/viewsa/ch90q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFz
#EXTINF:-1 tvg-id=""EU13"" group-title=""TBD(ComingSoon)"" tvg-logo=""https://192.168.1.50/uploads/"" tvg-name=""Plus 8"" tvg-num=""152"", Plus 8
https://192.168.1.50/viewsa/ch152q1/playlist.m3u8?wmsAuthSign=NumberMS8y
";



        static readonly string simplePlaylist =
            @"#EXTM3U x-tvg-url=""https://tvabiertapublicfiles.blob.core.windows.net/crc/xmltv""

#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:3

##COMMENT

#EXT-X-MEDIA-SEQUENCE:0

#EXTINF:-1 channelNumber=2 tvgId=2 tvg-id=2 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_2_CDR.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_2_CDR.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Repretel Canal 2
#EXTGRP:no EXTVLCOPT with group
https://firststream.cloudfront.net/hls/canal2.m3u8

#EXTINF:-1 channelNumber=4 tvgId=4 tvg-id=4 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_4_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_4_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Repretel Canal 4
#EXTVLCOPT:VLCOPT FIRST nO GROUP
https://secondstream.cloudfront.net/hls/canal4.m3u8

#EXTINF:-1 channelNumber=6 tvgId=6 tvg-id=6 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_6_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_6_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Repretel Canal 6
#EXTVLCOPT:VLCOPT FIRST THEN Xenu group
#EXTGRP:xenu
https://third.cloudfront.net/hls/canal6.m3u8

#EXTINF:-1 channelNumber=7 tvgId=7 tvg-id=7 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Teletica_Logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Teletica_Logo.png"" tvgurl=""https://www.dailymotion.com/embed/video/x29e3wg"" tvg-url=""https://www.dailymotion.com/embed/video/x29e3wg"" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Teletica Canal 7
#EXTGRP: GROUP FIRST, THEN COMMENT, THEN EXTVLTCOPT
##comment here!
#EXTVLCOPT:this is a GROUP with vlcopt after
http://fourth.fullspeed.tv/iptv-query?streaming-ip=https://www.dailymotion.com/video/x29e3wg

#EXTINF:-1 channelNumber=8 tvgId=8 tvg-id=8 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Multimedios Canal 8
#EXTVLCOPT:ASFDFSADFASD
https://5.com/live-stream-playlist/5a7b1e63a8da282c34d65445.m3u8

#EXTINF:-1 channelNumber=8 tvgId=8 tvg-id=8 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Multimedios Canal 8
#EXTVLCOPT:vltcopt first, no group
https://6.com/live-stream-playlist/5a7b1e63a8da282c34d65445.m3u8

#EXTINF:-1 channelNumber=8 tvgId=8 tvg-id=8 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Multimedios Canal 8
#comment

#EXTVLCOPT:http-user-agent=Mozilla/5.0 (iPhone; CPU iPhone OS 12_2 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148

https://7TH.com/live-stream-playlist/5a7b1e63a8da282c34d65445.m3u8

#comment

#EXTINF:-1 channelNumber=8 tvgId=8 tvg-id=8 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""lastcountry"" tvgcountry=""CRC""tvgname="""" tvg-name="""", The last stream
https://laststream.NODIRECTIVES.com/live-stream-playlist/5a7b1e63a8da282c34d65445.m3u8

";






        static readonly string simplePlaylist1 =
            @"#EXTM3U x-tvg-url=""https://tvabiertapublicfiles.blob.core.windows.net/crc/xmltv""
#EXTINF:-1 channelNumber=2 tvgId=2 tvg-id=2 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_2_CDR.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_2_CDR.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Repretel Canal 2
https://d3bgcstab9qhdz.cloudfront.net/hls/canal2.m3u8
#EXTINF:-1 channelNumber=4 tvgId=4 tvg-id=4 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_4_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_4_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Repretel Canal 4
https://d3bgcstab9qhdz.cloudfront.net/hls/canal4.m3u8
#EXTINF:-1 channelNumber=6 tvgId=6 tvg-id=6 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_6_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Repretel_6_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Repretel Canal 6
https://d3bgcstab9qhdz.cloudfront.net/hls/canal6.m3u8
#EXTINF:-1 channelNumber=7 tvgId=7 tvg-id=7 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Teletica_Logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/Teletica_Logo.png"" tvgurl=""https://www.dailymotion.com/embed/video/x29e3wg"" tvg-url=""https://www.dailymotion.com/embed/video/x29e3wg"" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Teletica Canal 7
http://free.fullspeed.tv/iptv-query?streaming-ip=https://www.dailymotion.com/video/x29e3wg
#EXTINF:-1 channelNumber=8 tvgId=8 tvg-id=8 tvglogo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvg-logo=""https://tvabiertapublicfiles.blob.core.windows.net/images/multimedios_8_logo.png"" tvgurl="""" tvg-url="""" group-title="""" tvg-country=""CRC"" tvgcountry=""CRC""tvgname="""" tvg-name="""", Multimedios Canal 8
https://mdstrm.com/live-stream-playlist/5a7b1e63a8da282c34d65445.m3u8";





        static readonly string simpleVod = @"#EXTM3U
#EXT-X-PLAYLIST-TYPE:VOD
#EXT-X-TARGETDURATION:10
#EXT-X-VERSION:3

##COMMENT

#EXT-X-MEDIA-SEQUENCE:0
#EXTINF:10.0,
http://example.com/movie1/fileSequenceA.ts
#EXTINF:10.0,
http://example.com/movie1/fileSequenceB.ts



#EXTINF:10.0,
http://example.com/movie1/fileSequenceC.ts


#EXTINF:9.0,

http://example.com/movie1/fileSequenceD.ts

#EXT-X-ENDLIST";

        static readonly string headerParameter = @"#EXTM3U url-tvg=""http://www.teleguide.info/download/new3/jtv.zip"" m3uautoload=1 cache=500 deinterlace=1 tvg-shift=0
#EXTINF:-1 tvgname=""Первый_канал"" tvglogo=""Первый канал"" grouptitle=""Каналы ЦЭТВ РТРС"" ,Первый канал
http://192.168.1.1:4022/udp/225.77.225.1:5000
#EXTINF:-1 tvg-name=""Россия_1"" tvg-logo=""Россия"" ,Россия
http://192.168.1.1:4022/udp/225.77.225.2:5000
#EXTINF:-1 tvg-name=""Матч!"" tvg-logo=""Матч ТВ"" ,Матч ТВ
http://192.168.1.1:4022/udp/225.77.225.3:5000";

    }
}