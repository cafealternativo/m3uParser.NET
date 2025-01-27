﻿using m3uParser.Model;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace m3uParser
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/M3U
    /// https://github.com/sprache/Sprache
    /// https://tools.ietf.org/html/draft-pantos-http-live-streaming-23#section-4.2
    /// http://ss-iptv.com/en/users/documents/m3u
    /// https://developer.apple.com/library/content/technotes/tn2288/_index.html
    /// https://developer.apple.com/documentation/http_live_streaming/example_playlists_for_http_live_streaming/event_playlist_construction
    /// https://tools.ietf.org/html/draft-pantos-http-live-streaming-23#page-12
    /// https://wiki.tvip.ru/en/m3u
    /// https://pastebin.com/KNYEZNun
    /// https://github.com/AlexanderSofronov/iptv.example
    /// </summary>
    public static class M3U
    {
        public static Extm3u Parse(string content)
        {

            try
            {
                return new Extm3u(content);
            }
            catch (M3UParserException ex) 
                { throw ex; }
            catch (Exception ex)
            {
                throw new M3UParserException("Could not parse content", ex);

            }
        }

        public static Extm3u ParseBytes(byte[] byteArr)
        {
            return Parse(Encoding.Default.GetString(byteArr));
        }

        public static Extm3u ParseFromFile(string file)
        {
            return Parse(System.IO.File.ReadAllText(file));
        }

        public static async Task<Extm3u> ParseFromUrlAsync(string requestUri)
        {
            return await ParseFromUrlAsync(requestUri, new HttpClient());
        }

        public static async Task<Extm3u> ParseFromUrlAsync(string requestUri, HttpClient client)
        {
            var get = await client.GetAsync(requestUri);
            var content = await get.Content.ReadAsStringAsync();
            return Parse(content);
        }
    }
}