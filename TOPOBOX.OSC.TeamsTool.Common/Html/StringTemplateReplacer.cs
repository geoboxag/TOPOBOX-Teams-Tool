using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TOPOBOX.OSC.TeamsTool.Common.Html
{
    /// <summary>
    /// Replaces keys (tags) in a string with the replacement values (non recursive version!).
    /// </summary>
    public static class StringTemplateReplacer
    {
        public static string Replace(string template, IEnumerable<KeyValuePair<string, string>> replacements,
                                        RegexOptions regexOptions = RegexOptions.ExplicitCapture
                                                                    | RegexOptions.IgnoreCase
                                                                    | RegexOptions.Multiline)
        {
            if (string.IsNullOrEmpty(template))
            {
                return string.Empty;
            }

            var replacementsDictionary = ToDictionary(replacements);
            if (replacements == null || replacementsDictionary.Count == 0)
            {
                return template;
            }

            var replacementRegEx = GenerateReplacementRegEx(replacementsDictionary.Keys);
            if (string.IsNullOrEmpty(replacementRegEx))
            {
                return template;
            }

            return Regex.Replace(template, replacementRegEx,
                matchEval => ReplaceKeyWithValue(replacementsDictionary, matchEval),
                                    regexOptions);
        }

        private static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> replacements)
        {
            if (replacements == null)
            {
                return new Dictionary<TKey, TValue>();
            }
            return replacements as IDictionary<TKey, TValue> ?? replacements.ToDictionary(keyItem => keyItem.Key, valueItem => valueItem.Value);
        }

        private static string ReplaceKeyWithValue(IDictionary<string, string> replacementsDictionary, Match matchEval)
        {
            return replacementsDictionary[matchEval.Groups["key"].Value];
        }

        public static string GenerateReplacementRegEx(IEnumerable<string> keys)
        {
            var keysList = ToList(keys);
            if (keys == null || keysList.Count == 0)
            {
                return null;
            }

            return string.Format("(?<key>({0}))", CreateRegexOptionsList(keysList));
        }

        private static IList<T> ToList<T>(IEnumerable<T> keys)
        {
            if (keys == null)
            {
                return Enumerable.Empty<T>().ToList();
            }

            return keys as IList<T> ?? keys.ToList();
        }

        private static string CreateRegexOptionsList(IList<string> keysList)
        {
            return String.Join("|", keysList.Select(Regex.Escape));
        }
    }
}