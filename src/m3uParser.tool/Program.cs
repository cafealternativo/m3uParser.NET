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
            //var customFile = Path.Combine(@"C:\Data\Desktop", "test.m3u");
            //var customM3u = M3U.ParseFromFile(customFile);
            //var simpleVodM3u = M3U.Parse(simpleVod);
            //var headerParameterM3u = M3U.Parse(headerParameter);

            var simplem3u = M3U.Parse(problemList);

            Console.WriteLine("Warnings: " + simplem3u.Warnings.Count());
        }

        //Every # symbol is being interpreted as a comment inside the list and is breaking the line

        static readonly string problemList =
 @"#EXTM3U
#EXTINF:-1 tvg-id=""ESPNEWS.us"" group-title=""Sports News"" tvg-logo=""https://xxx.url2.net/uploads/1518524153.png"" tvg-name=""ESPNNews"" tvg-num=""1"", ESPNNews
https://xxx.url.net/viewsa/ch01q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""US0"" group-title=""EVENTS (NFL, EPL, PPV)"" tvg-logo=""https://xxx.url2.net/uploads/USsmall.png"" tvg-name=""US Event Channel #0"" tvg-num=""90"", US Event Channel #0
https://xxx.url.net/viewsa/ch90q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""EU13"" group-title=""TBD(ComingSoon)"" tvg-logo=""https://xxx.url2.net/uploads/"" tvg-name=""Plus 8"" tvg-num=""152"", Plus 8
https://xxx.url.net/viewsa/ch152q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
";
        /*
 @"
#EXTM3U
#EXTINF:-1 tvg-id=""ESPNEWS.us"" group-title=""Sports News"" tvg-logo=""https://xxx.url2.net/uploads/1518524153.png"" tvg-name=""ESPNNews"" tvg-num=""1"", ESPNNews
https://xxx.url.net/viewsa/ch01q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""ESPN.us"" group-title=""Sports News"" tvg-logo=""https://xxx.url2.net/uploads/1518524153-1.png"" tvg-name=""ESPN"" tvg-num=""2"", ESPN
https://xxx.url.net/viewsa/ch02q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""ESPN2.us"" group-title=""Sports News"" tvg-logo=""https://xxx.url2.net/uploads/1518524153-2.png"" tvg-name=""ESPN2"" tvg-num=""3"", ESPN2
https://xxx.url.net/viewsa/ch03q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""US0"" group-title=""EVENTS (NFL, EPL, PPV)"" tvg-logo=""https://xxx.url2.net/uploads/USsmall.png"" tvg-name=""US Event Channel #0"" tvg-num=""90"", US Event Channel #0
https://xxx.url.net/viewsa/ch90q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=        
#EXTINF:-1 tvg-id=""US1"" group-title=""EVENTS (NFL, EPL, PPV)"" tvg-logo=""https://xxx.url2.net/uploads/USsmall.png"" tvg-name=""US Event Channel #1"" tvg-num=""91"", US Event Channel #1
https://xxx.url.net/viewsa/ch91q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""NBCSportsonPeacock.us"" group-title=""US Regional Sports"" tvg-logo=""https://xxx.url2.net/uploads/"" tvg-name=""NBCSN"" tvg-num=""143"", NBCSN
https://xxx.url.net/viewsa/ch143q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""EU11"" group-title=""TBD(ComingSoon)"" tvg-logo=""https://xxx.url2.net/uploads/"" tvg-name=""NZ SS Mix #2"" tvg-num=""150"", NZ SS Mix #2
https://xxx.url.net/viewsa/ch150q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
#EXTINF:-1 tvg-id=""EU13"" group-title=""TBD(ComingSoon)"" tvg-logo=""https://xxx.url2.net/uploads/"" tvg-name=""Plus 8"" tvg-num=""152"", Plus 8
https://xxx.url.net/viewsa/ch152q1/playlist.m3u8?wmsAuthSign=NumberMS8yMDIzIDI6NDQ6MjAgUE0maGFzaEncryptingEEwUlJQeUxFSndVTFdRPT0mdObscuringMjAwJmlkPTYwMjkzJmlwPTk2LjQ0LjE5MS4xMzU=
         
         */


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