﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Sprache;

namespace m3uParser.Model
{

    public class M3UParserException : Exception { 
    
        public M3UParserException(string message):base(message) { }

        public M3UParserException(string message, Exception inner) : base(message, inner) { }

    }

    public class Extm3u
    {
        public string PlayListType { get; set; }
        public bool HasEndList { get; set; }
        public int? TargetDuration { get; set; }
        public int? Version { get; set; }
        public int? MediaSequence { get; set; }
        public Attributes Attributes { get; set; }
        public IEnumerable<Media> Medias { get; set; }
        public IEnumerable<string> Warnings { get; set; }

        internal Extm3u(string content)
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var segments = SegmentSpecification.SegmentCollection.Parse(content);

            

            if (segments == null || segments.Count() == 0)
            {
                throw new M3UParserException("The content cannot be parsed. Zero segments found.");
            }

            if (!segments.First().StartsWith("#EXTM3U", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new M3UParserException("The content do not has extm3u header");
            }
            else
            {
                // parse attributes
                Attributes = new Attributes(LinesSpecification.HeaderLine.Parse(segments.First()));
            }

            IList<string> warnings = new List<string>();
            IList<Media> medias = new List<Media>();
            Media lastMedia = null;

            foreach (var item in segments.Skip(1))
            {
                var tag = PairsSpecification.Tag.Parse(item);

                switch (tag.Key)
                {
                    case "EXT-X-PLAYLIST-TYPE":
                        this.PlayListType = tag.Value;
                        break;

                    case "EXT-X-TARGETDURATION":
                        this.TargetDuration = int.Parse(tag.Value);
                        break;

                    case "EXT-X-VERSION":
                        this.Version = int.Parse(tag.Value);
                        break;

                    case "EXT-X-MEDIA-SEQUENCE":
                        this.MediaSequence = int.Parse(tag.Value);
                        break;

                    case "EXTINF":
                        try
                        {
                            lastMedia = LinesSpecification.Extinf.Parse(tag.Value); 
                            medias.Add(lastMedia);
                        }
                        catch
                        {
                            warnings.Add($"Can't parse media #{tag.Key}{(string.IsNullOrEmpty(tag.Value) ? string.Empty : ":")}{tag.Value}");
                        }
                        break;
                    
                    //this is the minimal change that I can do to support EXTGRP and avoiding breaking anything
                    case "EXTGRP":
                        try
                        {
                            if (lastMedia == null) {

                                warnings.Add($"Improper EXTGRP tag found");                                
                                break;
                            }

                            var m = LinesSpecification.Extgrp.Parse(tag.Value);
                            lastMedia.Group = m.Group;
                            if(string.IsNullOrEmpty( lastMedia.MediaFile ))
                                lastMedia.MediaFile = m.MediaFile;
                        }
                        catch
                        {
                            warnings.Add($"Can't parse media #{tag.Key}{(string.IsNullOrEmpty(tag.Value) ? string.Empty : ":")}{tag.Value}");
                        }
                        break;

                    //this is the minimal change that I can do to support EXTGRP and avoiding breaking anything
                    case "EXTVLCOPT":
                        try
                        {
                            if (lastMedia == null)
                            {

                                warnings.Add($"Improper EXTVLCOPT tag found");
                                break;
                            }

                            var m = LinesSpecification.ExtVLT.Parse(tag.Value);
                            
                            lastMedia.VlCopt = m.VlCopt;

                            if (string.IsNullOrEmpty(lastMedia.MediaFile))
                                lastMedia.MediaFile = m.MediaFile;
                        }
                        catch
                        {
                            warnings.Add($"Can't parse media #{tag.Key}{(string.IsNullOrEmpty(tag.Value) ? string.Empty : ":")}{tag.Value}");
                        }
                        break;

                    case "EXT-X-ENDLIST":
                        this.HasEndList = true;
                        break;

                    default:
                        warnings.Add($"Can't parse content #{tag.Key}{(string.IsNullOrEmpty(tag.Value) ? string.Empty : ":")}{tag.Value}");

                        break;
                }
            }

            this.Warnings = warnings.AsEnumerable();
            this.Medias = medias.AsEnumerable();
        }
    }
}
