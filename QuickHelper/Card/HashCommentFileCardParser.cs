using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuickHelper.Card
{
    class HashCommentFileCardParser : LineByLineFileCardParser
    {
        private static readonly Regex questionPattern = new Regex("^(?<answer>.+)#(?<question>.+)$");
        private static readonly Regex tagLinePattern = new Regex(@"^\s*##\s*(?<tag>.+)$");
        private static readonly Regex subjectPattern = new Regex(@"^#\s+(?<title>.+)$");

        private string CurrentTag= String.Empty;

        protected override void ParseLine(string fileLine)
        {
            var titleMatch = subjectPattern.Match(fileLine);
            if (titleMatch.Success && cardSet.Subject == null)
            {
                cardSet.Subject = titleMatch.Groups["title"].Value;
                cardSet.IdPrefix = titleMatch.Groups["title"].Value;
            }

            var questionAnswer = questionPattern.Match(fileLine);
            if (questionAnswer.Success)
            {
                cardSet.Questions.Add(new CardModel()
                {
                    Answer = questionAnswer.Groups["answer"].Value,
                    Question = questionAnswer.Groups["question"].Value,
                    Tags = CurrentTag
                });
            }

            var tag = tagLinePattern.Match(fileLine);
            if (tag.Success)
            {
                CurrentTag = tag.Groups["tag"].Value;
            }
        }

        public override bool CanParse(string fileName, IEnumerable<string> content)
        {
            var firstLine = content.FirstOrDefault();
            return firstLine.StartsWith("#");
        }
    }
}