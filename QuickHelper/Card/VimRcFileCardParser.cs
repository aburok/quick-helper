using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace QuickHelper.Card
{
    public class VimRcFileCardParser : LineByLineFileCardParser
    {
        private static readonly Regex mapPattern = new Regex(
            @"^\s*(n|v)?(nore)?map\s+(?<shortcut>.+)\s+:vsc\s+(?<command>.+)<CR>\s*""?(?<comment>.*)$");

        private static readonly Regex titlePattern = new Regex(@"^""""\s+(?<subject>.+)$");

        public override bool CanParse(string fileName, IEnumerable<string> content)
        {
            return Path.GetExtension(fileName) == ".vimrc";
        }

        protected override void ParseLine(string fileLine)
        {
            var questionAnswer = mapPattern.Match(fileLine);
            if (questionAnswer.Success)
            {
                cardSet.Questions.Add(new CardModel()
                {
                    Answer = questionAnswer.Groups["shortcut"].Value,
                    Question = questionAnswer.Groups["command"].Value,
                    Tags = "vim map"
                });
                return;
            }

            var subjectMatch = titlePattern.Match(fileLine);
            if (subjectMatch.Success)
            {
                cardSet.IdPrefix = subjectMatch.Groups["subject"].Value;
                return;
            }
        }
    }
}