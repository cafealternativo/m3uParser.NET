﻿using Sprache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("m3uParser.Tests")]
namespace m3uParser
{
    internal static class PrimitiveSpecification
    {

      
        internal static readonly Parser<string> ValueNumber =
            from value in Parse.Number.Text()
            select value;

        internal static readonly Parser<string> ValueStringQuoted =
            from open in Parse.Char('"').Once()
            from value in Parse.CharExcept('"').Many().Text()
            from close in Parse.Char('"').Once()
            select value;

        internal static readonly Parser<string> TagString =
            from value in Parse.CharExcept(':').Many().Text()
            select value;

        internal static readonly Parser<decimal> DecimalSigned =
            from op in Parse.Optional(Parse.Char('-').Token())
            from num in Parse.DecimalInvariant
            select decimal.Parse(num, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) * (op.IsDefined ? -1 : 1);
    }

    internal static class PairsSpecification
    {
        internal static readonly Parser<string> KeyAttribute =
            from key in Parse.AnyChar.Except(Parse.Char(' ').Or(Parse.Char('=')).Or(Parse.Char(','))).Many().Text()
            select key;

        internal static readonly Parser<KeyValuePair<string, string>> Attribute =
            from space in Parse.WhiteSpace.Optional()
            from key in KeyAttribute
            from assignment in Parse.Char('=')
            from value in PrimitiveSpecification.ValueStringQuoted.Optional().Or(PrimitiveSpecification.ValueNumber.Optional())
            from space2 in Parse.WhiteSpace.Optional()
            select new KeyValuePair<string, string>(key, value.GetOrDefault());

        // https://gitter.im/sprache/Sprache/archives/2017/02/20
        internal static readonly Parser<List<KeyValuePair<string, string>>> Attributes =
            from space in Parse.WhiteSpace.Optional()
            from att in PairsSpecification.Attribute.Many()
            from space2 in Parse.WhiteSpace.Optional()
            select new List<KeyValuePair<string, string>>(att);

        internal static readonly Parser<KeyValuePair<string, string>> Tag =
            from starting in Parse.String("#").Text()
            from key in PrimitiveSpecification.TagString.Token()
            from div in Parse.String(":").Text().Optional()
            from value in Parse.AnyChar.Many()
            //from value in Parse.Chars((Environment.NewLine).Many()
            //from end in Parse.Chars(Environment.NewLine).Optional()
            select new KeyValuePair<string, string>(key.ToUpper().Trim(), string.Concat(value).Trim());
    }

    internal static class LinesSpecification
    {
        internal static readonly Parser<List<KeyValuePair<string, string>>> HeaderLine =
                from header in Parse.String("#EXTM3U").Text()
                from param in PairsSpecification.Attributes.Token()
                from endOfLine in Parse.LineEnd.Optional()
                select new List<KeyValuePair<string, string>>(param);

        // https://stackoverflow.com/questions/21414309/sprache-parse-signed-integer
        internal static readonly Parser<Media> ExtinfFirstLine =
            //from open in Parse.String("#EXTINF:").Text()
            from duration in PrimitiveSpecification.DecimalSigned.Once()
            from param in PairsSpecification.Attributes.Token()
            from comma in Parse.Char(',')
            from title in Title.Optional()
            select new Media(duration.First(), param, title.GetOrDefault(), null);

        internal static readonly Parser<Title> Title =
            from raw in Parse.CharExcept(Environment.NewLine).Many()
                //from raw in Parse.AnyChar.Except(Environment.NewLine)
                //from raw in Parse.AnyChar.Many().Text()

                //from raw in Parse.AnyChar().Except(Environment.NewLine) //.Many()  Parse.CharExcept(Environment.NewLine).Many().Optional() //.Many().Text().Optional()
            select new Title(raw.Any() ? string.Concat(raw) : null, raw.Any() ? string.Concat(raw) : null);

        // https://stackoverflow.com/questions/21414309/sprache-parse-signed-integer
        internal static readonly Parser<Media> Extinf =
            from first in LinesSpecification.ExtinfFirstLine.Token()
            from media in Parse.CharExcept(Environment.NewLine).Many().Text()
            from ignore in Parse.CharExcept('#').Many()
            select new Media(first.Duration, first.Attributes.AttributeList, first.Title, media);

        internal static readonly Parser<Media> Extgrp =            
            from category in Parse.CharExcept(Environment.NewLine).Many().Text()
            from nl in Parse.String("\r").Optional()
            from nl2 in Parse.String("\n").Optional()
                //from media in Parse.CharExcept(Environment.NewLine).Many().Text()
            from media in Parse.CharExcept((c) => c == '\n' && c == '\r' && c == '#', "").Many().Text()
            from ignore in Parse.CharExcept('#').Many()
            select new Media(0, null, null, media, category);

        internal static readonly Parser<Media> ExtVLT =
            from directive in Parse.CharExcept(Environment.NewLine).Many().Text()
            from nl in Parse.String("\r").Optional()
            from nl2 in Parse.String("\n").Optional()
            from media in Parse.CharExcept((c)=> c=='\n' && c== '\r' && c == '#',"").Many().Text()
            from ignore in Parse.CharExcept('#').Many()
            select new Media(0, null, null, media, null, directive);
    }

    //internal static class ParseSpecification
    //{
    //    internal static readonly Parser<Root> root =
    //            from header in LinesSpecification.HeaderLine.Token()
    //            from blankLine in Parse.String(Environment.NewLine).Optional()
    //            from info in LinesSpecification.Extinf.AtLeastOnce()
    //            select new Root(string.Empty, info, new AttributeInfo(header));
    //}


    
    internal static class SegmentSpecification
    {

        //using regex is  too slow. Dont ever do it again

        //Parse.Regex(new Regex(@"\s*#") );
        //A start line can have several empty chars (spaces, tabs, breaklines) and then 1 # sign
        private static Parser<IEnumerable<char>> startLineHash = Parse.Chars('\r', '\n', ' ', '\t').Many().Optional().Then(c=> Parse.Char('#').Once());

        //Parse.Regex(new Regex(@"[\n\r]\s*#") );
        private static Parser<IEnumerable<char>> breaklineAndStartLineHash = Parse.Chars('\r','\n').Then(c=> startLineHash);

        
        internal static readonly Parser<string> Segment =                
                from header in startLineHash.Once()
                from content in Parse.AnyChar.Except(breaklineAndStartLineHash).Many().Text()                
                select string.Concat("#", content);
        


        
        //Original segment logic
        /*
        internal static readonly Parser<string> Segment =
                from header in Parse.Chars(new char[] { '\r', '\n', '#' }).Many()
                from content in Parse.CharExcept(new char[] { '#' }).Many().Text()//this is not correct, an attribute or item name can have the # symbol
                select string.Concat("#", content);
        */


        internal static readonly Parser<IEnumerable<string>> SegmentCollection =
                from collection in Segment.Many()
                select collection;
    }
}