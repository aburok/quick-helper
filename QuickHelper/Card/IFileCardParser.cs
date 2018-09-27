using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickHelper.Card
{
    public interface IFileCardParser
    {
        bool CanParse(string fileName, IEnumerable<string> content);
        CardSetModel Parse(string fileName, IEnumerable<string> content);
    }

    public abstract class LineByLineFileCardParser : IFileCardParser
    {
        public virtual bool CanParse(string fileName, IEnumerable<string> content)
        {
            return true;
        }

        protected CardSetModel cardSet;

        public virtual CardSetModel Parse(string fileName, IEnumerable<string> content)
        {
            cardSet = new CardSetModel();

            foreach (var fileLine in content)
            {
                try
                {
                    ParseLine(fileLine);
                }
                catch (Exception e)
                {
                    Trace.TraceError("Unable to parse line : " + fileLine);
                }
            }

            cardSet.FilePath = fileName;
            cardSet.Init();
            return cardSet;
        }

        protected abstract void ParseLine(string line);

    }
}