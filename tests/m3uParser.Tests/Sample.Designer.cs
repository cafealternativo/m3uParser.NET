﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace m3uParser.Tests {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Sample {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Sample() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("m3uParser.Tests.Sample", typeof(Sample).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #EXTM3U
        ///#EXTINF:0,#EXTM3U 
        ///#EXTINF:-1, [COLOR yellow]***FREE SKYNET TV*** [/COLOR]
        ///#EXTINF:0,http://95.128.90.97:1232/udp/239.1.8.2:1232
        ///#EXTINF:-1, [COLOR green]****Sky Sports**** [/COLOR]
        ///#EXTINF:0,http://95.128.90.97:1234/udp/239.1.8.2:1234
        ///http://937.48.118.32/live/skysports1.stream/playlist.m3u8
        ///#EXTINF:0,#EXTINF:0, Sky Sports 1
        ///http://9198.27.94.105/hls/skysports1.m3u8
        ///#EXTINF:0,#EXTINF:0, Sky Sports 2
        ///http://9198.27.94.105/hls/skysports2.m3u8
        ///#EXTINF:0,http://937.48.118.32/live/skysports2. [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string big_list {
            get {
                return ResourceManager.GetString("big_list", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #EXTM3U url-tvg=&quot;http://server/jtv.zip&quot; refresh=&quot;3600&quot;
        ///#EXTINF:-1 group-title=&quot;Free channels&quot;,Channel One
        ///udp://225.55.55.1:1234
        ///#EXTINF:-1 group-title=&quot;Free channels&quot;,Channel Two
        ///udp://225.55.55.2:1234
        ///#EXTINF:-1 group-title=&quot;Free channels&quot;,News Channel
        ///udp://225.55.55.3:1234
        ///#EXTINF:-1 group-title=&quot;Free channels&quot;,News 2 Channel
        ///http://udpxy.domain.ru:5555/udp/225.55.55.55:1234
        ///#EXTINF:-1 group-title=&quot;Music channels&quot;,MTV
        ///udp://225.55.55.4:1234.
        /// </summary>
        internal static string format_specs {
            get {
                return ResourceManager.GetString("format_specs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #EXTM3U url-tvg=&quot;http://www.teleguide.info/download/new3/jtv.zip&quot; m3uautoload=1 cache=500 deinterlace=1 tvg-shift=0
        ///#EXTINF:-1 tvgname=&quot;Первый_канал&quot; tvglogo=&quot;Первый канал&quot; grouptitle=&quot;Каналы ЦЭТВ РТРС&quot; ,Первый канал
        ///http://192.168.1.1:4022/udp/225.77.225.1:5000
        ///#EXTINF:-1 tvg-name=&quot;Россия_1&quot; tvg-logo=&quot;Россия&quot; ,Россия
        ///http://192.168.1.1:4022/udp/225.77.225.2:5000
        ///#EXTINF:-1 tvg-name=&quot;Матч!&quot; tvg-logo=&quot;Матч ТВ&quot; ,Матч ТВ
        ///http://192.168.1.1:4022/udp/225.77.225.3:5000.
        /// </summary>
        internal static string header_with_parameters {
            get {
                return ResourceManager.GetString("header_with_parameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #EXTM3U url-tvg=&quot;http://www.teleguide.info/download/new3/xmltv.xml.gz&quot;
        ///#EXTINF:-1 group-title=&quot;Science&quot;, Discovery
        ///http://example.com/channel1
        ///#EXTINF:-1 group-title=&quot;Sport&quot;, Eurosport
        ///http://example.com/channel2
        ///#EXTINF:-1 tvg-logo=&quot;Eurosport&quot; tvg-name=&quot;Eurosport&quot; tvg-shift=&quot;+1&quot;,Eurosport +1
        ///http://example.com/channel3
        ///#EXTINF:-1 group-title=&quot;Custom&quot; tvg-logo=&quot;https://cdn1.iconfinder.com/data/icons/Primo_Icons/PNG/128x128/video.png&quot;,My Custom channel
        ///http://example.com/channel4.
        /// </summary>
        internal static string iptv_sample {
            get {
                return ResourceManager.GetString("iptv_sample", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #EXTM3U url-tvg=&quot;http://raw.githubusercontent.com/Twilight0/repo-guide/master/guide-el.xml.gz&quot;
        ///#EXTINF:-1 group-title=&quot;ΠΑΝΕΛΛΑΔΙΚΑ&quot; tvg-name=&quot;ΕΡΤ1&quot; tvg-logo=&quot;http://greektv.pbworks.com/f/1433976522/ERT1.png&quot;,ERT1
        ///https://www.youtube.com/watch?v=WY_Zhh9-bI8
        ///#EXTINF:-1 group-title=&quot;ΠΑΝΕΛΛΑΔΙΚΑ&quot; tvg-name=&quot;ΕΡΤ1&quot; tvg-logo=&quot;http://greektv.pbworks.com/f/1433976522/ERT1.png&quot;,ERT1 WORLDWIDE
        ///https://www.youtube.com/watch?v=Zx98moCvkpU
        ///#EXTINF:-1 group-title=&quot;ΠΑΝΕΛΛΑΔΙΚΑ&quot; tvg-name=&quot;ΕΡΤ2&quot; tvg-logo=&quot;http://greektv. [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string sample_paste_bin {
            get {
                return ResourceManager.GetString("sample_paste_bin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #EXTM3U
        ///#EXT-X-PLAYLIST-TYPE:VOD
        ///#EXT-X-TARGETDURATION:10
        ///#EXT-X-VERSION:3
        ///#EXT-X-MEDIA-SEQUENCE:0
        ///#EXTINF:10.0,
        ///http://example.com/movie1/fileSequenceA.ts
        ///#EXTINF:10.0,
        ///http://example.com/movie1/fileSequenceB.ts
        ///#EXTINF:10.0,
        ///http://example.com/movie1/fileSequenceC.ts
        ///#EXTINF:9.0,
        ///http://example.com/movie1/fileSequenceD.ts
        ///#EXT-X-ENDLIST.
        /// </summary>
        internal static string simple_vod {
            get {
                return ResourceManager.GetString("simple_vod", resourceCulture);
            }
        }
    }
}
